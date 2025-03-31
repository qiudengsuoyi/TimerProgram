using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerOnTime.tool
{
    class TimeUtil
    {
        /*
    bflag : true 秒 ， false 毫秒
*/
        public static int GetTimeStamp(bool bflag)
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            int ret = 0;
            if (bflag)
                ret = Convert.ToInt32(ts.TotalSeconds);
            else
                ret = Convert.ToInt32(ts.TotalMilliseconds);

            return ret;
        }
    }
}
