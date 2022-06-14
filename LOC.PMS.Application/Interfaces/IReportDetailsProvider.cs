using LOC.PMS.Model;
using LOC.PMS.Model.Report;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LOC.PMS.Application.Interfaces
{
    public interface IReportDetailsProvider
    {
        Task<IEnumerable<DayPlan>> GetMonthlyPlanReport(string fromDate, string toDate, int vendorId);

        Task<IEnumerable<DCDetails>> GetDCDetailsReport(string fromDate, string toDate);

        Task<IEnumerable<OrderDetails>> GetDayPlanReport(string fromDate, string toDate);

        Task<IEnumerable<PalletsByOrderTransReport>> GetPalletOrderTransReport(string palletId);

        Task<IEnumerable<PalletsPartTransReport>> GetPalletPartTransReport(int userId, string palletPartNo);

        Task<IEnumerable<DCDetails>> GetDCDetails(string dCNumber);

        Task<IEnumerable<InwardReport>> GetInwardReport(int UserId, int PalletStatus);

        Task<IEnumerable<InwardReportDetails>> GetInwardReportByDCNumber(string dCNumber, int PalletStatus);

        Task<IEnumerable<InwardReport>> GetInwardReportByPartNumber(int UserId, string partNumber, int PalletStatus);

        Task<IEnumerable<WareHouseStock>> WareHouseStockDetails(string Status);

        Task<IEnumerable<InwardReport>> WareHouseTransitDetails(string Status, string DCNUmber, string PalletPartNo);

        Task<IEnumerable<PalletReportSelection>> GetPalletReportSelection(int UserId, string PalletStatus, string ModelNo);
        Task<IEnumerable<MonthlyPlan>> GetDateWiseOrder(string fromDate, string toDate);
        Task<IEnumerable<OrderDetailsByDate>> GetOrderDetailsByDate(string OrderDate);
    }
}
