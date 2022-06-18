using System;
using System.Collections.Generic;
using System.Text;

namespace LOC.PMS.Model
{
    public class Orders
    {
        public string OrderNo { get; set; }
        public int VendorId { get; set; }
        public int NoOfPartsOrdered { get; set; }
        public int OrderQty { get; set; }
        public int OrderTypeId { get; set; }
        public OrderStatus OrderStatusId { get; set; }
        public DateTime OrderCreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
