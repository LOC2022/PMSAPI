﻿using LOC.PMS.Application.Interfaces;
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
        public async Task<IEnumerable<DayPlan>> GetMonthlyPlanReport(string fromDate, string toDate, int VendorId)
        {
            try
            {
                _logger.ForContext("Select DayPlan Details", fromDate)
                    .Information("Select DayPlan Details request - Start");

                var response = await _reportDetailsRepository.GetMonthlyPlanReport(fromDate, toDate, VendorId);

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
    }
}