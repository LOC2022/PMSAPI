using LOC.PMS.Application.Interfaces;
using LOC.PMS.Application.Interfaces.IRepositories;
using LOC.PMS.Model;
using LOC.PMS.Model.DashBoard;
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


            var dt = _context.QueryData<int>(@$"select COUNT(*) from [dbo].PalletsByOrderTrans PR JOIN[dbo].[PalletMaster] PM ON PR.PalletId = PM.PalletId Where PR.PalletStatus  in (4) --AND PM.LocationId = {userId}").FirstOrDefault();
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

        public List<DBPalletPartDetails> GetPalletDetailsByPart(string userId, string status, string PalletPartNo)
        {
            var dt = _context.QueryData<DBPalletPartDetails>(@$"select distinct PM.PalletId,PalletPartNo,Model from [dbo].PalletsByOrderTrans PR JOIN [dbo].[PalletMaster] PM ON PR.PalletId=PM.PalletId Where PR.PalletStatus  in ({status}) and PalletPartNo='{PalletPartNo}'").ToList();
            return dt;
        }

        public SupplierInOutFlow GetSupplierInOutFlowDB(string vendorId, string startDate, string endDate)
        {
            string Condition = "";
            if (!string.IsNullOrEmpty(vendorId))
            {
                Condition = $" and VendorId={vendorId}";
            }
            if (!string.IsNullOrEmpty(startDate))
            {
                Condition = $" and CreatedDate between '{startDate}' and '{endDate}' ";
            }
            var Obj = new SupplierInOutFlow();
            Obj.supplierInOutFlowDB = new SupplierInOutFlowDB();
            Obj.supplierInOutFlowDB.Dispatched = _context.QueryData<string>(@$"select COUNT(PalletId) from [dbo].[DeliveryChallanTrans] where DCType=1 {Condition}").FirstOrDefault();
            Obj.supplierInOutFlowDB.Received = _context.QueryData<string>(@$"select COUNT(PalletId) from [dbo].[DeliveryChallanTrans] where DCType=1 {Condition}").FirstOrDefault();
            Obj.supplierInOutFlowGrid = _context.QueryData<SupplierInOutFlowGrid>(@$"select COUNT(DC.PalletId) QTY,PM.PalletPartNo PartNo,VM.VendorName Supplier,'Received' Status from [dbo].[DeliveryChallanTrans] DC
                                        Join PalletMaster PM on DC.PalletId=PM.PalletId
                                        JOIN VendorMaster VM ON VM.VendorId=DC.VendorId
                                        where DCType=1 {Condition}
                                        Group BY PM.PalletPartNo,VM.VendorName
                                        UNION ALL
                                        select COUNT(DC.PalletId) QTY,PM.PalletPartNo PartNo,VM.VendorName Supplier,'DisPatched' Status from [dbo].[DeliveryChallanTrans] DC
                                        Join PalletMaster PM on DC.PalletId=PM.PalletId
                                        JOIN VendorMaster VM ON VM.VendorId=DC.VendorId
                                        where DCType=2 {Condition}
                                        Group BY PM.PalletPartNo,VM.VendorName
                                        ORDER BY Status").ToList();
            return Obj;
        }

        public PlannedDBData GetPlannedDBDetails(string startDate, string endDate)
        {
            string Condition = "";
            string Condition1 = "";
            if (!string.IsNullOrEmpty(startDate))
            {
                Condition = $" and CreatedDate between '{startDate}' and '{endDate}' ";
                Condition1 = $" and OrderCreatedDate between '{startDate}' and '{endDate}' ";
            }
            PlannedDBData plannedDBData = new PlannedDBData();
            plannedDBData.Planned = _context.QueryData<int>(@$"select ISNULL(SUM(OrderQty),0) from Orders where PalletAssignedFlag=0  {Condition1}").FirstOrDefault();
            plannedDBData.Actual = _context.QueryData<int>(@$"select ISNULL(COUNT(PalletId),0) from DeliveryChallanTrans where DCStatus=1  {Condition}").FirstOrDefault();
            plannedDBData.Missed = Convert.ToInt32(plannedDBData.Planned) - Convert.ToInt32(plannedDBData.Actual);
            return plannedDBData;
        }

        public TripDetails GetTripDBDetails(string startDate, string endDate)
        {
            string Condition = "";

            if (!string.IsNullOrEmpty(startDate))
            {
                Condition = $" and CAST (TD.ModifiedDate as Date) between '{startDate}' and '{endDate}' ";
            }

            var Obj = new TripDetails();

            Obj.tripDetailsDB = _context.QueryData<TripDetailsDB>(@$"
                select ISNULL(SUM(Trip),0) as Value,'WHToSupplier' Flag from ( 
                select COUNT(Distinct VehicleNo) Trip,CAST (TD.ModifiedDate as Date) DLDate  from DeliveryChallanTrans  DC
                JOIN [dbo].[TransportDetails] TD on DC.DCNo=TD.DCNo 
                where DCType=1 {Condition}
                Group By CAST (TD.ModifiedDate as Date)
                ) a
                UNION ALL
                select ISNULL(SUM(Trip),0) as Value,  'SupplierToCIPL' Flag from (
                select COUNT(Distinct VehicleNo) Trip,CAST (TD.ModifiedDate as Date) DLDate  from DeliveryChallanTrans  DC
                JOIN [dbo].[TransportDetails] TD on DC.DCNo=TD.DCNo 
                where DCType=2 {Condition}
                Group By CAST (TD.ModifiedDate as Date)
                ) b
                UNION ALL
                select ISNULL(SUM(Trip),0) as Value,  'CIPLToWH' Flag from (
                select COUNT(Distinct VehicleNo) Trip,CAST (TD.ModifiedDate as Date) DLDate from DeliveryChallanTrans  DC
                JOIN [dbo].[TransportDetails] TD on DC.DCNo=TD.DCNo 
                where DCType=3 {Condition}
                Group By CAST (TD.ModifiedDate as Date)
                ) c ").ToList();

            Obj.tripDetailsGrid = _context.QueryData<TripDetailsGrid>(@$"
                select CAST (TD.ModifiedDate as Date) DCDate,DC.DCNo,COUNT(PalletId) Qty ,  'WHToSupplier' Flag from DeliveryChallanTrans  DC
                JOIN [dbo].[TransportDetails] TD on DC.DCNo=TD.DCNo 
                where DCType=1 {Condition}
                Group By CAST (TD.ModifiedDate as Date),DC.DCNo
                UNION ALL
                select CAST (TD.ModifiedDate as Date) DCDate,DC.DCNo,COUNT(PalletId) Qty,  'SupplierToCIPL' Flag  from DeliveryChallanTrans  DC
                JOIN [dbo].[TransportDetails] TD on DC.DCNo=TD.DCNo 
                where DCType=2 {Condition}
                Group By CAST (TD.ModifiedDate as Date),DC.DCNo
                UNION ALL
                select CAST (TD.ModifiedDate as Date) DCDate,DC.DCNo,COUNT(PalletId) Qty,  'CIPLToWH' Flag  from DeliveryChallanTrans  DC
                JOIN [dbo].[TransportDetails] TD on DC.DCNo=TD.DCNo 
                where DCType=3 {Condition}
                Group By CAST (TD.ModifiedDate as Date),DC.DCNo
                Order by DCDate ").ToList();

            return Obj;
        }

        public PalletAgingDetails GetPalletAgingDB(string VendorId, string Aging)
        {
            string condition = "";

            if (!string.IsNullOrEmpty(VendorId))
            {
                condition = $" and DC.VendorId={VendorId}";
            }
            if (string.IsNullOrEmpty(Aging))
            {
                Aging = "10";
            }
            var Obj = new PalletAgingDetails();
            Obj.palletAgingDetailsDB = _context.QueryData<PalletAgingDetailsDB>($@"
                    select PM.PalletPartNo,COUNT(DC.PalletId) QTY, DATEDIFF(DAY, DC.CreatedDate, GETDATE()) AS Aging from DeliveryChallanTrans DC
                    Join PalletMaster PM On PM.PalletId=DC.PalletId
                    where PM.Availability=5 {condition}
                    GROUP BY PM.PalletPartNo,DATEDIFF(DAY, DC.CreatedDate, GETDATE())
                    having DATEDIFF(DAY, DC.CreatedDate, GETDATE())>={Aging}
                    ").ToList();
            Obj.palletAgingDetailsGrid = _context.QueryData<PalletAgingDetailsGrid>($@"
            select PM.PalletPartNo,COUNT(DC.PalletId) QTY,VM.VendorName, DATEDIFF(DAY, DC.CreatedDate, GETDATE()) AS Aging from DeliveryChallanTrans DC
            Join VendorMaster VM on VM.VendorId = DC.VendorId
            Join PalletMaster PM On PM.PalletId=DC.PalletId
            where PM.Availability=5 {condition}
            GROUP BY PM.PalletPartNo,VM.VendorName,DATEDIFF(DAY, DC.CreatedDate, GETDATE())
            having DATEDIFF(DAY, DC.CreatedDate, GETDATE())>={Aging}
            ").ToList();

            return Obj;
        }

        public List<DCDetails> GetDcNoForManual(string vendorId, string flag)
        {
            return _context.QueryData<DCDetails>(@$"select distinct DCNo from [dbo].[DeliveryChallanTrans] where  VendorId={vendorId}  and DCStatus IN (2,4,6)").ToList();
        }

        public List<DCDetails> GetDcDetailsForInward(string DCNo)
        {
            return _context.QueryData<DCDetails>(@$"select distinct PM.PalletPartNo,DC.PalletId,VM.VendorName from [dbo].[DeliveryChallanTrans] DC
            JOIN PalletMaster PM on DC.PalletId=PM.PalletId
            JOIN VendorMaster VM on VM.VendorId=DC.VendorId
            where DC.DCStatus IN (2,4,6) and
            DC.DCNo='{DCNo}'").ToList();
        }

        public void SaveInwardDetails(List<string> lstPallets, string OrderNo)
        {

            var VendorId = _context.QueryData<string>($"select VendorId from [DeliveryChallanTrans] where DCNo='{OrderNo}'").FirstOrDefault();
            var Pallets = string.Join("','", lstPallets.ToArray());


            if (VendorId.ToString() == "13")
            {
                string UpdatePalletQry = @$"UPDATE PalletMaster SET availability=1 WHERE PalletId IN ('{Pallets}') ";
                _context.ExecuteSql(UpdatePalletQry);
                string UpdateDCQry = $"UPDATE DeliveryChallanTrans SET DCStatus=DCStatus+1,Isactive=0 WHERE DCNo='{OrderNo}' and PalletId IN ('{Pallets}')";
                _context.ExecuteSql(UpdateDCQry);
            }
            else
            {
                string UpdatePalletQry = @$"UPDATE PalletsByOrderTrans SET PalletStatus=PalletStatus+1, ModifiedDate = GETDATE() WHERE PalletId IN ('{Pallets}') 
            and OrderNo In (select OrderNo from DeliveryChallanTrans where DCNo='{OrderNo}') ";
                _context.ExecuteSql(UpdatePalletQry);
                string UpdateDCQry = $"UPDATE DeliveryChallanTrans SET DCStatus=DCStatus+1,Isactive=0 WHERE DCNo='{OrderNo}' and PalletId IN ('{Pallets}')";
                _context.ExecuteSql(UpdateDCQry);
            }

        }

        public string ValidatePalletId(string palletId)
        {
            return _context.QueryData<string>(@$"select COUNT(distinct PM.PalletId) from  PalletMaster PM WHERE PM.PalletId='{palletId}'").FirstOrDefault();
        }

        public void SaveDispatchDetails(List<DCDetails> lstPallets, string vendorId)
        {
            string DCNo = $"DC{DateTime.Now.ToString("ddMMyyyyHHmmss")}";
            string DCType = vendorId == "14" ? "3" : "2";
            string DCStatus = vendorId == "14" ? "5" : "3";
            string _vendorId = vendorId == "14" ? "13" : vendorId;

            foreach (var detail in lstPallets)
            {
                string UpdateDCQry = @$"INSERT INTO [DeliveryChallanTrans](OrderNo,DCNo,PalletId,VendorId,DCType,DCStatus,CreatedDate,CreatedBy,Isactive)
                Values('{DCNo}','{DCNo}','{detail.PalletId}',{vendorId},{DCType},{DCStatus},'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{vendorId}',1)";
                _context.ExecuteSql(UpdateDCQry);
            }




        }

        public List<Orders> GetOrderNoForManual()
        {
            return _context.QueryData<Orders>(@$"SELECT distinct OrderNo FROM Orders O WHERE o.OrderStatusId = 1;").ToList();
        }

        public List<DCDetails> GetOrderDetailsForDispatch(string orderNo)
        {
            return _context.QueryData<DCDetails>(@$"	select DISTINCT O.OrderNo,PO.PalletId,PM.PalletPartNo,VM.VendorName from PalletsByOrderTrans PO
							JOIN Orders O On PO.OrderNo=O.OrderNo
							JOIN PalletMaster PM on PM.palletId=Po.PalletId
							Join VendorMaster VM On VM.VendorId=O.VendorId
							where O.OrderNo='{orderNo}'").ToList();
        }

        public void GenerateDCForManual(List<string> lstPallets, string orderNo)
        {
            string PalletIds = "";
            if (lstPallets.Count > 0)
                PalletIds = string.Join("','", lstPallets.Select(x => x.ToString()));

            string UpdatePalletQry = $"UPDATE PalletsByOrderTrans SET PalletStatus={(int)PalletStatus.Picked} WHERE PalletId IN ('{PalletIds}') AND OrderNo='{orderNo}';";
            UpdatePalletQry += $"UPDATE Orders SET OrderStatusId={(int)OrderStatus.InTransit} WHERE  OrderNo='{orderNo}';";
            UpdatePalletQry += @$"INSERT INTO [dbo].[DeliveryChallanTrans]
           ([DCNo],[OrderNo],[PalletId],[VendorId],[DCType],[DCStatus],[CreatedDate],[CreatedBy],[IsActive])
			select DISTINCT 'DC{DateTime.Now.ToString("ddMMyyyyHHmmss")}',PT.OrderNo,PalletId,O.VendorId,1,1,GETDATE(),'13',1 from 
			PalletsByOrderTrans PT  
			JOIN Orders O on O.OrderNo=PT.OrderNo where PalletId IN ('{PalletIds}') AND PT.OrderNo='{orderNo}';";

            _context.ExecuteSql(UpdatePalletQry);
        }

        public List<DCDetails> GetOrderNoForDispatch()
        {
            return _context.QueryData<DCDetails>(@$"select Distinct O.OrderNo from Orders O
							Join PalletsByOrderTrans PO On O.OrderNo=PO.OrderNo
							Where PalletStatus=3").ToList();
        }

        public void DispatchOrder(List<string> lstPallets, string orderNo)
        {
            string PalletIds = "";
            if (lstPallets.Count > 0)
            {
                PalletIds = string.Join("','", lstPallets.Select(x => x.ToString()));
                string UpdatePalletQry = $"UPDATE PalletsByOrderTrans SET PalletStatus={(int)PalletStatus.FixedRead} WHERE PalletId IN ('{PalletIds}') AND OrderNo='{orderNo}';";
                _context.ExecuteSql(UpdatePalletQry);
            }
        }


    }
}
