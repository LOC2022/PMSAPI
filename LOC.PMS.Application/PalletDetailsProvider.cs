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


        public async Task ModifyPalletDetails(PalletDetails palletDetailsRequest)
        {
            try
            {
                _logger.ForContext("PalletDetailsRequest", palletDetailsRequest)
                    .Information("Add Pallet request - Start");

                //business logic

                await _palletRepository.ModifyPalletDetails(palletDetailsRequest);

                _logger.ForContext("PalletDetailsRequest", palletDetailsRequest)
                    .Information("Add Pallet request - End");
            }
            catch (Exception exception)
            {
                _logger.ForContext("PalletDetailsRequest", palletDetailsRequest)
                    .Error(exception, "Exception occurred during pallet insert or update.");
                await Task.FromException(exception);
            }
        }

        public async Task ModifyPalletLocation(LocationMaster palletLocation)
        {
            try
            {
                _logger.ForContext("palletLocation", palletLocation)
                    .Information("Add Pallet request - Start");

                //business logic

                await _palletRepository.ModifyPalletLocation(palletLocation);

                _logger.ForContext("palletLocation", palletLocation)
                    .Information("Add Pallet request - End");
            }
            catch (Exception exception)
            {
                _logger.ForContext("palletLocation", palletLocation)
                    .Error(exception, "Exception occurred during pallet Location insert or update.");
                await Task.FromException(exception);
            }

        }

        public async Task<IEnumerable<PalletDetails>> GetPalletDetails(int palletId)
        {
            try
            {
                _logger.ForContext("palletId", palletId)
                    .Information("Get Pallet by part id.");

                //business logic

                return await _palletRepository.SelectPalletDetails(palletId);
            }
            catch (Exception exception)
            {
                _logger.ForContext("palletId", palletId)
                    .Error(exception, "Exception occurred while trying to get pallet based on part id..");
                await Task.FromException(exception);
            }

            return null;
        }

        public async Task<IEnumerable<LocationMaster>> GetPalletLocation(int locationId)
        {
            try
            {
                _logger.ForContext("locationId", locationId)
                    .Information("Get Location by location id.");

                //business logic

                return await _palletRepository.SelectPalletLocation(locationId);
            }
            catch (Exception exception)
            {
                _logger.ForContext("locationId", locationId)
                    .Error(exception, "Exception occurred while trying to get location based on location id..");
                await Task.FromException(exception);
            }

            return null;
        }

        public async Task DeletePalletByPalletId(int palletId)
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

        public async Task DeletePalletLocationById(int locationId)
        {
            try
            {
                _logger.ForContext("locationId", locationId)
                    .Information("Delete Pallet location request - Start");

                //business logic

                await _palletRepository.DeletePalletLocation(locationId);

                _logger.ForContext("locationId", locationId)
                    .Information("Delete Pallet location request - End");
            }
            catch (Exception exception)
            {
                _logger.ForContext("palletPartNo", locationId)
                    .Error(exception, $"Exception occurred during pallet location delete operation for location id. - {locationId}");
                await Task.FromException(exception);
            }
        }
    }
}