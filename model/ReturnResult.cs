using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerOnTime.model
{
    public class ReturnResult
    {
        private int code;
        private int numCount;
        private string msg;
        public ReturnResult() { 
        }
        public ReturnResult(int code, int numCount, string msg)
        {
            this.Code = code;
            this.NumCount = numCount;
            this.Msg = msg;
        }

        public int Code { get => code; set => code = value; }
        public int NumCount { get => numCount; set => numCount = value; }
        public string Msg { get => msg; set => msg = value; }
    }
}
