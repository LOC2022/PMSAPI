using LOC.PMS.Application.Interfaces;
using LOC.PMS.Application.Interfaces.IRepositories;
using LOC.PMS.Model;
using LOC.PMS.Model.Report;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LOC.PMS.Application
{
    public class ReportDetailsProvider : IReportDetailsProvider
    {
        private readonly IReportDetailsRepository _reportDetailsRepository;
        private readonly ILogger _logger;


        public ReportDetailsProvider(IReportDetailsRepository reportDetailsRepository, ILogger logger)
        {
            _reportDetailsRepository = reportDetailsRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<MonthlyPlan>> GetDateWiseOrder(string fromDate, string toDate)
        {
            try
            {
                _logger.ForContext("Select GetDayPlanReport ", fromDate)
                    .Information("Select GetDayPlanReport request - Start");

                var response = await _reportDetailsRepository.GetDateWiseOrder(fromDate, toDate);

                _logger.ForContext("Select GetDayPlanReport", fromDate)
                    .Information("Select GetDayPlanReport - End");
                return response;
            }
            catch (Exception exception)
            {
                _logger.ForContext("GetDayPlanReport", fromDate)
                    .Error(exception, "Exception occurred during Select GetDayPlanReport .");
                await Task.FromException(exception);

            }

            return null;
        }

        public async Task<IEnumerable<OrderDetails>> GetDayPlanReport(string fromDate, string toDate)
        {
            try
            {
                _logger.ForContext("Select GetDayPlanReport ", fromDate)
                    .Information("Select GetDayPlanReport request - Start");

                var response = await _reportDetailsRepository.GetDayPlanReport(fromDate, toDate);

                _logger.ForContext("Select GetDayPlanReport", fromDate)
                    .Information("Select GetDayPlanReport - End");
                return response;
            }
            catch (Exception exception)
            {
                _logger.ForContext("GetDayPlanReport", fromDate)
                    .Error(exception, "Exception occurred during Select GetDayPlanReport .");
                await Task.FromException(exception);

            }

            return null;
        }

        public DBPalletCount GetDBPalletCount(string userId)
        {
            try
            {
                _logger.Information("Pallet part trans details report request - Start");

                var response = _reportDetailsRepository.GetDBPalletCount(userId);

                _logger.Information("Pallet part trans details report request - End");
                return response;
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Exception occurred while pulling pallet part trans report.");

                throw exception;

            }

            return null;
        }

        public DBOnSite GetDBPalletCountByTypeOnSite(string userId)
        {
            try
            {
                _logger.Information("Pallet part trans details report request - Start");

                var response = _reportDetailsRepository.GetDBPalletCountByTypeOnSite(userId);

                _logger.Information("Pallet part trans details report request - End");
                return response;
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Exception occurred while pulling pallet part trans report.");

                throw exception;

            }

        }

        public DBTransitCount GetDBPalletCountByTypeInTransit(string userId)
        {
            try
            {
                _logger.Information("Pallet part trans details report request - Start");

                var response = _reportDetailsRepository.GetDBPalletCountByTypeInTransit(userId);

                _logger.Information("Pallet part trans details report request - End");
                return response;
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Exception occurred while pulling pallet part trans report.");

                throw exception;

            }

        }

        public async Task<IEnumerable<DCDetails>> GetDCDetails(string dCNumber)
        {
            try
            {
                _logger.ForContext("Select DCDetails ", dCNumber)
                    .Information("Select DCDetails request - Start");

                var response = await _reportDetailsRepository.GetDCDetail(dCNumber);

                _logger.ForContext("Select DCDetails  ", dCNumber)
                    .Information("Select DCDetails - End");
                return response;
            }
            catch (Exception exception)
            {
                _logger.ForContext("GetDCDetailsReport", dCNumber)
                    .Error(exception, "Exception occurred during Select DCDetails .");
                await Task.FromException(exception);

            }

            return null;
        }

        public async Task<IEnumerable<DCDetails>> GetDCDetailsReport(string fromDate, string toDate, string UserId)
        {
            try
            {
                _logger.ForContext("Select DCDetails ", fromDate)
                    .Information("Select DCDetails request - Start");

                var response = await _reportDetailsRepository.GetDCDetailsReport(fromDate, toDate, UserId);

                _logger.ForContext("Select DCDetails  ", fromDate)
                    .Information("Select DCDetails - End");
                return response;
            }
            catch (Exception exception)
            {
                _logger.ForContext("GetDCDetailsReport", fromDate)
                    .Error(exception, "Exception occurred during Select DCDetails .");
                await Task.FromException(exception);

            }

            return null;
        }

        public async Task<IEnumerable<InwardReport>> GetInwardReport(int UserId, int PalletStatus)
        {
            try
            {
                _logger.ForContext("Select InwardReport ", UserId)
                    .Information("Select InwardReport request - Start");

                var response = await _reportDetailsRepository.GetInwardReport(UserId, PalletStatus);

                _logger.ForContext("Select InwardReport  ", UserId)
                    .Information("Select InwardReport - End");
                return response;
            }
            catch (Exception exception)
            {
                _logger.ForContext("GetInwardReport", UserId)
                    .Error(exception, "Exception occurred during Select GetInwardReport .");
                await Task.FromException(exception);

            }

            return null;
        }

        public async Task<IEnumerable<InwardReportDetails>> GetInwardReportByDCNumber(string dCNumber, int PalletStatus)
        {
            try
            {
                _logger.ForContext("Select InwardReportDetails ", dCNumber)
                    .Information("Select InwardReportDetails request - Start");

                var response = await _reportDetailsRepository.GetInwardReportByDCNumber(dCNumber, PalletStatus);

                _logger.ForContext("Select InwardReportDetails  ", dCNumber)
                    .Information("Select InwardReportDetails - End");
                return response;
            }
            catch (Exception exception)
            {
                _logger.ForContext("InwardReportDetails", dCNumber)
                    .Error(exception, "Exception occurred during Select InwardReportDetails .");
                await Task.FromException(exception);

            }

            return null;
        }

        public async Task<IEnumerable<InwardReport>> GetInwardReportByPartNumber(int UserId, string partNumber, int PalletStatus)
        {
            try
            {
                _logger.ForContext("Select InwardReportByPartNumber ", partNumber)
                    .Information("Select InwardReportByPartNumber request - Start");

                var response = await _reportDetailsRepository.GetInwardReportByPartNumber(UserId, partNumber, PalletStatus);

                _logger.ForContext("Select InwardReportByPartNumber  ", partNumber)
                    .Information("Select InwardReportByPartNumber - End");
                return response;
            }
            catch (Exception exception)
            {
                _logger.ForContext("InwardReportByPartNumber", partNumber)
                    .Error(exception, "Exception occurred during Select GetInwardReport .");
                await Task.FromException(exception);

            }

            return null;
        }

        public async Task<IEnumerable<DayPlan>> GetMonthlyPlanReport(string fromDate, string toDate, int vendorId)
        {
            try
            {
                _logger.ForContext("Select DayPlan Details", fromDate)
                    .Information("Select DayPlan Details request - Start");

                var response = await _reportDetailsRepository.GetMonthlyPlanReport(fromDate, toDate, vendorId);

                _logger.ForContext("Select DayPlan Details ", fromDate)
                    .Information("Select DayPlan Details- End");
                return response;
            }
            catch (Exception exception)
            {
                _logger.ForContext("GetMonthlyPlanReport", fromDate)
                    .Error(exception, "Exception occurred during Select DayPlan Details .");
                await Task.FromException(exception);

            }

            return null;
        }



        public async Task<IEnumerable<OrderDetailsByDate>> GetOrderDetailsByDate(string OrderDate)
        {
            try
            {
                _logger.ForContext("Select DayPlan Details", OrderDate)
                    .Information("Select DayPlan Details request - Start");

                var response = await _reportDetailsRepository.GetOrderDetailsByDate(OrderDate);

                _logger.ForContext("Select DayPlan Details ", OrderDate)
                    .Information("Select DayPlan Details- End");
                return response;
            }
            catch (Exception exception)
            {
                _logger.ForContext("GetMonthlyPlanReport", OrderDate)
                    .Error(exception, "Exception occurred during Select DayPlan Details .");
                await Task.FromException(exception);

            }

            return null;
        }

        public async Task<IEnumerable<PalletsByOrderTransReport>> GetPalletOrderTransReport(string palletId)
        {
            try
            {
                _logger.Information("Pallet by order trans details report request - Start");

                var response = await _reportDetailsRepository.GetPalletOrderTransReport(palletId);

                _logger.Information("Pallet by order trans details report request - End");
                return response;
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Exception occurred while pulling pallet by order trans report.");
                await Task.FromException(exception);

            }

            return null;
        }

        public async Task<IEnumerable<PalletsPartTransReport>> GetPalletPartTransReport(int userId, string palletPartNo)
        {
            try
            {
                _logger.Information("Pallet part trans details report request - Start");

                var response = await _reportDetailsRepository.GetPalletPartTransReport(userId, palletPartNo);

                _logger.Information("Pallet part trans details report request - End");
                return response;
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Exception occurred while pulling pallet part trans report.");
                await Task.FromException(exception);

            }

            return null;
        }

        public async Task<IEnumerable<PalletReportSelection>> GetPalletReportSelection(int UserId, string PalletStatus, string ModelNo)
        {
            try
            {
                _logger.Information("Pallet by order trans details report request - Start");

                var response = await _reportDetailsRepository.GetPalletReportSelection(UserId, PalletStatus, ModelNo);

                _logger.Information("Pallet by order trans details report request - End");
                return response;
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Exception occurred while pulling pallet by order trans report.");
                await Task.FromException(exception);

            }

            return null;
        }

        public async Task<IEnumerable<WareHouseStock>> WareHouseStockDetails(string Status)
        {

            try
            {
                _logger.Information("WareHouseTransitDetails report request - Start");

                var response = await _reportDetailsRepository.WareHouseStockDetails(Status);

                _logger.Information("WareHouseTransitDetails report request - End");
                return response;
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Exception occurred while pulling WareHouseTransitDetails report.");
                await Task.FromException(exception);

            }

            return null;
        }

        public async Task<IEnumerable<InwardReport>> WareHouseTransitDetails(string Status, string DCNUmber, string PalletPartNo)
        {
            try
            {
                _logger.ForContext("Select WareHouseTransitDetails ", Status)
                    .Information("Select WareHouseTransitDetails request - Start");

                var response = await _reportDetailsRepository.WareHouseTransitDetails(Status, DCNUmber, PalletPartNo);

                _logger.ForContext("Select WareHouseTransitDetails  ", Status)
                    .Information("Select WareHouseTransitDetails - End");
                return response;
            }
            catch (Exception exception)
            {
                _logger.ForContext("WareHouseTransitDetails", Status)
                    .Error(exception, "Exception occurred during Select WareHouseTransitDetails .");
                await Task.FromException(exception);

            }

            return null;
        }

        public List<DBPalletPart> GetDBPalletCountByTypeMaintenance(string userId)
        {
            try
            {
                _logger.Information("Pallet part trans details report request - Start");

                var response = _reportDetailsRepository.GetDBPalletCountByTypeMaintenance(userId);

                _logger.Information("Pallet part trans details report request - End");
                return response;
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Exception occurred while pulling pallet part trans report.");

                throw exception;

            }
        }

        public List<DBPalletPart> GetDBInTransit_AS(string userId)
        {
            try
            {
                _logger.Information("Pallet part trans details report request - Start");

                var response = _reportDetailsRepository.GetDBInTransit_AS(userId);

                _logger.Information("Pallet part trans details report request - End");
                return response;
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Exception occurred while pulling pallet part trans report.");

                throw exception;

            }
        }

        public List<DBPalletPart> GetDBInTransit_SC(string userId)
        {
            try
            {
                _logger.Information("Pallet part trans details report request - Start");

                var response = _reportDetailsRepository.GetDBInTransit_SC(userId);

                _logger.Information("Pallet part trans details report request - End");
                return response;
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Exception occurred while pulling pallet part trans report.");

                throw exception;

            }
        }

        public List<DBPalletPart> GetDBInTransit_CA(string userId)
        {
            try
            {
                _logger.Information("Pallet part trans details report request - Start");

                var response = _reportDetailsRepository.GetDBInTransit_CA(userId);

                _logger.Information("Pallet part trans details report request - End");
                return response;
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Exception occurred while pulling pallet part trans report.");

                throw exception;

            }
        }

        public List<DBPalletPartDetails> GetPalletDetailsByPart(string userId, string status, string PalletPartNo)
        {
            try
            {
                _logger.Information("Pallet part trans details report request - Start");

                var response = _reportDetailsRepository.GetPalletDetailsByPart(userId, status, PalletPartNo);

                _logger.Information("Pallet part trans details report request - End");
                return response;
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Exception occurred while pulling pallet part trans report.");

                throw exception;

            }
        }
    }
}
