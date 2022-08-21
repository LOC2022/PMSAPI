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
        public string Location { get; set; }

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
        public string Dimensions { get; set; }
        public string Rate { get; set; }
        public string VehicleNo { get; set; }
        public string DriverName { get; set; }
        public string DriverPhoneNo { get; set; }
        public string ReferenceNo { get; set; }
        public string GSTNo { get; set; }
        public string CreatedBy { get; set; }
        public string key { get; set; }


    }

    public class MonthlyPlan
    {
        public string OrderDate { get; set; }
        public int Supplier { get; set; }
        public int PalletQty { get; set; }
    }


    public class OrderDetailsByDate
    {
        public string OrderNo { get; set; }
        public string OrderDate { get; set; }
        public string DispatchDate { get; set; }
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
        public string Location { get; set; }
        public string Shortage { get; set; }

    }

    public class DBPalletCount
    {
        public string OnSite { get; set; }
        public string Maintenance { get; set; }
        public string InTransit { get; set; }
    }

    public class DBTransitCount
    {
        public string CiplToAce { get; set; }
        public string AceToSupplier { get; set; }
        public string SupplierToCipl { get; set; }
    }

    public class DBOnSite
    {
        public string Cipl { get; set; }
        public string Ace { get; set; }
        public string Supplier { get; set; }
    }

    public class DBPalletPart
    {
        public string PalletPartNo { get; set; }
        public string Count { get; set; }
    }

    public class DBPalletPartDetails
    {
        public string PalletPartNo { get; set; }
        public string PalletId { get; set; }
        public string Model { get; set; }
    }


}
