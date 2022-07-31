using System;
using System.Collections.Generic;
using System.Text;

namespace LOC.PMS.Model.DashBoard
{
    public class PalletAgingDetails
    {
        public List<PalletAgingDetailsDB> palletAgingDetailsDB { get; set; }
        public List<PalletAgingDetailsGrid> palletAgingDetailsGrid { get; set; }
    }

    public class PalletAgingDetailsDB
    {
        public string PalletPartNo { get; set; }
        public string Qty { get; set; }
    }
    public class PalletAgingDetailsGrid
    {
        public string PalletPartNo { get; set; }
        public string Qty { get; set; }
        public string VendorName { get; set; }
    }
    public class PalletAgingDetailsByPallet
    {
        public string PalletPartNo { get; set; }
        public string PalletId { get; set; }
        public string Qty { get; set; }
        public string VendorName { get; set; }
        public string Aging { get; set; }
    }

}
