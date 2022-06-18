namespace LOC.PMS.Model
{
    public class VendorMaster
    {
        public int VendorId { get; set; }

        public string VendorName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string BillToAddress { get; set; }

        public string ShipToAddress { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string GSTNo { get; set; }

        public int NonD2LDays { get; set; }

        public string Pincode { get; set; }

        public bool IsActive { get; set; }

        public string ModifiedBy { get; set; }
        public string ciplVendorCode { get; set; }
    }
}