using System;

namespace LOC.PMS.Model
{
    public class DayPlan
    {
        public string ByteArray { get; set; }

        public string Model { get; set; }   
        public string PalletPartNo { get; set; }    
        public string PalletPartName { get; set; }
        public string Date { get; set; }    
        public int Qty { get; set; }    

        public string Vendor { get; set; }  


        public static DayPlan FromCsv(string csvLine)
        {
            string[] lines = csvLine.Split(',');
            DayPlan palletDetailsRequest = new DayPlan();
            palletDetailsRequest.PalletPartNo = lines[2];
            palletDetailsRequest.PalletPartName = lines[3];           
            palletDetailsRequest.Qty = Convert.ToInt32(lines[5]);
            palletDetailsRequest.Model = lines[1];
            palletDetailsRequest.Date = lines[4];
            palletDetailsRequest.Vendor = lines[6];
            return palletDetailsRequest;
        }

    }
}
