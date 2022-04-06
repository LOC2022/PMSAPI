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

                //business logic

                await _orderRepository.AddDayPlanData(order);

                _logger.ForContext("PalletDetailsRequest", order)
                    .Information("Add Pallet request - End");
            }
            catch (Exception exception)
            {
                _logger.ForContext("Add Day Plan Data", order)
                    .Error(exception, "Exception occurred during Add Day Plan Data insert .");
                await Task.FromException(exception);
            }
        }

       
    }
}