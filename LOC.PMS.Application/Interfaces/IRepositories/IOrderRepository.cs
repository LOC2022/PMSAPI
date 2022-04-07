using System.Collections.Generic;
using System.Threading.Tasks;
using LOC.PMS.Model;

namespace LOC.PMS.Application.Interfaces.IRepositories
{
    public interface IOrderRepository
    {
       
        Task AddDayPlanData(List<DayPlan> order);
        Task<IEnumerable<OrderDetails>> GetOrderDetails(string orderNo);
        Task CancelOrder(string orderNo);
    }
}