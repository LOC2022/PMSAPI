using System;
using System.Collections.Generic;
using System.Text;

namespace LOC.PMS.Model
{
    public class PalletsByOrderTransReport
    {
        public string PalletId { get; set; }

        public string PalletPartNo { get; set; }

        public string VendorName { get; set; }

        public string PalletStatus { get; set; }

        public int AgeingDays { get; set; }

        public DateTime UpdatedDate {get; set; }

        public string UpdatedBy { get; set; }

    }
}
