using System;
using System.Collections.Generic;
using System.Text;

namespace LOC.PMS.Model
{
    public class PalletsByOrderTrans
    {
        public int OrderPalletTransId { get; set; }
        public string OrderNo { get; set; }
        public string PalletId { get; set; }
        public int AssignedQty { get; set; }
        public int LocationId { get; set; }
        public PalletStatus PalletStatus { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

    }
}
