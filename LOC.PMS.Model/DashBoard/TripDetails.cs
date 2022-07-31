using System;
using System.Collections.Generic;
using System.Text;

namespace LOC.PMS.Model.DashBoard
{
    public class TripDetails
    {
        public List<TripDetailsDB> tripDetailsDB { get; set; }
        public List<TripDetailsGrid> tripDetailsGrid { get; set; }
    }

    public class TripDetailsDB
    {
        public int Value { get; set; }
        public string Flag { get; set; }
    }

    public class TripDetailsGrid
    {
        public string DCDate { get; set; }
        public string DCNo { get; set; }
        public string Qty { get; set; }
        public string Flag { get; set; }
    }
}
