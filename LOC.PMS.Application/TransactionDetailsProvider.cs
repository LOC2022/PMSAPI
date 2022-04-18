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
    public class TransactionDetailsProvider : ITransactionDetailsProvider
    {

        private readonly ITransactionRepository _transactionRepository;
        private readonly ILogger _logger;
        private const int DefaultReturnValue = 0;

        public TransactionDetailsProvider(ITransactionRepository transactionRepository, ILogger logger)
        {
            _transactionRepository = transactionRepository;
            _logger = logger;
        }


        public async Task<IEnumerable<OrderDetails>> GetHHTOrderDetails(OrderDetails orderDetails)
        {
            try
            {
                _logger.ForContext("Select Order Details for HHT", orderDetails)
                    .Information("Select Order Details for HHT request - Start");

                var response = await _transactionRepository.GetHHTOrderDetails(orderDetails);

                _logger.ForContext("Select Order Details for HHT", orderDetails)
                    .Information("Select Order Details for HHT- End");
                return response;
            }
            catch (Exception exception)
            {
                _logger.ForContext("GetHHTOrderDetails", orderDetails)
                    .Error(exception, "Exception occurred during Select Order Details for HHT.");
                await Task.FromException(exception);

            }

            return null;
        }

        public async Task<IEnumerable<DCDetails>> GetDCDetails(string orderNo, string DCStatus, string UserName)
        {
            try
            {
                _logger.ForContext("Select Get DC Details", orderNo)
                    .Information("Select  Get DC Details request - Start");

                var response = await _transactionRepository.GetDCDetails(orderNo, DCStatus, UserName);

                _logger.ForContext("Select  Get DC Details", orderNo)
                    .Information("Select  Get DC Details - End");
                return response;
            }
            catch (Exception exception)
            {
                _logger.ForContext("GetOrderDetails", orderNo)
                    .Error(exception, "Exception occurred during Select Order Details .");
                await Task.FromException(exception);

            }

            return null;
        }

        public async Task SaveVehicleAndUpdateStatus(VechicleDetails vechicleDetails, string ToDCStage, string ToPalletStage)
        {
            try
            {
                _logger.ForContext("saving vechicle details & change DC and pallet", vechicleDetails)
                    .Information("saving vechicle details - Start");

                //business logic

                await _transactionRepository.SaveVehicleDetailsAndUpdateDCStatus(vechicleDetails, ToDCStage, ToPalletStage);

                _logger.ForContext("saving vechicle details", vechicleDetails)
                    .Information("saving vechicle details & change DC and pallet - End");
            }
            catch (Exception exception)
            {
                _logger.ForContext("vechicleDetails", vechicleDetails)
                    .Error(exception, $"saving vechicle details & change DC and pallet. - {vechicleDetails}");
                await Task.FromException(exception);
            }
        }

        public async Task UpdateScanDetails(List<string> PalletIds, int ScannedQty, string ToStatus, string OrderNumber, int VendorId)
        {
            try
            {
                _logger.ForContext("palletIds", PalletIds)
                    .Information("Pallet Scan - Start");
                //business logic
                if (OrderNumber != null)
                {
                    await _transactionRepository.UpdateScanDetailsForInward(PalletIds, ScannedQty, ToStatus, OrderNumber);
                }
                else
                {
                    await _transactionRepository.UpdateScanDetails(PalletIds, ScannedQty, ToStatus, VendorId);
                }

                _logger.ForContext("PalletDetailsRequest", PalletIds)
                    .Information("Pallet Scan - End");
            }
            catch (Exception exception)
            {
                _logger.ForContext("palletIds", PalletIds)
                    .Error(exception, $"Exception occurred during pallet scan. - {PalletIds}");
                await Task.FromException(exception);
            }
        }

        public async Task SaveHHTOrderDetails(List<OrderDetails> orderDetails)
        {
            try
            {
                _logger.ForContext("palletIds", orderDetails)
                    .Information("Pallet Scan - Start");
                //business logic
                if (orderDetails.Count > 0)
                {
                    await _transactionRepository.SaveHHTOrderDetails(orderDetails);
                }


                _logger.ForContext("PalletDetailsRequest", orderDetails)
                    .Information("Pallet Scan - End");
            }
            catch (Exception exception)
            {
                _logger.ForContext("palletIds", orderDetails)
                    .Error(exception, $"Exception occurred during pallet scan. - {orderDetails}");
                await Task.FromException(exception);
            }
        }
    }
}
