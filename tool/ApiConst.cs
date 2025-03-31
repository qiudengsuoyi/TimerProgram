using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerOnTime.tool
{
    public class ApiConst
    {
        public const int DEVICE_CAR_OUT_PAY_FINISH = 1; //支付停车费离场 上报无车
        public const int DEVICE_CAR_OUT_NO_PAY_FINISH = 2; //未支付停车费离场 上报无车
        public const int DEVICE_RENT_RECYCLE = 7; //月租到期，收回权限


        public const int INTERVAL_PAY_LEAVE_TIME = 900; //15 * 60支付后离场的超时时间
        public const int INTERVAL_NO_PAY_CANCEL_TIME = 180; //3 * 60预订未支付时订单取消时间
        public const int INTERVAL_OPEN_FAIL_TIME = 40; //车位锁开锁失败时间
        public const int INTERVAL_OPEN_BARRIER_FAIL_TIME = 80; //闸杆开锁失败时间


        public const int INTERVAL_PARK_ORDER_TIME = 30; //临停生成订单的时间


        //离线
        public const int DISCONNECT_TIME = 180; //离线时间


        public const string RENT_NEAR_DATE = "rentNearDate";
        public const string RENT_TIMEOUT = "rentTimeout";
    }
}
