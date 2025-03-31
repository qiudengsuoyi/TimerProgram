using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerOnTime.model
{
    class Revenue
    {
        private int revenueID;
        private string revenueName;
        private int revenueType;

        public int RevenueID { get => revenueID; set => revenueID = value; }
        public string RevenueName { get => revenueName; set => revenueName = value; }
        public int RevenueType { get => revenueType; set => revenueType = value; }
    }
}
