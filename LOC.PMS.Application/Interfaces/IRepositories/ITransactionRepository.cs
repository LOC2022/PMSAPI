using LOC.PMS.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LOC.PMS.Application.Interfaces.IRepositories
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<OrderDetails>> GetOrderDetails(OrderDetails orderDetails);
        Task<IEnumerable<DCDetails>> GetDCDetails(string orderNo);
    }
}
