using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LOC.PMS.Application.Interfaces.IRepositories;
using LOC.PMS.Model;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace LOC.PMS.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IContext _context;

        public OrderRepository(IContext context)
        {
            _context = context;
        }

        public Task AddDayPlanData(List<DayPlan> order)
        {
            var ColList = new List<string>
            {
                "PalletPartNo",
                "RequiredQty",
                "Description",
                "Model",
                "OrderDate",
                "VendorId",
                "CreatedDate"
            };
            _context.BulkCopy(order, ColList, order.Count, "DayPlan");
            CreateOrder();
            return Task.CompletedTask;
        }

        public Task CancelOrder(string orderNo)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@orderNo", orderNo)
            };
            _context.ExecuteStoredProcedure("[dbo].[Orders_Cancel]", sqlParams.ToArray());
            return Task.CompletedTask;
        }

        public void CreateOrder()
        {
            string VendorQry = @"select DISTINCT DP.VendorId,DP.OrderDate,COUNT(distinct DP.PalletPartNo) ReqPart,PM.D2LDays,VM.NonD2LDays from [dbo].[DayPlan] DP
                                LEFT JOIN PalletMaster PM on DP.PalletPartNo=PM.PalletPartNo
                                LEFT JOIN VendorMaster VM on VM.VendorId=DP.VendorId
                                where DP.IsActive = 1
                                GROUP BY DP.VendorId,DP.OrderDate,PM.D2LDays,VM.NonD2LDays";

            var dt = _context.QueryData<DayPlan>(VendorQry);
            List<Orders> OrderList = new List<Orders>();
            List<PalletsByOrderTrans> palletsByOrderTrans = new List<PalletsByOrderTrans>();

            foreach (var d in dt)
            {
                DateTime OrderDate;
                if (d.D2LDays != 0)
                    OrderDate = d.OrderDate.AddDays(-d.D2LDays);
                else if (d.NonD2LDays != 0)
                    OrderDate = d.OrderDate.AddDays(-d.NonD2LDays);
                else
                    OrderDate = d.OrderDate;

                if (OrderDate.ToString("dd-MM-yyyy") == DateTime.Now.ToString("dd-MM-yyyy"))
                {
                    var OrderId = "ORD" + DateTime.Now.ToString("ddMMyyyyHHmmssfff");
                    string sql = @$"select SUM(RequiredQty) Qty,PalletPartNo,Description PalletPartName,VendorId,OrderDate from [dbo].[DayPlan]
                            where IsActive = 1 and VendorId='{d.VendorId}' and OrderDate='{d.OrderDate}'
                            Group by PalletPartNo,Description,VendorId,OrderDate ";
                    var Data = _context.QueryData<DayPlan>(sql);

                    foreach (var d1 in Data)
                    {
                        var PalletSize = 1;
                        string strPalletWeight = @$"select top 1 KitUnit from PalletMaster where PalletPartNo='{d1.PalletPartNo}'";
                        PalletSize = _context.QueryData<int>(strPalletWeight).FirstOrDefault();
                        if (PalletSize != 0)
                        {
                            var ReqPallet = (int)(d1.Qty / PalletSize);
                            var Pallets = @"SELECT TOP  " + ReqPallet + @$"[PalletId]
                                  ,[PalletPartNo]
                                  ,[PalletName]
                                  ,[PalletWeight]
                                  ,[Model]
                                  ,[KitUnit]
                                  ,[WhereUsed]
                                  ,[PalletType]
                                  ,[LocationId]
                              FROM[PalletMaster] Where PalletPartNo = '{d1.PalletPartNo}' and Availability= {(int)PalletAvailability.Ideal}";

                            var PalletList = _context.QueryData<PalletDetails>(Pallets).ToList();


                            OrderList.Add(new Orders()
                            {
                                OrderNo = OrderId,
                                VendorId = d1.VendorId,
                                NoOfPartsOrdered = int.Parse(d.ReqPart),
                                OrderQty = d1.Qty,
                                OrderTypeId = 1,
                                OrderStatusId = OrderStatus.Open,
                                OrderCreatedDate = DateTime.Now
                            });

                            if (PalletList.Count > 0)
                            {
                                foreach (var d2 in PalletList)
                                {
                                    palletsByOrderTrans.Add(new PalletsByOrderTrans()
                                    {
                                        OrderNo = OrderId,
                                        PalletId = d2.PalletId,
                                        AssignedQty = d1.Qty,
                                        LocationId = d2.LocationId,
                                        PalletStatus = PalletStatus.Assigned,
                                        ModifiedDate = DateTime.Now,
                                        ModifiedBy = null
                                    });
                                }

                                var str = String.Join("','", PalletList.Select(x => x.PalletId));
                                string UpdatePalletQry = $"UPDATE PalletMaster SET Availability=2 WHERE Availability = 1 and PalletId IN ('{str}')";
                                _context.ExecuteSql(UpdatePalletQry);
                            }

                        }
                    }

                    string UpdateQry = $"UPDATE DayPlan SET IsActive=0 WHERE IsActive = 1 and VendorId = '{d.VendorId}' and OrderDate = '{d.OrderDate}'";
                    _context.ExecuteSql(UpdateQry);
                }

            }

            if (OrderList.Count > 0)
            {
                var sum = OrderList.Select(c => c.OrderQty).Sum();
                OrderList.FirstOrDefault().OrderQty = sum;
                if (palletsByOrderTrans.Count > 0)
                {
                    var Or = new List<Orders>();
                    Or.Add(OrderList.First());

                    var ColList = new List<string> { "OrderNo", "VendorId", "NoOfPartsOrdered", "OrderQty", "OrderTypeId", "OrderStatusId", "OrderCreatedDate" };
                    _context.BulkCopy(Or, ColList, 1, "Orders");

                    ColList = new List<string> {
                "OrderNo","PalletId","AssignedQty","LocationId","PalletStatus","ModifiedDate","ModifiedBy"
                };
                    _context.BulkCopy(palletsByOrderTrans, ColList, palletsByOrderTrans.Count, "PalletsByOrderTrans");
                    Thread.Sleep(5000);
                    SendMailToTMS(OrderList.First().OrderNo);
                }
            }




        }

        public async Task<IEnumerable<OrderDetails>> GetOrderDetails(string OrderNo)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@OrderNo", OrderNo)
            };
            return await _context.QueryStoredProcedureAsync<OrderDetails>("[dbo].[OrderDetails_Select]", sqlParams.ToArray());
        }

        Task IOrderRepository.CreateOrder()
        {

            CreateOrder();
            //SendMailToTMS("ORD16062022011136121");
            return Task.CompletedTask;
        }
        public async void SendMailToTMS(string Order)
        {
            if (!string.IsNullOrEmpty(Order))
            {


                string VendorQry = @$"select DISTINCT O.OrderNo,O.OrderQty,VM.VendorName,PO.PalletId
                                from Orders O
                                Join VendorMaster VM on VM.VendorId=O.VendorId
                                join PalletsByOrderTrans PO on Po.OrderNo=O.OrderNo where O.OrderNo='{Order}'";
                var dt = _context.QueryData<MailModel>(VendorQry).ToList();
                string HtmlContent = "Please Find the below Order Details Which as been Created \n";
                HtmlContent += " <table class='table table-bordered' style='border-collapse: collapse;border: 1px solid #ddd;'><thead><tr><td style='border: 1px solid #ddd; padding: 15px;'>Order No</td><td style='border: 1px solid #ddd; padding: 15px;'>Order Qty</td><td style='border: 1px solid #ddd; padding: 15px;'>Vendor Name</td><td style='border: 1px solid #ddd; padding: 15px;'>Pallet Id</td></tr><thead><tbody>";
                foreach (var data in dt)
                {
                    HtmlContent += $"<tr><td style='border: 1px solid #ddd; padding: 15px;'>{data.OrderNo}</td><td style='border: 1px solid #ddd; padding: 15px;'>{data.OrderQty}</td><td style='border: 1px solid #ddd;'>{data.VendorName}</td><td style='border: 1px solid #ddd; padding: 15px;'>{data.PalletId}</td></tr>";
                }
                HtmlContent += "</tbody></table>";


                string apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
                string fromEmail = "victor@theacedigi.com";

                var client = new SendGridClient(apiKey);
                var msg = new SendGridMessage()
                {
                    From = new EmailAddress(fromEmail, "Ace Digital"),
                    Subject = "",
                    HtmlContent = HtmlContent
                };

                var toEmailList = new List<EmailAddress>();
                var test = new EmailAddress(fromEmail, "Ace Digital");


                //toEmailList.Add(new EmailAddress("Raju_Rajendran@cat.com"));
                //toEmailList.Add(new EmailAddress("Sant_Kumar_Yadav_Astbhuja@cat.com"));
                //toEmailList.Add(new EmailAddress("B_Babu@cat.com"));
                //toEmailList.Add(new EmailAddress("Bakthavatchalu_Suresh@cat.com"));
                //toEmailList.Add(new EmailAddress("Chidambaram_Hariharasubramaniam@cat.com"));
                //toEmailList.Add(new EmailAddress("Eswaran_Vignesh@cat.com"));
                //toEmailList.Add(new EmailAddress("muthazagan123@gmail.com"));
                toEmailList.Add(new EmailAddress("muthazagan123@gmail.com", ""));
                toEmailList.Add(new EmailAddress("Saravana.m88@gmail.com", ""));



                var message = MailHelper.CreateSingleEmailToMultipleRecipients(test, toEmailList, "Order Details " + Order, "", HtmlContent);

                var result = await client.SendEmailAsync(message);


            }

        }

        public class MailModel
        {
            public string Email { get; set; }
            public string OrderNo { get; set; }
            public string OrderQty { get; set; }
            public string VendorName { get; set; }
            public string VendorId { get; set; }
            public string PalletId { get; set; }

        }
    }
}