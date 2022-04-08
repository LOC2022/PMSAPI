using LOC.PMS.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LOC.PMS.Application.Interfaces
{
    public interface IOrdesDetailProvider
    {
        Task AddDayPlanData(List<DayPlan> order);
        Task<IEnumerable<OrderDetails>> GetOrderDetails(string palletId);
        Task CancelOrder(string orderNo);
    }
}
