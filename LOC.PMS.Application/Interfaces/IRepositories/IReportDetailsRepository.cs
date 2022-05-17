using LOC.PMS.Model;
using LOC.PMS.Model.Report;
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

        Task<IEnumerable<PalletsPartTransReport>> GetPalletPartTransReport( int userId, string palletPartNo);

        Task<IEnumerable<DCDetails>> GetDCDetail(string dCNumber);

        Task<IEnumerable<InwardReport>> GetInwardReport(int UserId);

        Task<IEnumerable<InwardReportDetails>> GetInwardReportByDCNumber(string dCNumber);

        Task<IEnumerable<InwardReport>> GetInwardReportByPartNumber(int UserId, string partNumber);

        Task<IEnumerable<PalletReportSelection>> GetPalletReportSelection(int UserId, string PalletStatus, string ModelNo);


    }
}
