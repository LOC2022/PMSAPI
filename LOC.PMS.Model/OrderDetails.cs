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
        public string PalletId { get; set; }
        public string PalletPartNo { get; set; }
        public string PalletName { get; set; }
        public string PalletStatus { get; set; }
        public int Stage { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; }

    }


    public class DCDetails
    {
        public string OrderNo { get; set; }
        public string DCNo { get; set; }
        public string DCStatus { get; set; }
        public int DCType { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ScannedDate { get; set; }
        public int NoOfPartsOrdered { get; set; }
        public int OrderQty { get; set; }
        public string VendorName { get; set; }
        public string BillToAddress { get; set; }
        public string PalletId { get; set; }
        public string PalletPartNo { get; set; }
        public string PalletName { get; set; }
        public string PalletStatus { get; set; }
        public string Status { get; set; }

    }
}
