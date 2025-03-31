using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerOnTime.tool
{
    class ConStantConfig
    {
        public static string redisPreLockCommand = ConfigurationManager.ConnectionStrings["redis_lock_pre_command"].ConnectionString;
     
    }
}
