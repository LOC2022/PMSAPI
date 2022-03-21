using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LOC.PMS.Application.Interfaces;
using LOC.PMS.Application.Interfaces.IRepositories;
using LOC.PMS.Model;
using Serilog;

namespace LOC.PMS.Application
{
    public class PalletDetailsProvider:IPalletDetailsProvider
    {
        private readonly IPalletRepository _palletRepository;
        private readonly ILogger _logger;

        public PalletDetailsProvider(IPalletRepository palletRepository, ILogger logger)
        {
            _palletRepository = palletRepository;
            _logger = logger;
        }

        public async Task AddDayPlanData(List<DayPlan> order)
        {
            try
            {
                _logger.ForContext("PalletDetailsRequest", order)
                    .Information("Add Pallet request - Start");

                //business logic

                await _palletRepository.AddDayPlanData(order);

                _logger.ForContext("PalletDetailsRequest", order)
                    .Information("Add Pallet request - End");
            }
            catch (Exception exception)
            {
                _logger.ForContext("PalletDetailsRequest", order)
                    .Error(exception, "Exception occurred during pallet insert .");
                await Task.FromException(exception);
            }
        }

        public async Task AddPallet(PalletDetailsRequest palletDetailsRequest)
        {
            try
            {
                _logger.ForContext("PalletDetailsRequest", palletDetailsRequest)
                    .Information("Add Pallet request - Start");

                //business logic

                await _palletRepository.InsertPallets(palletDetailsRequest);

                _logger.ForContext("PalletDetailsRequest", palletDetailsRequest)
                    .Information("Add Pallet request - End");
            }
            catch (Exception exception)
            {
                _logger.ForContext("PalletDetailsRequest", palletDetailsRequest)
                    .Error(exception, "Exception occurred during pallet insert .");
                await Task.FromException(exception);
            }
        }
    }
}