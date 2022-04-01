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
        private readonly IPalletDetailsRepository _palletRepository;
        private readonly ILogger _logger;
        private const int DefaultReturnValue = 0;

        public PalletDetailsProvider(IPalletDetailsRepository palletRepository, ILogger logger)
        {
            _palletRepository = palletRepository;
            _logger = logger;
        }


        public async Task<int> ModifyPalletDetails(PalletDetails palletDetailsRequest)
        {
            var returnPalletPartId = DefaultReturnValue;

            try
            {
                _logger.ForContext("PalletDetailsRequest", palletDetailsRequest)
                    .Information("Add Pallet request - Start");

                //business logic

                returnPalletPartId = await _palletRepository.ModifyPalletDetails(palletDetailsRequest);

                _logger.ForContext("PalletDetailsRequest", palletDetailsRequest)
                    .Information("Add Pallet request - End");
                return returnPalletPartId;
            }
            catch (Exception exception)
            {
                _logger.ForContext("PalletDetailsRequest", palletDetailsRequest)
                    .Error(exception, "Exception occurred during pallet insert or update.");
                await Task.FromException(exception);
            }

            return returnPalletPartId;
        }

        public async Task<int> ModifyPalletLocation(LocationMaster palletLocation)
        {
            var returnPalletLocationId = DefaultReturnValue;
            try
            {
                _logger.ForContext("palletLocation", palletLocation)
                    .Information("Add Pallet request - Start");

                //business logic

                returnPalletLocationId =  await _palletRepository.ModifyPalletLocation(palletLocation);

                _logger.ForContext("palletLocation", palletLocation)
                    .Information("Add Pallet request - End");

                return returnPalletLocationId;
            }
            catch (Exception exception)
            {
                _logger.ForContext("palletLocation", palletLocation)
                    .Error(exception, "Exception occurred during pallet Location insert or update.");
                await Task.FromException(exception);
            }

            return returnPalletLocationId;

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

        public async Task DeactivatePalletByPalletId(int palletId)
        {
            try
            {
                _logger.ForContext("palletPartNo", palletId)
                    .Information("Deactivate Pallet request - Start");

                //business logic

                await _palletRepository.DeactivatePallets(palletId);

                _logger.ForContext("PalletDetailsRequest", palletId)
                    .Information("Deactivate Pallet request - End");
            }
            catch (Exception exception)
            {
                _logger.ForContext("palletPartNo", palletId)
                    .Error(exception, $"Exception occurred during pallet Deactivate operation for pallet no. - {palletId}");
                await Task.FromException(exception);
            }
        }

        public async Task DeactivatePalletLocationById(int locationId)
        {
            try
            {
                _logger.ForContext("locationId", locationId)
                    .Information("Deactivate Pallet location request - Start");

                //business logic

                await _palletRepository.DeactivatePalletLocation(locationId);

                _logger.ForContext("locationId", locationId)
                    .Information("Deactivate Pallet location request - End");
            }
            catch (Exception exception)
            {
                _logger.ForContext("palletPartNo", locationId)
                    .Error(exception, $"Exception occurred during pallet location Deactivate operation for location id. - {locationId}");
                await Task.FromException(exception);
            }
        }
    }
}