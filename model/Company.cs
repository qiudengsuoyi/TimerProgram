using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerOnTime.model
{
    public class Company
    {
        private int id;
        private string workLeaveTime;
        private string autoEndOrderTime;

        public int Id { get => id; set => id = value; }
        public string WorkLeaveTime { get => workLeaveTime; set => workLeaveTime = value; }
        public string AutoEndOrderTime { get => autoEndOrderTime; set => autoEndOrderTime = value; }
    }
}
