using System;
using System.Collections.Generic;
using System.Text;

namespace LOC.PMS.Model.DashBoard
{
    public class SupplierInOutFlow
    {
        public SupplierInOutFlowDB supplierInOutFlowDB { get; set; }
        public List<SupplierInOutFlowGrid> supplierInOutFlowGrid { get; set; }

    }

    public class SupplierInOutFlowDB
    {
        public string Received { get; set; }
        public string Dispatched { get; set; }

    }

    public class SupplierInOutFlowGrid
    {
        public string Supplier { get; set; }
        public string PartNo { get; set; }
        public string Qty { get; set; }
        public string Status { get; set; }


    }

}
