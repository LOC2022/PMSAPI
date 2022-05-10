namespace LOC.PMS.Model.Report
{
    public class InwardReport
    {
        public string DCNo { get; set; }

        public string CreatedDate { get; set; }

        public int Qty { get; set; }

        public string Status { get; set; }

    }

    public class InwardReportDetails
    {
        public string DCNo { get; set; }

        public string PalletPartNo { get; set; }

        public int Qty { get; set; }

        public string Status { get; set; }

    }
}
