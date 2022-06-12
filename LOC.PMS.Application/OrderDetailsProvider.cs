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
    public class OrderDetailsProvider : IOrdesDetailProvider
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger _logger;

        public OrderDetailsProvider(IOrderRepository orderRepository, ILogger logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }     

        public async Task AddDayPlanData(List<DayPlan> order)
        {
            try
            {
                _logger.ForContext("AddDayPlanData", order)
                    .Information("Add Day Plan Data request - Start");                

                await _orderRepository.AddDayPlanData(order);

                _logger.ForContext("AddDayPlanData", order)
                    .Information("Add Pallet request - End");
            }
            catch (Exception exception)
            {
                _logger.ForContext("Add Day Plan Data", order)
                    .Error(exception, "Exception occurred during Add Day Plan Data insert .");
                await Task.FromException(exception);
            }
        }

        public async Task CancelOrder(string orderNo)
        {
            try
            {
                _logger.ForContext("Cancel Order", orderNo)
                    .Information("CancelOrder request - Start");

                await _orderRepository.CancelOrder(orderNo);

                _logger.ForContext("CancelOrder", orderNo)
                    .Information("Cancel Order request - End");
            }
            catch (Exception exception)
            {
                _logger.ForContext("CancelOrder", orderNo)
                    .Error(exception, "Exception occurred during Cancel Order .");
                await Task.FromException(exception);
            }
        }

        public async Task CreateOrder()
        {
            await _orderRepository.CreateOrder();
        }

        public async Task<IEnumerable<OrderDetails>> GetOrderDetails(string OrderNo)
        {
            
            try
            {
                _logger.ForContext("Select Order Details", OrderNo)
                    .Information("Select Order Details request - Start");

             var  response=  await _orderRepository.GetOrderDetails(OrderNo);

                _logger.ForContext("Select Order Details", OrderNo)
                    .Information("Select Order Details - End");
                return response;
            }
            catch (Exception exception)
            {
                _logger.ForContext("Add Day Plan Data", OrderNo)
                    .Error(exception, "Exception occurred during Select Order Details .");
                await Task.FromException(exception);

            }

            return null;
        }
    }
}