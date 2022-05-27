using LOC.PMS.Application.Interfaces;
using LOC.PMS.Application.Interfaces.IRepositories;
using LOC.PMS.Model;
using LOC.PMS.Model.Report;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace LOC.PMS.Infrastructure.Repositories
{
    public class ReportDetailsRepository : IReportDetailsRepository
    {

        private readonly IContext _context;

        public ReportDetailsRepository(IContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderDetails>> GetDayPlanReport(string fromDate, string toDate)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@FromDate", fromDate),
                new SqlParameter("@ToDate", toDate),

            };

            return await _context.QueryStoredProcedureAsync<OrderDetails>("[dbo].[Report_DayPlanSelect_ByDate]", sqlParams.ToArray());
        }

        public async Task<IEnumerable<DCDetails>> GetDCDetail(string dCNumber)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@dCNumber", dCNumber),

            };

            return await _context.QueryStoredProcedureAsync<DCDetails>("[dbo].[Report_DCSelect_ByDCNumber]", sqlParams.ToArray());
        }

        public async Task<IEnumerable<DCDetails>> GetDCDetailsReport(string fromDate, string toDate)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@FromDate", fromDate),
                new SqlParameter("@ToDate", toDate),

            };

            return await _context.QueryStoredProcedureAsync<DCDetails>("[dbo].[Report_DCSelect_ByDate]", sqlParams.ToArray());
        }

        public async Task<IEnumerable<InwardReport>> GetInwardReport(int UserId, int PalletStatus)
        {

            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@UserId", UserId),
                new SqlParameter("@PalletStatus", PalletStatus)
            };

            return await _context.QueryStoredProcedureAsync<InwardReport>("[dbo].[Report_VendorInwardDetails]", sqlParams.ToArray());
        }

        public async Task<IEnumerable<InwardReportDetails>> GetInwardReportByDCNumber(string dCNumber, int PalletStatus)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@DCNumber", dCNumber),
                 new SqlParameter("@PalletStatus", PalletStatus)

            };

            return await _context.QueryStoredProcedureAsync<InwardReportDetails>("[dbo].[Report_VendorInwardDetails]", sqlParams.ToArray());
        }

        public async Task<IEnumerable<InwardReport>> GetInwardReportByPartNumber(int UserId, string partNumber, int PalletStatus)
        {

            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@UserId", UserId),
                new SqlParameter("@PartNo", partNumber),
                new SqlParameter("@PalletStatus", PalletStatus)

            };

            return await _context.QueryStoredProcedureAsync<InwardReport>("[dbo].[Report_VendorInwardDetails]", sqlParams.ToArray());
        }

        public async Task<IEnumerable<DayPlan>> GetMonthlyPlanReport(string fromDate, string toDate, int vendorId)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@FromDate", fromDate),
                new SqlParameter("@ToDate", toDate),
                new SqlParameter("@VendorId", vendorId)

            };
            return await _context.QueryStoredProcedureAsync<DayPlan>("[dbo].[Report_MonthlyPlan]", sqlParams.ToArray());
        }

        public async Task<IEnumerable<PalletsByOrderTransReport>> GetPalletOrderTransReport(string palletId)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@PalletId", palletId),
            };

            return await _context.QueryStoredProcedureAsync<PalletsByOrderTransReport>("[dbo].[Report_PalletsByOrderTransDetails]", sqlParams.ToArray());
        }

        public async Task<IEnumerable<PalletsPartTransReport>> GetPalletPartTransReport(int userId, string palletPartNo)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@UserId", userId),
                new SqlParameter("@PalletPartNo", palletPartNo)

            };

            return await _context.QueryStoredProcedureAsync<PalletsPartTransReport>("[dbo].[Report_PalletPartTransDetails]", sqlParams.ToArray());
        }

        public async Task<IEnumerable<PalletReportSelection>> GetPalletReportSelection(int UserId, string PalletStatus, string ModelNo)
        {

            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@UserId", UserId),
                new SqlParameter("@PalletStatus", PalletStatus),
                new SqlParameter("@ModelNo", ModelNo)
            };

            return await _context.QueryStoredProcedureAsync<PalletReportSelection>("[dbo].[Report_Pallet_Dropdown_Selection]", sqlParams.ToArray());
        }

        public async Task<IEnumerable<WareHouseTransitAndStock>> WareHouseTransitAndStockDetails(string Status)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@Status", Status)
            };

            return await _context.QueryStoredProcedureAsync<WareHouseTransitAndStock>("[dbo].[Report_AdminTransitandStock]", sqlParams.ToArray());
        }
    }
}
