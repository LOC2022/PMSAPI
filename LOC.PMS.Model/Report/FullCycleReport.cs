using System;
using System.Collections.Generic;
using System.Text;

namespace LOC.PMS.Model.Report
{
    public class FullCycleReport
    {
        public string PalletId { get; set; }
        public string WarehouseDC { get; set; }
        public string WarehouseDispatch { get; set; }
        public string WHRefNo { get; set; }
        public string WHDispatchTo { get; set; }
        public string SupplierDC { get; set; }
        public string SupplierDispatch { get; set; }
        public string SupplierRefNo { get; set; }
        public string SupplierDispatchTo { get; set; }
        public string CIPLDCNo { get; set; }
        public string CIPLDispatch { get; set; }
        public string CIPLRefNo { get; set; }
        public string CIPLDispatchTo { get; set; }

        //PalletId	WarehouseDC	WarehouseDispatch	WHRefNo	WHDispatchTo	SupplierDC	SupplierDispatch	SupplierRefNo	SupplierDispatchTo	CIPLDCNo	CIPLDispatch	CIPLRefNo	CIPLDispatchTo

    }
}
