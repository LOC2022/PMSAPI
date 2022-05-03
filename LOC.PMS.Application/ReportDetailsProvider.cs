using LOC.PMS.Application.Interfaces;
using LOC.PMS.Application.Interfaces.IRepositories;
using LOC.PMS.Model;
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

        public async Task<IEnumerable<DCDetails>> GetDCDetailsReport(string fromDate, string toDate)
        {
            try
            {
                _logger.ForContext("Select DCDetails ", fromDate)
                    .Information("Select DCDetails request - Start");

                var response = await _reportDetailsRepository.GetDCDetailsReport(fromDate, toDate);

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
    }
}
