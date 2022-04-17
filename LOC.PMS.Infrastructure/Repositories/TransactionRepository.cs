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

        public async Task<IEnumerable<OrderDetails>> GetOrderDetails(OrderDetails orderDetails)
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

        public async Task UpdateScanDetails(List<int> PalletIds, int ScannedQty, string ToStatus)
        {
            string sql = @$"select StatusId from PalletStatus where PalletStatus='{ToStatus}'";
            var PalletStatusId = _context.QueryData<int>(sql);

            foreach(var PalletId in PalletIds)
            {
                string UpdatePalletQry = $"UPDATE PalletsByOrderTrans SET PalletStatus={PalletStatusId.First()} WHERE PalletId IN ({PalletId})";
                _context.ExecuteSql(UpdatePalletQry);
            }
          
            Task.CompletedTask.Wait();
        }

        public async Task UpdateScanDetailsForInward(List<int> PalletIds, int ScannedQty, string ToStatus, string OrderNumber)
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
    }
}
