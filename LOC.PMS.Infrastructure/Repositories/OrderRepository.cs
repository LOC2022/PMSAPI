using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LOC.PMS.Application.Interfaces.IRepositories;
using LOC.PMS.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace LOC.PMS.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IContext _context;
        private readonly IConfiguration _config;

        public OrderRepository(IContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
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

            Thread.Sleep(3000);
            CreateOrder();

            Thread.Sleep(3000);
            AssignPalletForDayOrder();
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

        //public void CreateOrder()
        //{
        //    string VendorQry = @"select DISTINCT DP.VendorId,DP.OrderDate,COUNT(distinct DP.PalletPartNo) ReqPart,PM.D2LDays,VM.NonD2LDays from [dbo].[DayPlan] DP
        //                        LEFT JOIN PalletMaster PM on DP.PalletPartNo=PM.PalletPartNo
        //                        LEFT JOIN VendorMaster VM on VM.VendorId=DP.VendorId
        //                        where DP.IsActive = 1
        //                        GROUP BY DP.VendorId,DP.OrderDate,PM.D2LDays,VM.NonD2LDays";

        //    var dt = _context.QueryData<DayPlan>(VendorQry);
        //    List<Orders> OrderList = new List<Orders>();
        //    List<PalletsByOrderTrans> palletsByOrderTrans = new List<PalletsByOrderTrans>();

        //    foreach (var d in dt)
        //    {
        //        DateTime OrderDate;
        //        if (d.D2LDays != 0)
        //            OrderDate = d.OrderDate.AddDays(-d.D2LDays);
        //        else if (d.NonD2LDays != 0)
        //            OrderDate = d.OrderDate.AddDays(-d.NonD2LDays);
        //        else
        //            OrderDate = d.OrderDate;

        //        if (OrderDate.ToString("dd-MM-yyyy") == DateTime.Now.ToString("dd-MM-yyyy"))
        //        {
        //            var OrderId = "ORD" + DateTime.Now.ToString("ddMMyyyyHHmmssfff");
        //            string sql = @$"select SUM(RequiredQty) Qty,PalletPartNo,Description PalletPartName,VendorId,OrderDate from [dbo].[DayPlan]
        //                    where IsActive = 1 and VendorId='{d.VendorId}' and OrderDate='{d.OrderDate}'
        //                    Group by PalletPartNo,Description,VendorId,OrderDate ";
        //            var Data = _context.QueryData<DayPlan>(sql);

        //            foreach (var d1 in Data)
        //            {
        //                var PalletSize = 1;
        //                string strPalletWeight = @$"select top 1 KitUnit from PalletMaster where PalletPartNo='{d1.PalletPartNo}'";
        //                PalletSize = _context.QueryData<int>(strPalletWeight).FirstOrDefault();
        //                if (PalletSize != 0)
        //                {
        //                    var ReqPallet = (int)(d1.Qty / PalletSize);
        //                    var Pallets = @"SELECT TOP  " + ReqPallet + @$"[PalletId]
        //                          ,[PalletPartNo]
        //                          ,[PalletName]
        //                          ,[PalletWeight]
        //                          ,[Model]
        //                          ,[KitUnit]
        //                          ,[WhereUsed]
        //                          ,[PalletType]
        //                          ,[LocationId]
        //                      FROM[PalletMaster] Where PalletPartNo = '{d1.PalletPartNo}' and Availability= {(int)PalletAvailability.Ideal}";

        //                    var PalletList = _context.QueryData<PalletDetails>(Pallets).ToList();


        //                    OrderList.Add(new Orders()
        //                    {
        //                        OrderNo = OrderId,
        //                        VendorId = d1.VendorId,
        //                        NoOfPartsOrdered = int.Parse(d.ReqPart),
        //                        OrderQty = d1.Qty,
        //                        OrderTypeId = 1,
        //                        OrderStatusId = OrderStatus.Open,
        //                        OrderCreatedDate = DateTime.Now
        //                    });

        //                    if (PalletList.Count > 0)
        //                    {
        //                        foreach (var d2 in PalletList)
        //                        {
        //                            palletsByOrderTrans.Add(new PalletsByOrderTrans()
        //                            {
        //                                OrderNo = OrderId,
        //                                PalletId = d2.PalletId,
        //                                AssignedQty = d1.Qty,
        //                                LocationId = d2.LocationId,
        //                                PalletStatus = PalletStatus.Assigned,
        //                                ModifiedDate = DateTime.Now,
        //                                ModifiedBy = null
        //                            });
        //                        }

        //                        var str = String.Join("','", PalletList.Select(x => x.PalletId));
        //                        string UpdatePalletQry = $"UPDATE PalletMaster SET Availability=2 WHERE Availability = 1 and PalletId IN ('{str}')";
        //                        _context.ExecuteSql(UpdatePalletQry);
        //                    }

        //                }
        //            }

        //            string UpdateQry = $"UPDATE DayPlan SET IsActive=0 WHERE IsActive = 1 and VendorId = '{d.VendorId}' and OrderDate = '{d.OrderDate}'";
        //            _context.ExecuteSql(UpdateQry);
        //        }

        //    }

        //    if (OrderList.Count > 0)
        //    {
        //        var sum = OrderList.Select(c => c.OrderQty).Sum();
        //        OrderList.FirstOrDefault().OrderQty = sum;
        //        if (palletsByOrderTrans.Count > 0)
        //        {
        //            var Or = new List<Orders>();
        //            Or.Add(OrderList.First());

        //            var ColList = new List<string> { "OrderNo", "VendorId", "NoOfPartsOrdered", "OrderQty", "OrderTypeId", "OrderStatusId", "OrderCreatedDate" };
        //            _context.BulkCopy(Or, ColList, 1, "Orders");

        //            ColList = new List<string> {
        //        "OrderNo","PalletId","AssignedQty","LocationId","PalletStatus","ModifiedDate","ModifiedBy"
        //        };
        //            _context.BulkCopy(palletsByOrderTrans, ColList, palletsByOrderTrans.Count, "PalletsByOrderTrans");
        //            Thread.Sleep(5000);
        //            SendMailToTMS(OrderList.First().OrderNo);
        //        }
        //    }




        //}

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

            //CreateOrder();
            //SendMailToTMS("ORD16062022011136121");
            AssignPalletForDayOrder();
            return Task.CompletedTask;
        }
        public async void SendMailToTMS(string Order)
        {
            if (!string.IsNullOrEmpty(Order))
            {


                string VendorQry = @$"select DISTINCT O.OrderNo,O.OrderQty,VM.VendorName,PM.PalletPartNo,COUNT(PO.PalletId) Qty,PM.PalletWeight ,PM.Dimensions
                                from Orders O
                                Join VendorMaster VM on VM.VendorId=O.VendorId
                                join PalletsByOrderTrans PO on Po.OrderNo=O.OrderNo 
								JOIN PalletMaster PM on Pm.PalletId=PO.PalletId
								where O.OrderNo='{Order}'
								GROUP BY O.OrderNo,O.OrderQty,VM.VendorName,PM.PalletPartNo,PM.PalletWeight ,PM.Dimensions


";
                var dt = _context.QueryData<MailModel>(VendorQry).ToList();
                string HtmlContent = @"<html>
    <head>
       
    <style>
.table-bordered>:not(caption)>*>* {
    border - width: 0 1px;
}
@media (min-width: 1200px)
.container, .container-lg, .container-md, .container-sm, .container-xl {
    max - width: 1140px;
}
@media (min-width: 992px)
.container, .container-lg, .container-md, .container-sm {
    max - width: 960px;
}
@media (min-width: 768px)
.container, .container-md, .container-sm {
    max - width: 720px;
}
@media (min-width: 576px)
.container, .container-sm {
    max - width: 540px;
}
.row {
    --bs - gutter - x: 1.5rem;
    --bs-gutter-y: 0;
    display: flex;
    flex-wrap: wrap;
    margin-top: calc(-1 * var(--bs-gutter-y));
    margin-right: calc(-.5 * var(--bs-gutter-x));
    margin-left: calc(-.5 * var(--bs-gutter-x));
}
.container, .container-fluid, .container-lg, .container-md, .container-sm, .container-xl, .container-xxl {
    --bs - gutter - x: 1.5rem;
    --bs-gutter-y: 0;
    width: 100%;
    padding-right: calc(var(--bs-gutter-x) * .5);
    padding-left: calc(var(--bs-gutter-x) * .5);
    margin-right: auto;
    margin-left: auto;
}
body {
    margin: 0;
    font-family: var(--bs-body-font-family);
    font-size: var(--bs-body-font-size);
    font-weight: var(--bs-body-font-weight);
    line-height: var(--bs-body-line-height);
    color: var(--bs-body-color);
    text-align: var(--bs-body-text-align);
    background-color: var(--bs-body-bg);
    -webkit-text-size-adjust: 100%;
    -webkit-tap-highlight-color: transparent;
}
*, ::after, ::before {
    box - sizing: border-box;
}
*, ::after, ::before {
    box - sizing: border-box;
}
.table {
    --bs - table - color: var(--bs-body-color);
    --bs-table-bg: transparent;
    --bs-table-border-color: var(--bs-border-color);
    --bs-table-accent-bg: transparent;
    --bs-table-striped-color: var(--bs-body-color);
    --bs-table-striped-bg: rgba(0, 0, 0, 0.05);
    --bs-table-active-color: var(--bs-body-color);
    --bs-table-active-bg: rgba(0, 0, 0, 0.1);
    --bs-table-hover-color: var(--bs-body-color);
    --bs-table-hover-bg: rgba(0, 0, 0, 0.075);
    width: 100%;
    margin-bottom: 1rem;
    color: var(--bs-table-color);
    vertical-align: top;
    border-color: var(--bs-table-border-color);
}
table {
    caption - side: bottom;
    border-collapse: collapse;
}
.table>thead {
    vertical - align: bottom;
}
tbody, td, tfoot, th, thead, tr {
    border - color: inherit;
    border-style: solid;
    border-width: 0;
}
.table-bordered>:not(caption)>* {
    border - width: 1px 0;
}
tbody, td, tfoot, th, thead, tr {
    border - color: inherit;
    border-style: solid;
    border-width: 0;
}
.table>tbody {
    vertical - align: inherit;
}
.col-3 {
    flex: 0 0 auto;
    width: 25% !important;;
}
.row>* {
    flex - shrink: 0;
    width: 100%;
    max-width: 100%;
    padding-right: calc(var(--bs-gutter-x) * .5);
    padding-left: calc(var(--bs-gutter-x) * .5);
    margin-top: var(--bs-gutter-y);
}
.text-success {
    --bs - text - opacity: 1;
    color: rgba(var(--bs-success-rgb),var(--bs-text-opacity))!important;
}
label {
    display: inline-block;
}
:root {
    --bs - blue: #0d6efd;
    --bs-indigo: #6610f2;
    --bs-purple: #6f42c1;
    --bs-pink: #d63384;
    --bs-red: #dc3545;
    --bs-orange: #fd7e14;
    --bs-yellow: #ffc107;
    --bs-green: #198754;
    --bs-teal: #20c997;
    --bs-cyan: #0dcaf0;
    --bs-black: #000;
    --bs-white: #fff;
    --bs-gray: #6c757d;
    --bs-gray-dark: #343a40;
    --bs-gray-100: #f8f9fa;
    --bs-gray-200: #e9ecef;
    --bs-gray-300: #dee2e6;
    --bs-gray-400: #ced4da;
    --bs-gray-500: #adb5bd;
    --bs-gray-600: #6c757d;
    --bs-gray-700: #495057;
    --bs-gray-800: #343a40;
    --bs-gray-900: #212529;
    --bs-primary: #0d6efd;
    --bs-secondary: #6c757d;
    --bs-success: #198754;
    --bs-info: #0dcaf0;
    --bs-warning: #ffc107;
    --bs-danger: #dc3545;
    --bs-light: #f8f9fa;
    --bs-dark: #212529;
    --bs-primary-rgb: 13,110,253;
    --bs-secondary-rgb: 108,117,125;
    --bs-success-rgb: 25,135,84;
    --bs-info-rgb: 13,202,240;
    --bs-warning-rgb: 255,193,7;
    --bs-danger-rgb: 220,53,69;
    --bs-light-rgb: 248,249,250;
    --bs-dark-rgb: 33,37,41;
    --bs-white-rgb: 255,255,255;
    --bs-black-rgb: 0,0,0;
    --bs-body-color-rgb: 33,37,41;
    --bs-body-bg-rgb: 255,255,255;
   
}

</style >
    </head>
    <body>
        <div class='container'>

            <p>
Dear All,<br/> The Order has been Created Successfully. Please Find the Order Details Below

            </p>
<div class='row'>
    <div class='col-3'>
    <label class='text-success' ><strong>Order No:</strong> </label>

    </div>
    <div class='col-3'>
        <label class='' >" + dt.FirstOrDefault().OrderNo + @"</label>

        </div >
        <div class='col-3'>
            <label class='text-success' ><strong>Vendor Name:</strong> </label>
        
            </div>
            <div class='col-3'>
                <label class='' >" + dt.FirstOrDefault().VendorName + @"</label>
            
                </div>
</div>
<div class='row'>
    <div class='col-3'>
    <label class='text-success' ><strong>Order Qty:</strong> </label>

    </div>
    <div class='col-3'>
        <label class='' >" + dt.FirstOrDefault().OrderQty + @"</ label >


</div>


</div>
<div class= 'mt-4' >

<table class= 'table table-bordered' >

<thead style = 'color: whitesmoke;background: rgb(117, 151, 83);' >

<tr>

<td> Order No </td>

<td> Pallet Part No</td>
                <td>Part Qty</td>
                <td>Dimensions</td>
                <td>Weight</td>
            </tr>
            <thead>
            <tbody>";
                //HtmlContent += " <table class='table table-bordered' style='border-collapse: collapse;border: 1px solid #ddd;'><thead><tr><td style='border: 1px solid #ddd; padding: 15px;'>Order No</td><td style='border: 1px solid #ddd; padding: 15px;'>Order Qty</td><td style='border: 1px solid #ddd; padding: 15px;'>Vendor Name</td><td style='border: 1px solid #ddd; padding: 15px;'>Pallet Id</td></tr><thead><tbody>";
                foreach (var data in dt)
                {
                    HtmlContent += $"<tr><td>{data.OrderNo}</td><td>{data.PalletPartNo}</td><td>{data.Qty}</td><td >{data.Dimensions}</td><td style='border: 1px solid #ddd; padding: 15px;'>{data.PalletWeight}</td></tr>";
                }
                HtmlContent += @"</tbody>
                                </table>
                            </div>

                                    </div>
                                </body>
                            </html>";


                //string apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
                string apiKey = _config.GetValue<string>("NotificationSettings:EmailNotification:SENDGRID_API_KEY");
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


                var _lstMail = GetMailIds("1");
                foreach (var mail in _lstMail)
                {
                    toEmailList.Add(new EmailAddress(mail, ""));
                }
                //toEmailList.Add(new EmailAddress("muthazagan123@gmail.com", ""));
                //toEmailList.Add(new EmailAddress("Saravana.m88@gmail.com", ""));
                //toEmailList.Add(new EmailAddress("shalibhadra1488@gmail.com", ""));



                var message = MailHelper.CreateSingleEmailToMultipleRecipients(test, toEmailList, "Order Details " + Order, "", HtmlContent);

                var result = await client.SendEmailAsync(message);


            }

        }

        public void CreateOrder()
        {
            string VendorQry = @"select DISTINCT SUM(RequiredQty) Qty,COUNT(distinct DP.PalletPartNo) ReqPart, DP.VendorId,DP.OrderDate,DP.VendorId from [dbo].[DayPlan] DP                               
                                LEFT JOIN VendorMaster VM on VM.VendorId=DP.VendorId
                                where DP.IsActive = 1
                                GROUP BY DP.VendorId,DP.OrderDate,DP.VendorId";

            var dt = _context.QueryData<DayPlan>(VendorQry);
            List<Orders> OrderList = new List<Orders>();
            List<PalletsByOrderTrans> palletsByOrderTrans = new List<PalletsByOrderTrans>();

            foreach (var d in dt)
            {
                //DateTime OrderDate;
                //if (d.D2LDays != 0)
                //    OrderDate = d.OrderDate.AddDays(-d.D2LDays);
                //else if (d.NonD2LDays != 0)
                //    OrderDate = d.OrderDate.AddDays(-d.NonD2LDays);
                //else
                //
                DateTime OrderDate = d.OrderDate.AddDays(-6);


                var OrderId = "ORD" + DateTime.Now.ToString("ddMMyyyyHHmmssfff");
                //string sql = @$"select SUM(RequiredQty) Qty,PalletPartNo,Description PalletPartName,VendorId,OrderDate from [dbo].[DayPlan]
                //            where IsActive = 1 and VendorId='{d.VendorId}' and OrderDate='{d.OrderDate}'
                //            Group by PalletPartNo,Description,VendorId,OrderDate ";
                //var Data = _context.QueryData<DayPlan>(sql);



                OrderList.Add(new Orders()
                {
                    OrderNo = OrderId,
                    VendorId = d.VendorId,
                    NoOfPartsOrdered = int.Parse(d.ReqPart),
                    OrderQty = d.Qty,
                    OrderTypeId = 1,
                    OrderStatusId = OrderStatus.Open,
                    OrderCreatedDate = DateTime.Now,
                    OrderDate = d.OrderDate,
                });

                string UpdateQry = $"UPDATE DayPlan SET IsActive=0,OrderNo='{OrderId}' WHERE IsActive = 1 and VendorId = '{d.VendorId}' and OrderDate = '{d.OrderDate}'";
                _context.ExecuteSql(UpdateQry);
            }






            if (OrderList.Count > 0)
            {

                var ColList = new List<string> { "OrderNo", "VendorId", "NoOfPartsOrdered", "OrderQty", "OrderTypeId", "OrderStatusId", "OrderCreatedDate", "OrderDate" };
                _context.BulkCopy(OrderList, ColList, 1, "Orders");

                OrderList = new List<Orders>();
                palletsByOrderTrans = new List<PalletsByOrderTrans>();
            }




        }

        public void AssignPalletForDayOrder()
        {
            string VendorQry = @"select distinct OrderDate,OrderNo,OrderQty,ISNULL(Shortage,0) Shortage from Orders O								
								where o.palletassignedflag=1
								order by ISNULL(Shortage,0) desc";
            var dt = _context.QueryData<DayPlan>(VendorQry);

            foreach (var d in dt)
            {
                List<PalletsByOrderTrans> palletsByOrderTrans = new List<PalletsByOrderTrans>();

                DateTime OrderDate;
                if (d.D2LDays != 0)
                    OrderDate = d.OrderDate.AddDays(-d.D2LDays);
                else if (d.NonD2LDays != 0)
                    OrderDate = d.OrderDate.AddDays(-d.NonD2LDays);
                else
                    OrderDate = d.OrderDate.AddDays(-6);

                if (OrderDate.ToString("dd-MM-yyyy") == DateTime.Now.ToString("dd-MM-yyyy"))
                {

                    if (d.Shortage == 0)
                    {
                        string qryPalletPart = $"select PalletPartNo,RequiredQty from DayPlan where OrderNo='{d.OrderNo}'";

                        var palletPartData = _context.QueryData<DayPlan>(qryPalletPart);

                        foreach (var pallet in palletPartData)
                        {
                            var Pallets = @"SELECT TOP  " + pallet.RequiredQty + @$"[PalletId]
                                      ,[PalletPartNo]                                     
                                      ,[LocationId]
                                  FROM[PalletMaster] Where PalletPartNo = '{pallet.PalletPartNo}' and Availability= {(int)PalletAvailability.Ideal}";

                            var PalletList = _context.QueryData<PalletDetails>(Pallets).ToList();

                            foreach (var pl in PalletList)
                            {
                                palletsByOrderTrans.Add(new PalletsByOrderTrans()
                                {
                                    OrderNo = d.OrderNo,
                                    PalletId = pl.PalletId,
                                    AssignedQty = 1,
                                    LocationId = pl.LocationId,
                                    PalletStatus = PalletStatus.Assigned,
                                    ModifiedDate = DateTime.Now,
                                    ModifiedBy = "ADMIN"
                                });
                            }
                        }

                        if (palletsByOrderTrans.Count > 0)
                        {

                            var ColList = new List<string> {
                "OrderNo","PalletId","AssignedQty","LocationId","PalletStatus","ModifiedDate","ModifiedBy"
                };
                            _context.BulkCopy(palletsByOrderTrans, ColList, palletsByOrderTrans.Count, "PalletsByOrderTrans");

                            var Pallets = string.Join("','", palletsByOrderTrans.Select(s => s.PalletId).ToList());
                            _context.ExecuteSql($"Update PalletMaster set Availability=2  where PalletId IN ('{Pallets}') ");


                            if (d.OrderQty == palletsByOrderTrans.Count)
                            {

                                _context.ExecuteSql($"Update Orders set palletassignedflag=0,Shortage=(OrderQty-{palletsByOrderTrans.Count}) where OrderNo='{palletsByOrderTrans.First().OrderNo}' ");
                                Thread.Sleep(2000);
                                SendMailToTMS(palletsByOrderTrans.First().OrderNo);

                            }
                            else
                            {
                                _context.ExecuteSql($"Update Orders set Shortage=(OrderQty-{palletsByOrderTrans.Count}) where OrderNo='{palletsByOrderTrans.First().OrderNo}' ");

                            }
                        }
                    }
                    else
                    {
                        string qryPalletPart = @$"
                            WITH
                            A(PalletPart, Qty)
                            as
                            (
                            select PalletPartNo, Count(PalletId)AssignedQty from PalletMaster where PalletId In(
                            select PalletId from PalletsByOrderTrans where OrderNo = '{d.OrderNo}'
                            )
                            group by PalletPartNo
                            ) 
                            select B.PalletPartNo,RequiredQty - Qty as RequiredQty from A
                              join (select PalletPartNo,RequiredQty from DayPlan WHERE OrderNo = '{d.OrderNo}') B on A.PalletPart = B.PalletPartNo
                            where RequiredQty-Qty > 0
                            ";

                        var palletPartData = _context.QueryData<DayPlan>(qryPalletPart);

                        foreach (var pallet in palletPartData)
                        {
                            var Pallets = @"SELECT TOP  " + pallet.RequiredQty + @$"[PalletId]
                                      ,[PalletPartNo]                                     
                                      ,[LocationId]
                                  FROM[PalletMaster] Where PalletPartNo = '{pallet.PalletPartNo}' and Availability= {(int)PalletAvailability.Ideal}";

                            var PalletList = _context.QueryData<PalletDetails>(Pallets).ToList();

                            foreach (var pl in PalletList)
                            {
                                palletsByOrderTrans.Add(new PalletsByOrderTrans()
                                {
                                    OrderNo = d.OrderNo,
                                    PalletId = pl.PalletId,
                                    AssignedQty = 1,
                                    LocationId = pl.LocationId,
                                    PalletStatus = PalletStatus.Assigned,
                                    ModifiedDate = DateTime.Now,
                                    ModifiedBy = "ADMIN"
                                });
                            }
                        }

                        if (palletsByOrderTrans.Count > 0)
                        {

                            var ColList = new List<string> {
                "OrderNo","PalletId","AssignedQty","LocationId","PalletStatus","ModifiedDate","ModifiedBy"
                };
                            _context.BulkCopy(palletsByOrderTrans, ColList, palletsByOrderTrans.Count, "PalletsByOrderTrans");

                            var Pallets = string.Join("','", palletsByOrderTrans.Select(s => s.PalletId).ToList());
                            _context.ExecuteSql($"Update PalletMaster set Availability=2  where PalletId IN ('{Pallets}') ");

                            int qty = _context.QueryData<int>($"select COUNT(*) from PalletsByOrderTrans where OrderNo='{d.OrderNo}'").FirstOrDefault();

                            if (d.OrderQty == qty)
                            {

                                _context.ExecuteSql($"Update Orders set palletassignedflag=0,Shortage=(Shortage-{palletsByOrderTrans.Count}) where OrderNo='{palletsByOrderTrans.First().OrderNo}' ");
                                Thread.Sleep(4000);
                                SendMailToTMS(palletsByOrderTrans.First().OrderNo);

                            }
                            else
                            {
                                _context.ExecuteSql($"Update Orders set Shortage=(OrderQty-{palletsByOrderTrans.Count}) where OrderNo='{palletsByOrderTrans.First().OrderNo}' ");

                            }
                        }
                    }




                }
            }
        }


        private List<string> GetMailIds(string Id)
        {
            return _context.QueryData<string>(@$"select Email from tbl_MailConfiguration where ModuleId={Id} and Isactive=1").ToList();
        }



        public class MailModel
        {
            public string Email { get; set; }
            public string OrderNo { get; set; }
            public string OrderQty { get; set; }
            public string VendorName { get; set; }
            public string VendorId { get; set; }
            public string PalletId { get; set; }
            public string Qty { get; set; }
            public string PalletWeight { get; set; }
            public string Dimensions { get; set; }
            public string PalletPartNo { get; set; }

        }
    }
}