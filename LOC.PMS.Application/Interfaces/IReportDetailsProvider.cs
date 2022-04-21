using LOC.PMS.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LOC.PMS.Application.Interfaces
{
    public interface IReportDetailsProvider
    {
        Task<IEnumerable<DayPlan>> GetMonthlyPlanReport(string FromDate, string ToDate, int VendorId);
    }
}
