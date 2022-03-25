namespace LOC.PMS.Model
{
    public class VendorMaster
    {
        public string VendorName { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public string VendorBillToAddress { get; set; }

        public string VendorShipToAddress { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Pincode { get; set; }

        public bool IsActive { get; set; }
    }
}