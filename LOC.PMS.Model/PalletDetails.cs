using System;

namespace LOC.PMS.Model
{
    public class PalletDetails
    {
        public int PalletId { get; set; }

        public string PalletPartNo { get; set; }

        public string PalletName { get; set; }

        public int PalletWeight { get; set; }

        public string Model { get; set; }

        public int KitUnit { get; set; }

        public string WhereUsed { get; set; }

        public string PalletType { get; set; }

        public int LocationId { get; set; }

        public int D2LDays { get; set; }

        public int Availability { get; set; }

        public string CreatedBy { get; set; }

    }

    public class LocationMaster
    {
        public int LocationId { get; set; }
        
        public string Location { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; }
    }
}