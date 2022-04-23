using LOC.PMS.Application.Interfaces;
using LOC.PMS.Application.Interfaces.IRepositories;
using LOC.PMS.Model;
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
    }
}
