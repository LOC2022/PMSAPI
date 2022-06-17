using LOC.PMS.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LOC.PMS.Application.Interfaces
{
    public interface ITransactionDetailsProvider
    {
        Task<IEnumerable<OrderDetails>> GetHHTOrderDetails(OrderDetails orderDetails);
        Task<IEnumerable<DCDetails>> GetDCDetails(string orderNo, string DCStatus, string UserName);
        Task UpdateScanDetails(List<string> PalletIds, int ScannedQty, string ToStatus, string OrderNumber, int VendorId);
        Task SaveVehicleAndUpdateStatus(VechicleDetails vechicleDetails, string ToDCStage, string ToPalletStage);
        Task SaveHHTOrderDetails(List<OrderDetails> orderDetails);
        Task UpdateHHTDispatchDetails(List<OrderDetails> orderDetails);
        Task<IEnumerable<DCDetails>> GetDCDetailsByPallet(string palletId, int DCStatus, string dCNo);
        Task<IEnumerable<PalletDetails>> GetPalletPartNo(string PalletPartNo);
        Task UpdateHHTInwardDetails(List<OrderDetails> orderDetails);
        Task<IEnumerable<DCDetails>> GetPalletForPutAway();
        Task UpdatePalletWriteCount(string PalletId);
        Task<string> SwapPallets(string oldPalletId, string newPalletId, string OrderNo);
    }
}
