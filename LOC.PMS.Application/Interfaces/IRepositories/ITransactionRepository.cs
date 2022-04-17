﻿using LOC.PMS.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LOC.PMS.Application.Interfaces.IRepositories
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<OrderDetails>> GetHHTOrderDetails(OrderDetails orderDetails);
        Task<IEnumerable<DCDetails>> GetDCDetails(string orderNo, string DCStatus, string UserName);
        Task UpdateScanDetails(List<int> PalletIds, int ScannedQty, string ToStatus);
        Task UpdateScanDetailsForInward(List<int> PalletIds, int ScannedQty, string ToStatus, string OrderNumber);
        Task SaveVehicleDetailsAndUpdateDCStatus(VechicleDetails vechicleDetails, string ToDCStage, string ToPalletStage);
        Task SaveHHTOrderDetails(List<OrderDetails> orderDetails);
    }
}


