using System;
using System.Collections.Generic;
using System.Text;

namespace LOC.PMS.Model.Report
{
    public class PalletsPartTransReport
    {
        public string PalletId { get; set; }

        public string PalletPartNo { get; set; }

        public int AgeingDays { get; set; }

        public int PalletQty { get; set; }

    }
}
