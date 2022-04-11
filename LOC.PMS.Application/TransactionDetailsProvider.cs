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


        public async Task<IEnumerable<OrderDetails>> GetOrderDetails(OrderDetails orderDetails)
        {
            try
            {
                _logger.ForContext("Select Order Details", orderDetails)
                    .Information("Select Order Details request - Start");

                var response = await _transactionRepository.GetOrderDetails(orderDetails);

                _logger.ForContext("Select Order Details", orderDetails)
                    .Information("Select Order Details - End");
                return response;
            }
            catch (Exception exception)
            {
                _logger.ForContext("GetOrderDetails", orderDetails)
                    .Error(exception, "Exception occurred during Select Order Details .");
                await Task.FromException(exception);

            }

            return null;
        }

        public async Task<IEnumerable<DCDetails>> GetDCDetails(string orderNo)
        {
            try
            {
                _logger.ForContext("Select Get DC Details", orderNo)
                    .Information("Select  Get DC Details request - Start");

                var response = await _transactionRepository.GetDCDetails(orderNo);

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
    }
}
