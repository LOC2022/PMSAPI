using LOC.PMS.Application.Interfaces.IRepositories;
using LOC.PMS.Model;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOC.PMS.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IContext _context;

        public TransactionRepository(IContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DCDetails>> GetDCDetails(string orderNo, string DCStatus, string UserName)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@OrderNo", orderNo),
                new SqlParameter("@Stage", DCStatus),
                new SqlParameter("@UserName", UserName)

            };
            return await _context.QueryStoredProcedureAsync<DCDetails>("[dbo].[DC_Select]", sqlParams.ToArray());
        }

        public async Task<IEnumerable<OrderDetails>> GetHHTOrderDetails(OrderDetails orderDetails)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@OrderNo", orderDetails.OrderNo),
                new SqlParameter("@Stage", orderDetails.Stage)
            };
            return await _context.QueryStoredProcedureAsync<OrderDetails>("[dbo].[HHTOrderDetails_Select]", sqlParams.ToArray());
        }



        public async Task SaveVehicleDetailsAndUpdateDCStatus(VechicleDetails vechicleDetails, string ToDCStage, string ToPalletStage)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@VehicleNo", vechicleDetails.VechicleNo),
                new SqlParameter("@DriverName", vechicleDetails.DriverName),
                new SqlParameter("@DriverPhoneNo", vechicleDetails.DriverMobileNo),
                new SqlParameter("@DCNo", vechicleDetails.DCNo),
                new SqlParameter("@ToDCStage", ToDCStage),
                new SqlParameter("@ToPalletStage", ToPalletStage),
                new SqlParameter("@RefNo", vechicleDetails.ReferenceNo)
            };
            await _context.QueryStoredProcedureAsync<OrderDetails>("[dbo].[UpdateDCAndVechicleDetails]", sqlParams.ToArray());
            Task.CompletedTask.Wait();
        }

        public async Task UpdateScanDetails(List<string> PalletIds, int ScannedQty, string ToStatus, int VendorId)
        {
            string sql = @$"select StatusId from PalletStatus where PalletStatus='{ToStatus}'";
            var PalletStatusId = _context.QueryData<int>(sql);
            var DCNo = DateTime.Now.ToString("ddMMyyyyHHmmss");
            foreach (var PalletId in PalletIds)
            {
                string UpdatePalletQry = $"UPDATE PalletsByOrderTrans SET PalletStatus='{PalletStatusId.First()}', ModifiedDate = GETDATE() WHERE PalletId IN ('{PalletId}')";
                _context.ExecuteSql(UpdatePalletQry);

                if (PalletId != "0")
                {
                    //TODO: CHange the vendor no to get the user
                    if (ToStatus == "CIPLInwardScan")
                    {
                        string CreateDCQuery = @$"INSERT INTO [dbo].[DeliveryChallanTrans]
                   ([DCNo],[OrderNo],[PalletId],[VendorId],[DCType],[DCStatus],[CreatedDate],[CreatedBy])
			        select DISTINCT 'DC{DCNo}','DC{DCNo}','{PalletId}',{VendorId},2,3,GETDATE(),''";

                        _context.ExecuteSql(CreateDCQuery);
                        SendMailToVendor($"DC{DCNo}");
                        string qryMaster = $"UPDATE PalletMaster set LocationId={VendorId} where PalletId IN ('{PalletId}')";
                        _context.ExecuteSql(qryMaster);
                    }
                    else if (ToStatus == "CIPLDispatchScan")
                    {
                        string CreateDCQuery = @$"INSERT INTO [dbo].[DeliveryChallanTrans]
                   ([DCNo],[OrderNo],[PalletId],[VendorId],[DCType],[DCStatus],[CreatedDate],[CreatedBy])
			        select DISTINCT 'DC{DCNo}','DC{DCNo}','{PalletId}',14,3,5,GETDATE(),''";

                        _context.ExecuteSql(CreateDCQuery);
                        SendMailToVendor($"DC{DCNo}");

                        string qryMaster = $"UPDATE PalletMaster set LocationId=14 where PalletId IN ('{PalletId}')";
                        _context.ExecuteSql(qryMaster);

                    }
                }

            }


            Task.CompletedTask.Wait();
        }

        public async Task UpdateScanDetailsForInward(List<string> PalletIds, int ScannedQty, string ToStatus, string OrderNumber)
        {
            string sql = @$"select StatusId from PalletStatus where PalletStatus='{ToStatus}'";
            var PalletStatusId = _context.QueryData<int>(sql);

            foreach (var PalletId in PalletIds)
            {
                if(ToStatus == "VendorDispatchScan")
                {
                    string UpdatePalletQry = $"UPDATE PalletsByOrderTrans SET PalletStatus='5', ModifiedDate = GETDATE() WHERE PalletId IN ('{PalletId}') AND OrderNo='{OrderNumber}'";
                    _context.ExecuteSql(UpdatePalletQry);
                }
                else
                {
                    string UpdatePalletQry = $"UPDATE PalletsByOrderTrans SET PalletStatus='{PalletStatusId.First()}', ModifiedDate = GETDATE() WHERE PalletId IN ('{PalletId}') AND OrderNo='{OrderNumber}'";
                    _context.ExecuteSql(UpdatePalletQry);
                }
                
            }

            if (ToStatus == "VendorDispatchScan")
            {
                string UpdateDCQry = $"UPDATE DeliveryChallanTrans SET DCStatus='3' WHERE OrderNo='{OrderNumber}' AND DCStatus='2'";
                _context.ExecuteSql(UpdateDCQry);
            }
            else
            {
                string UpdateDCQry = $"UPDATE DeliveryChallanTrans SET DCStatus='5' WHERE OrderNo='{OrderNumber}' AND DCStatus='4'";
                _context.ExecuteSql(UpdateDCQry);
            }


            //TODO: Split the DC into Repair DC for missing palletId during the scan

            Task.CompletedTask.Wait();
        }


        public Task SaveHHTOrderDetails(List<OrderDetails> orderDetails)
        {
            string PalletIds = "";
            if (orderDetails.Count > 0)
                PalletIds = string.Join("','", orderDetails.Select(x => x.PalletId.ToString()));

            string UpdatePalletQry = $"UPDATE PalletsByOrderTrans SET PalletStatus={(int)PalletStatus.Picked} WHERE PalletId IN ('{PalletIds}') AND OrderNo='{orderDetails.First().OrderNo}';";
            UpdatePalletQry += $"UPDATE Orders SET OrderStatusId={(int)OrderStatus.InTransit} WHERE  OrderNo='{orderDetails.First().OrderNo}';";
            UpdatePalletQry += @$"INSERT INTO [dbo].[DeliveryChallanTrans]
           ([DCNo],[OrderNo],[PalletId],[VendorId],[DCType],[DCStatus],[CreatedDate],[CreatedBy],[IsActive])
			select DISTINCT 'DC{DateTime.Now.ToString("ddMMyyyyHHmmss")}',PT.OrderNo,PalletId,O.VendorId,1,1,GETDATE(),'{orderDetails.First().UserId}',1 from 
			PalletsByOrderTrans PT  
			JOIN Orders O on O.OrderNo=PT.OrderNo where PalletId IN ('{PalletIds}') AND PT.OrderNo='{orderDetails.First().OrderNo}';";

            _context.ExecuteSql(UpdatePalletQry);

            SendMailToTMS(orderDetails[0].OrderNo);

            return Task.CompletedTask;
        }
        public Task UpdateHHTDispatchDetails(List<OrderDetails> orderDetails)
        {
            string PalletIds = "";
            if (orderDetails.Count > 0)
                PalletIds = string.Join("','", orderDetails.Select(x => x.PalletId.ToString()));

            string UpdatePalletQry = $"UPDATE PalletsByOrderTrans SET PalletStatus={(int)PalletStatus.FixedRead} WHERE PalletId IN ('{PalletIds}') AND OrderNo='{orderDetails.First().OrderNo}';";
            _context.ExecuteSql(UpdatePalletQry);

            return Task.CompletedTask;
        }

        public async Task<IEnumerable<DCDetails>> GetDCDetailsByPallet(string PalletId, int DCStatus, string DCNo)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@PalletId", PalletId),
                new SqlParameter("@DCStatus", DCStatus),
                new SqlParameter("@DCNo", DCNo)

            };
            return await _context.QueryStoredProcedureAsync<DCDetails>("[dbo].[DC_SelectByPallet]", sqlParams.ToArray());

        }

        public async Task<IEnumerable<PalletDetails>> GetPalletPartNo(string PalletPartNo)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@PalletPartNo", PalletPartNo)

            };
            return await _context.QueryStoredProcedureAsync<PalletDetails>("[dbo].[PalletId_Select]", sqlParams.ToArray());
        }

        public Task UpdateHHTInwardDetails(List<OrderDetails> orderDetails)
        {
            string PalletIds = "";
            if (orderDetails.Count > 0)
                PalletIds = string.Join("','", orderDetails.Select(x => x.PalletId.ToString()));

            if (orderDetails.FirstOrDefault().Status.ToString() == "INWARD")
            {
                string UpdatePalletQry = $"UPDATE PalletsByOrderTrans SET PalletStatus={(int)PalletStatus.WarehouseInward} WHERE PalletId IN ('{PalletIds}') AND PalletStatus=7;UPDATE [DeliveryChallanTrans] SET DCStatus=7 WHERE DCNo='{orderDetails.FirstOrDefault().OrderNo}'";
                //string UpdatePalletQry = $"UPDATE PalletsByOrderTrans SET PalletStatus={(int)PalletStatus.WarehouseInward} WHERE PalletId IN ('{PalletIds}') AND PalletStatus=8;UPDATE [DeliveryChallanTrans] SET DCStatus=7 WHERE DCNo='{orderDetails.FirstOrDefault().OrderNo}'";
                _context.ExecuteSql(UpdatePalletQry);

                string qryMaster = $"UPDATE PalletMaster set LocationId=13 where PalletId IN ('{PalletIds}')";
                _context.ExecuteSql(qryMaster);

            }
            else if (orderDetails.FirstOrDefault().Status.ToString() == "PUTAWAY")
            {
                string PalletUpdate = "";
                foreach (var data in orderDetails)
                {
                    PalletUpdate += $"UPDATE PalletMaster SET Availability={(int)PalletAvailability.Ideal},LocationId={data.Location} where PalletId='{data.PalletId}'; ";
                }
                string UpdatePalletQry = $"UPDATE [DeliveryChallanTrans] SET DCStatus=8 WHERE DCNo='{orderDetails.FirstOrDefault().OrderNo}' ;";
                _context.ExecuteSql(PalletUpdate);
                _context.ExecuteSql(UpdatePalletQry);
            }

            return Task.CompletedTask;
        }

        public async Task<IEnumerable<DCDetails>> GetPalletForPutAway()
        {
            return await _context.QueryStoredProcedureAsync<DCDetails>("[dbo].[PalletsForPutAway_Select]");
        }

        public Task UpdatePalletWriteCount(string PalletId)
        {
            string UpdatePalletQry = $"update [dbo].[PalletMaster] set [WriteCount]=[WriteCount]+1 where PalletId='{PalletId}'";
            _context.ExecuteSql(UpdatePalletQry);
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
                string HtmlContent = "Please Find the below Order Details \n";
                HtmlContent += "    ";
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


                toEmailList.Add(new EmailAddress("viswanathan_hemachandran@cat.com"));
                toEmailList.Add(new EmailAddress("s_arun_prasath@cat.com"));
                toEmailList.Add(new EmailAddress("GTOCITRL@cat.com"));
                toEmailList.Add(new EmailAddress("Raju_Rajendran@cat.com"));
                toEmailList.Add(new EmailAddress("Sant_Kumar_Yadav_Astbhuja@cat.com"));
                toEmailList.Add(new EmailAddress("B_Babu@cat.com"));
                toEmailList.Add(new EmailAddress("Bakthavatchalu_Suresh@cat.com"));
                toEmailList.Add(new EmailAddress("Chidambaram_Hariharasubramaniam@cat.com"));
                toEmailList.Add(new EmailAddress("Eswaran_Vignesh@cat.com"));
                toEmailList.Add(new EmailAddress("muthazagan123@gmail.com"));
                toEmailList.Add(new EmailAddress("Saravana.m88@gmail.com"));



                var message = MailHelper.CreateSingleEmailToMultipleRecipients(msg.From, toEmailList, "Order Details ", "", HtmlContent);

                var result = await client.SendEmailAsync(message);


            }

        }

        public async void SendMailToVendor(string DCNo)
        {
            if (!string.IsNullOrEmpty(DCNo))
            {

                string Email = "";
                string VendorQry = @$"select DISTINCT O.DCNo OrderNo,O.VendorId,VM.VendorName,PO.PalletId,VM.Email
                                from DeliveryChallanTrans O
                                Join VendorMaster VM on VM.VendorId=O.VendorId
                                join PalletsByOrderTrans PO on Po.OrderNo=O.OrderNo where O.DCNo='{DCNo}'";
                var dt = _context.QueryData<MailModel>(VendorQry).ToList();
                string HtmlContent = "Please Find the below DC Details \n";

                if (dt.Count > 0)
                {
                    HtmlContent += " <table class='table table-bordered' style='border-collapse: collapse;border: 1px solid #ddd;'><thead><tr><td style='border: 1px solid #ddd; padding: 15px;'>DC No</td><td style='border: 1px solid #ddd; padding: 15px;'>Vendor Name</td><td style='border: 1px solid #ddd; padding: 15px;'>Pallet Id</td></tr><thead><tbody>";
                    foreach (var data in dt)
                    {
                        HtmlContent += $"<tr><td style='border: 1px solid #ddd; padding: 15px;'>{data.OrderNo}</td><td style='border: 1px solid #ddd;'>{data.VendorName}</td><td style='border: 1px solid #ddd; padding: 15px;'>{data.PalletId}</td></tr>";
                    }
                    HtmlContent += "</tbody></table>";


                    Email = dt.FirstOrDefault().Email;


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


                    toEmailList.Add(new EmailAddress(string.IsNullOrEmpty(Email) ? "muthazagan123@gmail.com" : Email));
                    toEmailList.Add(new EmailAddress("viswanathan_hemachandran@cat.com"));
                    toEmailList.Add(new EmailAddress("s_arun_prasath@cat.com"));
                    toEmailList.Add(new EmailAddress("GTOCITRL@cat.com"));
                    toEmailList.Add(new EmailAddress("Raju_Rajendran@cat.com"));
                    toEmailList.Add(new EmailAddress("Sant_Kumar_Yadav_Astbhuja@cat.com"));
                    toEmailList.Add(new EmailAddress("B_Babu@cat.com"));
                    toEmailList.Add(new EmailAddress("Bakthavatchalu_Suresh@cat.com"));
                    toEmailList.Add(new EmailAddress("Chidambaram_Hariharasubramaniam@cat.com"));
                    toEmailList.Add(new EmailAddress("Eswaran_Vignesh@cat.com"));
                    toEmailList.Add(new EmailAddress("muthazagan123@gmail.com"));



                    var message = MailHelper.CreateSingleEmailToMultipleRecipients(msg.From, toEmailList, "DC Details ", "", HtmlContent);

                    var result = await client.SendEmailAsync(message);
                }



            }
        }

        public async Task<string> SwapPallets(string oldPalletId, string newPalletId, string OrderNo)
        {
            string qry = $"select COUNT(PalletId) from PalletMaster where PalletId='{newPalletId}' and Availability=1";
            var count = _context.QueryData<int>(qry).FirstOrDefault();
            if (count == 0)
            {
                return "Pallet Not Available";
            }
            else
            {
                var SwapQry = @$"Update PalletsByOrderTrans set PalletId='{newPalletId}' where PalletId='{oldPalletId}' and OrderNo='{OrderNo}';
                                Update PalletMaster SET Availability=1 where PalletId='{oldPalletId}';
                                Update PalletMaster SET Availability=2 where PalletId='{newPalletId}';";

                _context.ExecuteSql(SwapQry);
            }
            return "Updated SuccessFully";

        }

        public async Task<IEnumerable<DCDetails>> GetManualDcDetails(string vendorId)
        {
            return await _context.QueryStoredProcedureAsync<DCDetails>("[dbo].[PalletsForPutAway_Select]");
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
