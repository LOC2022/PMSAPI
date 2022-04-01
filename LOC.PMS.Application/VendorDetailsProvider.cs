using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LOC.PMS.Application.Interfaces;
using LOC.PMS.Application.Interfaces.IRepositories;
using LOC.PMS.Model;
using Serilog;

namespace LOC.PMS.Application
{
    public class VendorDetailsProvider : IVendorDetailsProvider
    {
        private readonly IVendorDetailsRepository _vendorDetailsRepository;
        private readonly ILogger _logger;
        private const int DefaultReturnValue = 0;

        public VendorDetailsProvider(IVendorDetailsRepository vendorDetailsRepository, ILogger logger)
        {
            _vendorDetailsRepository = vendorDetailsRepository;
            _logger = logger;
        }

        public async Task<int> ModifyVendorDetails(VendorMaster vendorDetailsRequest)
        {
            var returnVendorId = DefaultReturnValue;

            try
            {
                _logger.ForContext("vendorDetailsRequest", vendorDetailsRequest)
                    .Information("Add or Modify vendor details - Start");

                //business logic

                returnVendorId = await _vendorDetailsRepository.ModifyVendorDetails(vendorDetailsRequest);

                _logger.ForContext("vendorDetailsRequest", vendorDetailsRequest)
                    .Information("Add or Modify vendor details - End");
            }
            catch (Exception exception)
            {
                _logger.ForContext("vendorDetailsRequest", vendorDetailsRequest)
                    .Error(exception, "Exception occurred during pallet insert or update.");
                await Task.FromException(exception);
            }

            return returnVendorId;
        }

        public async Task<IEnumerable<VendorMaster>> GetVendorDetails(int vendorId)
        {
            try
            {
                _logger.ForContext("vendorId", vendorId)
                    .Information("Get vendor by vendor id.");

                //business logic

                return await _vendorDetailsRepository.SelectVendorDetails(vendorId);
            }
            catch (Exception exception)
            {
                _logger.ForContext("vendorId", vendorId)
                    .Error(exception, "Exception occurred while trying to get pallet based on part id..");
                await Task.FromException(exception);
            }

            return null;
        }

        public async Task DeactivateVendorById(int vendorId)
        {
            try
            {
                _logger.ForContext("vendorId", vendorId)
                    .Information("Deactivate vendor request - Start");

                //business logic

                await _vendorDetailsRepository.DeactivateVendorById(vendorId);

                _logger.ForContext("vendorId", vendorId)
                    .Information("Deactivate vendor request - End");
            }
            catch (Exception exception)
            {
                _logger.ForContext("vendorId", vendorId)
                    .Error(exception, $"Exception occurred during vendor details Deactivate operation for vendor id. - {vendorId}");
                await Task.FromException(exception);
            }
        }
    }
}