using LOC.PMS.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LOC.PMS.Application.Interfaces
{
    public interface ITransactionDetailsProvider
    {
        Task<IEnumerable<OrderDetails>> GetOrderDetails(OrderDetails orderNo);
        Task<IEnumerable<DCDetails>> GetDCDetails(string orderNo, string DCStatus, string UserName);
        Task UpdateScanDetails(List<int> PalletIds, int ScannedQty, string ToStatus, string OrderNumber);
        Task SaveVehicleAndUpdateStatus(VechicleDetails vechicleDetails, string ToDCStage, string ToPalletStage);
    }
}
