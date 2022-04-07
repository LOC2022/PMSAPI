using System;
using System.Collections.Generic;
using System.Text;

namespace LOC.PMS.Model
{
    public class OrderDetails
    {
        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public int NoOfPartsOrdered { get; set; }
        public int OrderQty { get; set; }
        public string VendorName { get; set; }
        public string BillToAddress { get; set; }
        public int PalletId { get; set; }
        public string PalletPartNo { get; set; }
        public string PalletName { get; set; }
        public string PalletStatus { get; set; }
    }
}
