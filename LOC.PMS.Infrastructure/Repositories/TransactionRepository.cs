﻿using LOC.PMS.Application.Interfaces.IRepositories;
using LOC.PMS.Model;
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
                new SqlParameter("@ToPalletStage", ToPalletStage)
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
                string UpdatePalletQry = $"UPDATE PalletsByOrderTrans SET PalletStatus='{PalletStatusId.First()}' WHERE PalletId IN ('{PalletId}')";
                _context.ExecuteSql(UpdatePalletQry);

                //TODO: CHange the vendor no to get the user
                if (ToStatus == "CIPLInwardScan")
                {
                    string CreateDCQuery = @$"INSERT INTO [dbo].[DeliveryChallanTrans]
                   ([DCNo],[OrderNo],[PalletId],[VendorId],[DCType],[DCStatus],[CreatedDate],[CreatedBy])
			        select DISTINCT 'DC{DCNo}','DC{DCNo}',{PalletId},{VendorId},1,3,GETDATE(),''";

                    _context.ExecuteSql(CreateDCQuery);
                }
                else if(ToStatus == "CiplDisStg")
                {
                    string CreateDCQuery = @$"INSERT INTO [dbo].[DeliveryChallanTrans]
                   ([DCNo],[OrderNo],[PalletId],[VendorId],[DCType],[DCStatus],[CreatedDate],[CreatedBy])
			        select DISTINCT 'DC{DCNo}','DC{DCNo}',{PalletId},14,1,5,GETDATE(),''";

                    _context.ExecuteSql(CreateDCQuery);

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
                string UpdatePalletQry = $"UPDATE PalletsByOrderTrans SET PalletStatus='{PalletStatusId.First()}' WHERE PalletId IN ({PalletId}) AND OrderNo='{OrderNumber}'";
                _context.ExecuteSql(UpdatePalletQry);
            }

            string UpdateDCQry = $"UPDATE DeliveryChallanTrans SET DCStatus='3' WHERE OrderNo='{OrderNumber}' AND DCStatus='2'";
            _context.ExecuteSql(UpdateDCQry);

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
           ([DCNo],[OrderNo],[PalletId],[VendorId],[DCType],[DCStatus],[CreatedDate],[CreatedBy])
			select DISTINCT 'DC{DateTime.Now.ToString("ddMMyyyyHHmmss")}',PT.OrderNo,PalletId,O.VendorId,1,1,GETDATE(),'{orderDetails.First().UserId}' from 
			PalletsByOrderTrans PT  
			JOIN Orders O on O.OrderNo=PT.OrderNo where PalletId IN ('{PalletIds}') AND PT.OrderNo='{orderDetails.First().OrderNo}';";

            _context.ExecuteSql(UpdatePalletQry);

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

        public async Task<IEnumerable<DCDetails>> GetDCDetailsByPallet(string PalletId)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@PalletId", PalletId)

            };
            return await _context.QueryStoredProcedureAsync<DCDetails>("[dbo].[DC_SelectByPallet]", sqlParams.ToArray());
            //var res= await _context.ExecuteSqlAsync<List<DCDetails>>(@$"select PalletId,'Pending' as Status from [DeliveryChallanTrans] WHERE DCNo in (select TOP 1 DCNo from[dbo].[DeliveryChallanTrans] where PalletId = '{PalletId}' and IsActive = 1 Order by CreatedDate DESC )").Result;
            //return res;
        }
    }
}
