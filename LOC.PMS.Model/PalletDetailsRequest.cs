using System;
using System.Collections.Generic;
using System.Text;

namespace LOC.PMS.Model
{
    public class PalletDetailsRequest
    {
        public string PalletPartNo { get; set; }

        public string PalletName { get; set; }

        public int PalletWeight { get; set; }

        public string Model { get; set; }

        public int KitUnit { get; set; }

        public string WhereUsed { get; set; }

        public string PalletType { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

        public bool IsActive { get; set; }
    }
}
