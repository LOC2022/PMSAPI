using LOC.PMS.Application.Interfaces;
using LOC.PMS.Application.Interfaces.IRepositories;
using LOC.PMS.Model;
using LOC.PMS.Model.Report;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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

        public async Task<IEnumerable<DCDetails>> GetDCDetailsReport(string fromDate, string toDate, string UserId)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@FromDate", fromDate),
                new SqlParameter("@ToDate", toDate),
                new SqlParameter("@UserId", UserId=="13"?"":UserId)

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

        public async Task<IEnumerable<WareHouseStock>> WareHouseStockDetails(string Status)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@Status", Status)
            };

            return await _context.QueryStoredProcedureAsync<WareHouseStock>("[dbo].[Report_AdminStock]", sqlParams.ToArray());
        }

        public async Task<IEnumerable<InwardReport>> WareHouseTransitDetails(string Status, string DCNUmber, string PalletPartNo)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@PalletStatus", Status),
                new SqlParameter("@DCNumber", DCNUmber),
                new SqlParameter("@PartNo", PalletPartNo)
            };

            return await _context.QueryStoredProcedureAsync<InwardReport>("[dbo].[Report_AdminInwardDispatchDetails]", sqlParams.ToArray());
        }

        public async Task<IEnumerable<MonthlyPlan>> GetDateWiseOrder(string fromDate, string toDate)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@FromDate", fromDate),
                new SqlParameter("@ToDate", toDate),

            };

            return await _context.QueryStoredProcedureAsync<MonthlyPlan>("[dbo].[Report_DateWiseOrder]", sqlParams.ToArray());
        }

        public async Task<IEnumerable<OrderDetailsByDate>> GetOrderDetailsByDate(string Date)
        {
            List<IDbDataParameter> sqlParams = new List<IDbDataParameter>
            {
                new SqlParameter("@OrderDate", Date)
            };
            return await _context.QueryStoredProcedureAsync<OrderDetailsByDate>("[dbo].[Report_OrderDetailsByDate]", sqlParams.ToArray());
        }

        public DBPalletCount GetDBPalletCount(string userId)
        {
            DBPalletCount dBPalletCount = new DBPalletCount();

            var dt = _context.QueryData<int>("select Count(*) from PalletsByOrderTrans where PalletStatus in (1, 2, 3, 5, 7)").FirstOrDefault();
            var dt1 = _context.QueryData<int>("select Count(*) from PalletsByOrderTrans where PalletStatus in (4,8,6)").FirstOrDefault();
            var dt2 = _context.QueryData<int>("select Count(*) from PalletsByOrderTrans where PalletStatus in (11)").FirstOrDefault();


            dBPalletCount.OnSite = Convert.ToString(dt);
            dBPalletCount.InTransit = Convert.ToString(dt1);
            dBPalletCount.Maintenance = Convert.ToString(dt2);


            return dBPalletCount;
        }

        public DBOnSite GetDBPalletCountByTypeOnSite(string userId)
        {
            DBOnSite dBOnSite = new DBOnSite();


            var dt = _context.QueryData<int>(@"select COUNT(*) from [dbo].PalletsByOrderTrans PR JOIN [dbo].[PalletMaster] PM ON PR.PalletId = PM.PalletId Where PR.PalletStatus  in (1, 2)").FirstOrDefault();
            var dt1 = _context.QueryData<int>(@$"select COUNT(*) from [dbo].PalletsByOrderTrans PR JOIN [dbo].[PalletMaster] PM ON PR.PalletId=PM.PalletId Where PR.PalletStatus  in (5) --AND PM.LocationId={userId}").FirstOrDefault();
            var dt2 = _context.QueryData<int>(@"select COUNT(*) from [dbo].PalletsByOrderTrans PR JOIN [dbo].[PalletMaster] PM ON PR.PalletId=PM.PalletId Where PR.PalletStatus  in (7)").FirstOrDefault();


            dBOnSite.Ace = Convert.ToString(dt);
            dBOnSite.Supplier = Convert.ToString(dt1);
            dBOnSite.Cipl = Convert.ToString(dt2);

            return dBOnSite;
        }

        public DBTransitCount GetDBPalletCountByTypeInTransit(string userId)
        {
            DBTransitCount dBOnSite = new DBTransitCount();


            var dt = _context.QueryData<int>(@$"select COUNT(*) from [dbo].PalletsByOrderTrans PR JOIN[dbo].[PalletMaster] PM ON PR.PalletId = PM.PalletId Where PR.PalletStatus  in (3) --AND PM.LocationId = {userId}").FirstOrDefault();
            var dt1 = _context.QueryData<int>(@$"select COUNT(*) from [dbo].PalletsByOrderTrans PR JOIN [dbo].[PalletMaster] PM ON PR.PalletId=PM.PalletId Where PR.PalletStatus  in (6) --AND PM.LocationId={userId}").FirstOrDefault();
            var dt2 = _context.QueryData<int>(@$"select COUNT(*) from [dbo].PalletsByOrderTrans PR JOIN [dbo].[PalletMaster] PM ON PR.PalletId=PM.PalletId Where PR.PalletStatus  in (8) --AND PM.LocationId = {userId}").FirstOrDefault();


            dBOnSite.AceToSupplier = Convert.ToString(dt);
            dBOnSite.SupplierToCipl = Convert.ToString(dt1);
            dBOnSite.CiplToAce = Convert.ToString(dt2);

            return dBOnSite;
        }



        public List<DBPalletPart> GetDBPalletCountByTypeMaintenance(string userId)
        {
            var dt = _context.QueryData<DBPalletPart>(@$"select COUNT(distinct PM.PalletId) Count,PalletPartNo from [dbo].PalletsByOrderTrans PR JOIN [dbo].[PalletMaster] PM ON PR.PalletId=PM.PalletId Where PR.PalletStatus  in (11)  AND PM.LocationId = {userId} GROUP BY PalletPartNo").ToList();
            return dt;
        }        

        public List<DBPalletPart> GetDBInTransit_AS(string userId)
        {
            var dt = _context.QueryData<DBPalletPart>(@$"select COUNT(distinct PM.PalletId) Count,PalletPartNo from [dbo].PalletsByOrderTrans PR JOIN [dbo].[PalletMaster] PM ON PR.PalletId=PM.PalletId Where PR.PalletStatus  in (3)   GROUP BY PalletPartNo").ToList();
            return dt;
        }

        public List<DBPalletPart> GetDBInTransit_SC(string userId)
        {
            var dt = _context.QueryData<DBPalletPart>(@$"select COUNT(distinct PM.PalletId) Count,PalletPartNo from [dbo].PalletsByOrderTrans PR JOIN [dbo].[PalletMaster] PM ON PR.PalletId=PM.PalletId Where PR.PalletStatus  in (6)   GROUP BY PalletPartNo").ToList();
            return dt;
        }

        public List<DBPalletPart> GetDBInTransit_CA(string userId)
        {
            var dt = _context.QueryData<DBPalletPart>(@$"select COUNT(distinct PM.PalletId) Count,PalletPartNo from [dbo].PalletsByOrderTrans PR JOIN [dbo].[PalletMaster] PM ON PR.PalletId=PM.PalletId Where PR.PalletStatus  in (8)   GROUP BY PalletPartNo").ToList();
            return dt;
        }        

        public List<DBPalletPartDetails> GetPalletDetailsByPart(string userId, string status,string PalletPartNo)
        {
            var dt = _context.QueryData<DBPalletPartDetails>(@$"select distinct PM.PalletId,PalletPartNo,Model from [dbo].PalletsByOrderTrans PR JOIN [dbo].[PalletMaster] PM ON PR.PalletId=PM.PalletId Where PR.PalletStatus  in ({status}) and PalletPartNo='{PalletPartNo}'").ToList();
            return dt;
        }
    }
}
