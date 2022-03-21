using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LOC.PMS.Application.Interfaces;
using LOC.PMS.Application.Interfaces.IRepositories;
using LOC.PMS.Model;
using Serilog;
using static System.Threading.Tasks.TaskStatus;

namespace LOC.PMS.Application
{
    public class PalletDetailsProvider : IPalletDetailsProvider
    {
        private readonly IPalletRepository _palletRepository;
        private readonly ILogger _logger;

        public PalletDetailsProvider(IPalletRepository palletRepository, ILogger logger)
        {
            _palletRepository = palletRepository;
            _logger = logger;
        }


        public async Task AddOrModifyPallet(PalletDetails palletDetailsRequest)
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
        public async Task<IEnumerable<PalletDetails>> GetPalletDetails(string palletPartNo)
        {
            palletPartNo ??= "ALL";

            try
            {
                _logger.ForContext("palletPartNo", palletPartNo)
                    .Information("Get Pallet by part no.");

                //business logic

                return await _palletRepository.SelectPalletDetails(palletPartNo);
            }
            catch (Exception exception)
            {
                _logger.ForContext("palletPartNo", palletPartNo)
                    .Error(exception, "Exception occurred while trying to get pallet based on part no..");
                await Task.FromException(exception);
            }

            return null;
        }

        public async Task DeletePalletByPartNo(int palletId)
        {
            try
            {
                _logger.ForContext("palletPartNo", palletId)
                    .Information("Delete Pallet request - Start");

                //business logic

                await _palletRepository.DeletePallets(palletId);

                _logger.ForContext("PalletDetailsRequest", palletId)
                    .Information("Delete Pallet request - End");
            }
            catch (Exception exception)
            {
                _logger.ForContext("palletPartNo", palletId)
                    .Error(exception, $"Exception occurred during pallet delete operation for pallet no. - {palletId}");
                await Task.FromException(exception);
            }
        }
    }
}