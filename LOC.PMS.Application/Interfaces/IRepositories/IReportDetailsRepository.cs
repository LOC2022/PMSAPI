using LOC.PMS.Model;
using LOC.PMS.Model.DashBoard;
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

        Task<IEnumerable<DCDetails>> GetDCDetailsReport(string fromDate, string toDate, string UserId);

        Task<IEnumerable<OrderDetails>> GetDayPlanReport(string fromDate, string toDate);

        Task<IEnumerable<PalletsByOrderTransReport>> GetPalletOrderTransReport(string palletId);

        Task<IEnumerable<PalletsPartTransReport>> GetPalletPartTransReport(int userId, string palletPartNo);

        Task<IEnumerable<DCDetails>> GetDCDetail(string dCNumber);

        Task<IEnumerable<InwardReport>> GetInwardReport(int UserId, int PalletStatus);

        Task<IEnumerable<InwardReportDetails>> GetInwardReportByDCNumber(string dCNumber, int PalletStatus);

        Task<IEnumerable<InwardReport>> GetInwardReportByPartNumber(int UserId, string partNumber, int PalletStatus);
        Task<IEnumerable<MonthlyPlan>> GetDateWiseOrder(string fromDate, string toDate);
        Task<IEnumerable<WareHouseStock>> WareHouseStockDetails(string Status);

        Task<IEnumerable<InwardReport>> WareHouseTransitDetails(string Status, string DCNUmber, string PalletPartNo);

        Task<IEnumerable<PalletReportSelection>> GetPalletReportSelection(int UserId, string PalletStatus, string ModelNo);
        Task<IEnumerable<OrderDetailsByDate>> GetOrderDetailsByDate(string orderDate);
        DBPalletCount GetDBPalletCount(string userId);
        DBOnSite GetDBPalletCountByTypeOnSite(string userId);
        DBTransitCount GetDBPalletCountByTypeInTransit(string userId);

        List<DBPalletPart> GetDBPalletCountByTypeMaintenance(string userId);

        List<DBPalletPart> GetDBInTransit_AS(string userId);
        List<DBPalletPart> GetDBInTransit_SC(string userId);
        List<DBPalletPart> GetDBInTransit_CA(string userId);

        List<DBPalletPartDetails> GetPalletDetailsByPart(string userId, string status, string PalletPartNo);
        SupplierInOutFlow GetSupplierInOutFlowDB(string vendorId, string startDate, string endDate);
        PlannedDBData GetPlannedDBDetails(string UserId, string startDate, string endDate);
        TripDetails GetTripDBDetails(string startDate, string endDate);
        PalletAgingDetails GetPalletAgingDB(string vendorId, string aging);
        List<DCDetails> GetDcNoForManual(string vendorId, string flag);
        List<DCDetails> GetDcDetailsForInward(string dCNo);
        void SaveInwardDetails(List<string> lstPallets, string OrderNo);
        string ValidatePalletId(string palletId);
        void SaveDispatchDetails(List<DCDetails> lstPallets, string vendorId);
        List<Orders> GetOrderNoForManual();
        List<DCDetails> GetOrderDetailsForDispatch(string orderNo);
        void GenerateDCForManual(List<string> lstPallets, string orderNo);
        List<DCDetails> GetOrderNoForDispatch();
        void DispatchOrder(List<string> lstPallets, string orderNo);
        DCAdditionalDetails GetAddtionalDCDetails(string dCNo);
        List<FullCycleReport> GetFullCycleReport();
        void SaveMailDetails(MailModel mailModel);
        List<MailModel> GetMailDetails();
        void DeleteMailDetails(string id);
    }
}
