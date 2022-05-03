using LOC.PMS.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LOC.PMS.Application.Interfaces.IRepositories
{
    public interface IReportDetailsRepository
    {
        Task<IEnumerable<DayPlan>> GetMonthlyPlanReport(string fromDate, string toDate, int vendorId);

        Task<IEnumerable<DCDetails>> GetDCDetailsReport(string fromDate, string toDate);

        Task<IEnumerable<OrderDetails>> GetDayPlanReport(string fromDate, string toDate);

        Task<IEnumerable<PalletsByOrderTransReport>> GetPalletOrderTransReport(string palletId);

        Task<IEnumerable<DCDetails>> GetDCDetail(string dCNumber);
    }
}
