using LOC.PMS.Application.Interfaces.IRepositories;
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

        public async Task SaveVehicleDetails(VechicleDetails vechicleDetails)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@VehicleNo", vechicleDetails.VechicleNo),
                new SqlParameter("@DriverName", vechicleDetails.DriverName),
                new SqlParameter("@DriverPhoneNo", vechicleDetails.DriverMobileNo),
                new SqlParameter("@DCNo", vechicleDetails.DCNo),
            };
            await _context.QueryStoredProcedureAsync<OrderDetails>("[dbo].[VechicleDetails_Add]", sqlParams.ToArray());
            Task.CompletedTask.Wait();
        }

        public async Task UpdateScanDetails(List<int> PalletIds, int ScannedQty, string ToStatus)
        {
            string sql = @$"select StatusId from PalletStatus where PalletStatus='{ToStatus}'";
            var PalletStatusId = _context.QueryData<int>(sql);

            foreach(var PalletId in PalletIds)
            {
                string UpdatePalletQry = $"UPDATE PalletsByOrderTrans SET PalletStatus={PalletStatusId.First()} AND AssignedQty = {ScannedQty} WHERE PalletId IN ({PalletId})";
                _context.ExecuteSql(UpdatePalletQry);
            }
          
            Task.CompletedTask.Wait();
        }
    }
}
