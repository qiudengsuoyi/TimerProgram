using MySqlX.XDevAPI.Common;
using MySqlX.XDevAPI.Relational;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.ComponentModel.Design;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using TimerOnTime.model;
using TimerOnTime.model.order;
using TimerOnTime.tool;

namespace TimerOnTime
{
    public partial class Form1 : Form
    {
        private RedisHelper redis = new RedisHelper();
        private static ArrayList orderLeaveCompanyList = new ArrayList();
        private bool orderLeaveState = false;
        private bool lockCommandState = false;
        private bool rentNearDateState = false;
        private bool rentTimeoutState = false;
        private bool refundState = false;
        private bool orderMissOutState = false;
        private bool regionCheckState = false;
        private bool deviceConnectionState = false;

        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void btOrderLeave_Click(object sender, EventArgs e)
        {
            orderLeaveState = !orderLeaveState;
            if (orderLeaveState)
            {
                this.timerSecond.Start();
                timerSecond_Tick(sender, e);
                btOrderLeave.Text = @"停止";
            }
            else
            {
                timerSecond.Stop();
                btOrderLeave.Text = "开启";

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            this.timerLockCommand.Enabled = true;
            this.timerLockCommand.Interval = 1000;
            timerLockCommand.Stop();

        }

        private void timerSecond_Tick(object sender, EventArgs e)
        {

            #region 变量

            string startHandelTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff", DateTimeFormatInfo.InvariantInfo);
            string endHandelTime;
            string showmsg = "";
            bool recordflag = true;
            ReturnResult returnResult;
            #endregion 变量
            showmsg += "\r\n订单离场定时器运行开始..." + startHandelTime;

            OrderLeaveCompanyList();
            showmsg += "\r\n当前查询到..." + orderLeaveCompanyList.Count;
            DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            string strTime = DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Second.ToString().PadLeft(2, '0');
            //如果当前时分秒与设定时间相同，订单离场
            if (orderLeaveState)
            {
                foreach (Company item in orderLeaveCompanyList)
                {
                    /*if (strTime.Equals(item.WorkLeaveTime))
                    {
                        Thread thread = new Thread(new ParameterizedThreadStart(CallOrderLeaveThread));
                        thread.IsBackground = true;
                        thread.Start(item);
                    }*/
                    try
                    {


                        Task.Run(() => UpdateOrderLeaveTime(item));

                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    { }
                }
            }
            showmsg += "\r\n订单离场定时器运行结束..." + startHandelTime;
            ShowMessage(tvOrderLeaveDetail, showmsg, recordflag);
        }

        public void OrderLeaveCompanyList()
        {
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            MySQLDBHelp mySQLDBConn = new MySQLDBHelp();
            mySQLDBConn.StartMysqlCon();
            orderLeaveCompanyList = new ArrayList();
            Company model;

            string sql = "select ID,CompanyID,WorkLeaveTime,AutoEndOrderTime from pos_set_system  where AutoEndOrderState = 1" +
                " and NOW()> CONCAT('" + currentTime.ToString("yyyy-MM-dd") + " ', WorkLeaveTime,'00') and AutoEndOrderTime < CONCAT('" + currentTime.ToString("yyyy-MM-dd") + " ', WorkLeaveTime,'00')";//查询需要自动结束订单的车场
            RecordLog.AppendInfoLog(sql);
            DataTable datatable = mySQLDBConn.ExecuteMySqlRead(sql);
            if (datatable.Rows.Count > 0)
            {
                for (int i = 0; i < datatable.Rows.Count; i++)
                {
                    model = new Company();
                    model.Id = Int32.Parse(datatable.Rows[i]["CompanyID"].ToString());
                    model.WorkLeaveTime = currentTime.ToString("yyyy-MM-dd") + " " + datatable.Rows[i]["WorkLeaveTime"].ToString() + ":00";
                    orderLeaveCompanyList.Add(model);
                }

            }
            mySQLDBConn.CloseMysqlCon();


        }
        public void UpdateOrderLeaveTime(Company model)
        {
            DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            string strResult = HttpRequests.HttpPost(HttpRequests.order_leave_url, "&companyID=" + model.Id + "&endTime=" + model.WorkLeaveTime);
            JObject jsonObj = JObject.Parse(strResult);
            if ((int)jsonObj["Result"] == 1)
            {
                int countHandle = (int)jsonObj["Data"]["HandleCount"];
                if (countHandle == 0)
                {
                    MySQLDBHelp mySQLDBConn = new MySQLDBHelp();
                    mySQLDBConn.StartMysqlCon();
                    string sql = "update pos_set_system set AutoEndOrderTime = '" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + "' where CompanyID = " + model.Id;//查询需要自动结束订单的车场
                    RecordLog.AppendInfoLog(sql);
                    mySQLDBConn.ExecuteMySqlCom(sql);
                    mySQLDBConn.CloseMysqlCon();
                }
            }


        }


        public void CallOrderLeaveThread(object obj)
        {/*
            Park park = obj as Park;
            string url = HttpRequests.order_leave_url + "&parkID=" + park.Id + "&endTime=" + park.WorkLeaveTime;
            string result = HttpRequests.HttpPost(HttpRequests.order_leave_url, "&parkID=" + park.Id + "&endTime=" + park.WorkLeaveTime);

            ShowMessage(tvOrderLeaveDetail, url, true);
            ShowMessage(tvOrderLeaveDetail, result, true);
            int i = 0;
            foreach (Park item in parkOrderLeaveList)
            {
                if (item.Id == park.Id)
                {
                    System.DateTime currentTime = new System.DateTime();
                    currentTime = System.DateTime.Now;
                    ((Park)parkOrderLeaveList[i]).RunTime = currentTime.ToString();
                    break;
                }
                i++;
            }*/

        }



        private void btCommand_Click(object sender, EventArgs e)
        {
            lockCommandState = !lockCommandState;
            if (lockCommandState)
            {
                this.timerLockCommand.Start();
                btCommand.Text = @"停止";
            }
            else
            {
                timerLockCommand.Stop();
                btCommand.Text = "开启";

            }
        }



        private void timerLockCommand_Tick(object sender, EventArgs e)
        {
            int timeStamp = TimeUtil.GetTimeStamp(true);

            List<LockPreOrder> lockPreOrders = redis.SortedSetRangeByScore<LockPreOrder>(ConStantConfig.redisPreLockCommand, 0, timeStamp);

            foreach (LockPreOrder lockPreOrder in lockPreOrders)
            {
                ThreadPool.QueueUserWorkItem(LockCommandThread, lockPreOrder);
            }
        }



        public void LockCommandThread(object obj)
        {
            string result = "";
            try
            {
                LockPreOrder model = obj as LockPreOrder;
                string url = HttpRequests.lock_command_url;
                string postStr = "&parkID=" + model.ParkID + "&lotID=" + model.LotID + "&orderID=" + model.OrderID + "&eventType=" + model.EventType;
                result = HttpRequests.HttpPost(url, postStr);
                ShowMessage(tvLockCommandDetail, url, true);
                ShowMessage(tvLockCommandDetail, postStr, true);

            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            finally
            {

                ShowMessage(tvLockCommandDetail, result, true);
            }
        }



        public void ShowMessage(TextBox textBox, string str, bool recordflag = true)
        {
            try
            {
                if (textBox.Text.Length > 2000)
                {
                    textBox.Clear();
                }
                textBox.Text = textBox.Text.Insert(0, "\r\n" + str);
            }
            catch (Exception ex)
            {
                RecordLog.AppendErrorLog(ex.ToString());
            }
            finally
            {
                if (recordflag)
                {
                    RecordLog.AppendInfoLog(str);
                }

            }
        }

        public void ShowMessageError(string str, bool recordflag = true)
        {
            try
            {
                /*   if (textBox_err.Text.Length > 5000)
                   {
                       textBox_err.Clear();
                   }
                   textBox_err.Text = textBox_err.Text.Insert(0, "\r\n" + str);*/
            }
            catch (Exception ex)
            {
                RecordLog.AppendErrorLog(ex.ToString());
            }
            finally
            {
                if (recordflag)
                {
                    RecordLog.AppendErrorLog(str);
                }

            }
        }

        private void timerRent_Tick(object sender, EventArgs e)
        {
            #region 变量
            DateTime currentTime = DateTime.Now;
            string startHandelTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff", DateTimeFormatInfo.InvariantInfo);
            string endHandelTime;
            string showmsg = "";
            bool recordflag = true;
            ReturnResult returnResult;
            #endregion 变量
            try
            {
                showmsg += "\r\n固定月租回收处理定时器运行开始..." + startHandelTime;
                if (currentTime.Hour >= 0 && currentTime.Hour < 1)
                {
                    returnResult = TimerOperate.MonthRentCheck();
                    showmsg += "\r\n处理结果:" + returnResult.Msg;
                    showmsg += "\r\n处理数目:" + returnResult.NumCount;
                }
                else
                {
                    showmsg += "\r\n非定时器处理时段";
                }
            }
            catch (Exception ex)
            {
                ShowMessageError("\r\n" + startHandelTime + "\r\n固定月租回收处理报异常:" + ex.ToString());
            }
            finally
            {
                endHandelTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff", DateTimeFormatInfo.InvariantInfo);
                showmsg += "\r\n固定月租回收处理运行结束..." + endHandelTime;
                ShowMessage(tvRentDetail, showmsg, recordflag);
            }
        }

        private void btRent_Click(object sender, EventArgs e)
        {
            try
            {
                if (timerRent.Enabled)
                {

                    timerRent.Stop();
                    timerRent.Enabled = false;
                    btRent.Text = "开始";

                }
                else
                {

                    btRent.Text = "停止";
                    timerRent.Enabled = true;
                    timerRent.Start();


                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btRevenue_Click(object sender, EventArgs e)
        {
            try
            {
                if (timerRevenue.Enabled)
                {

                    timerRevenue.Stop();
                    timerRevenue.Enabled = false;

                    tvRevenueInterval.Enabled = true;
                    btRevenue.Text = "开始";

                }
                else
                {
                    if (!string.IsNullOrEmpty(tvRevenueInterval.Text))
                    {
                        btRevenue.Text = "停止";
                        tvRevenueInterval.Enabled = false;

                        timerRevenue.Interval = Convert.ToInt32(tvRevenueInterval.Text) * 1000;
                        timerRevenue.Enabled = true;
                        timerRevenue.Start();
                    }
                    else
                    {
                        MessageBox.Show("请输入间隔时间！");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void timerRevenue_Tick(object sender, EventArgs e)
        {
            #region 变量
            DateTime currentTime = DateTime.Now;
            string startHandelTime = currentTime.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff", DateTimeFormatInfo.InvariantInfo);
            DateTime yesterdayTime = currentTime.AddDays(-1);
            string endHandelTime;
            string showmsg = "";

            bool recordflag = true;
            ReturnResult returnResult;
            #endregion 变量
            try
            {
                showmsg += "\r\n营收定时器运行开始..." + startHandelTime;
                if (currentTime.Hour >= 0 && currentTime.Hour < 1)
                {
                    returnResult = TimerOperate.IncomeRevenue(yesterdayTime.Year, yesterdayTime.Month, yesterdayTime.Day);
                    showmsg += "\r\n处理结果:" + returnResult.Msg;
                    showmsg += "\r\n处理数目:" + returnResult.NumCount;
                }
                else
                {
                    showmsg += "\r\n非定时器处理时段";
                }

            }
            catch (Exception ex)
            {
                ShowMessageError("\r\n" + startHandelTime + "\r\n营收定时器处理报异常:" + ex.ToString());
            }
            finally
            {
                endHandelTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff", DateTimeFormatInfo.InvariantInfo);
                showmsg += "\r\n营收定时器处理运行结束..." + endHandelTime;
                ShowMessage(tvRevenueDetail, showmsg, recordflag);
            }
        }

        private void timerNearDate_Tick(object sender, EventArgs e)
        {
            #region 变量
            DateTime currentTime = DateTime.Now;
            string startHandelTime = currentTime.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff", DateTimeFormatInfo.InvariantInfo);
            string endHandelTime;
            string showmsg = "";
            string postStr = "";

            bool recordflag = true;
            ReturnResult returnResult;
            #endregion 变量
            try
            {
                showmsg += "\r\n月租快到期..." + startHandelTime;



                postStr = "&type=" + ApiConst.RENT_NEAR_DATE;
                string strResult = HttpRequests.HttpPost(HttpRequests.msg_rent_url, postStr);
                JObject jsonObj = JObject.Parse(strResult);
                int timeInterval = 2000;
                if ((int)jsonObj["Result"] == 1)
                {
                    int countHandle = (int)jsonObj["Data"]["HandleCount"];
                    if (countHandle == 0)
                    {
                        //已经没有需要处理的事件了，定时器设置到明日8点后启动
                        // 获取当前时间
                        currentTime = DateTime.Now;

                        // 获取明天早上8点的时间
                        DateTime tomorrowMorning8AM = DateTime.Today.AddDays(1).AddHours(8).AddMinutes(0).AddSeconds(0);

                        // 计算时间间隔
                        TimeSpan timeUntilTomorrowMorning8AM = tomorrowMorning8AM - currentTime;

                        // 转换为秒数
                        timeInterval = (int)timeUntilTomorrowMorning8AM.TotalMilliseconds;

                    }

                    timerNearDate.Interval = timeInterval;
                    timerNearDate.Stop();
                    timerNearDate.Start();

                    showmsg += "\r\n处理成功，处理数目：" + countHandle + "，重置定时器时间间隔：" + timeInterval;
                }
                else
                {
                    showmsg += "\r\n处理失败！";
                }
            }
            catch (Exception ex)
            {
                ShowMessageError("\r\n" + startHandelTime + "\r\n月租快到期定时器处理报异常:" + ex.ToString());
            }
            finally
            {
                endHandelTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff", DateTimeFormatInfo.InvariantInfo);
                showmsg += "\r\n月租快到期定时器处理运行结束..." + endHandelTime;
                ShowMessage(tvRentNearDate, showmsg, recordflag);
            }
        }

        private void btRentNearDate_Click(object sender, EventArgs e)
        {
            rentNearDateState = !rentNearDateState;
            if (rentNearDateState)
            {
                this.timerNearDate.Start();
                btRentNearDate.Text = @"停止";
            }
            else
            {
                timerNearDate.Stop();
                btRentNearDate.Text = "开启";

            }
        }

        private void btTimeOut_Click(object sender, EventArgs e)
        {
            rentTimeoutState = !rentTimeoutState;
            if (rentTimeoutState)
            {
                this.timerTimeout.Start();
                btTimeOut.Text = @"停止";
            }
            else
            {
                timerTimeout.Stop();
                btTimeOut.Text = "开启";

            }
        }

        private void timerTimeout_Tick(object sender, EventArgs e)
        {
            #region 变量
            DateTime currentTime = DateTime.Now;
            string startHandelTime = currentTime.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff", DateTimeFormatInfo.InvariantInfo);
            string endHandelTime;
            string showmsg = "";
            string postStr = "";

            bool recordflag = true;
            ReturnResult returnResult;
            #endregion 变量
            try
            {
                showmsg += "\r\n月租转临停..." + startHandelTime;



                postStr = "&type=" + ApiConst.RENT_TIMEOUT;
                string strResult = HttpRequests.HttpPost(HttpRequests.msg_rent_url, postStr);
                JObject jsonObj = JObject.Parse(strResult);
                int timeInterval = 2000;
                if ((int)jsonObj["Result"] == 1)
                {
                    int countHandle = (int)jsonObj["Data"]["HandleCount"];
                    if (countHandle == 0)
                    {
                        //已经没有需要处理的事件了，定时器设置到明日0点后启动
                        // 获取当前时间
                        currentTime = DateTime.Now;

                        // 获取明天早上0点的时间
                        DateTime tomorrow = DateTime.Today.AddDays(1).AddHours(0).AddMinutes(0).AddSeconds(0);

                        // 计算时间间隔
                        TimeSpan timeUntilTomorrow = tomorrow - currentTime;

                        // 转换为秒数
                        timeInterval = (int)timeUntilTomorrow.TotalMilliseconds;

                    }

                    timerTimeout.Interval = timeInterval;
                    timerTimeout.Stop();
                    timerTimeout.Start();

                    showmsg += "\r\n处理成功，处理数目：" + countHandle + "，重置定时器时间间隔：" + timeInterval;
                }
                else
                {
                    showmsg += "\r\n处理失败！";
                }
            }
            catch (Exception ex)
            {
                ShowMessageError("\r\n" + startHandelTime + "\r\n月租转临停定时器处理报异常:" + ex.ToString());
            }
            finally
            {
                endHandelTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff", DateTimeFormatInfo.InvariantInfo);
                showmsg += "\r\n月租转临停定时器处理运行结束..." + endHandelTime;
                ShowMessage(tvRentTimeout, showmsg, recordflag);
            }
        }

        private void timerRefund_Tick(object sender, EventArgs e)
        {

            #region 变量
            DateTime currentTime = DateTime.Now;
            string startHandelTime = currentTime.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff", DateTimeFormatInfo.InvariantInfo);
            string endHandelTime;
            string showmsg = "";

            bool recordflag = true;
            #endregion 变量
            try
            {
                timerRefund.Stop();
                showmsg += "\r\n退款定时器运行开始..." + startHandelTime;
                string postStr = "";
                string strResult = HttpRequests.HttpPost(HttpRequests.refund_url, postStr);
                JObject jsonObj = JObject.Parse(strResult);
                int timeInterval = 2000;
                if ((int)jsonObj["Result"] == 1)
                {


                    // 获取明天早上8点的时间
                    DateTime tomorrowMorning8AM = DateTime.Today.AddDays(1).AddHours(8).AddMinutes(0).AddSeconds(0);

                    // 计算时间间隔
                    TimeSpan timeUntilTomorrowMorning8AM = tomorrowMorning8AM - currentTime;

                    // 转换为秒数
                    timeInterval = (int)timeUntilTomorrowMorning8AM.TotalMilliseconds;
                }
                timerRefund.Interval = timeInterval;
                timerNearDate.Start();
                showmsg += "\r\n" + (string)jsonObj["Message"] + "，重置定时器时间间隔：" + timeInterval;
            }
            catch (Exception ex)
            {
                ShowMessageError("\r\n" + startHandelTime + "\r\n退款定时器处理报异常:" + ex.ToString());
            }
            finally
            {
                endHandelTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff", DateTimeFormatInfo.InvariantInfo);
                showmsg += "\r\n退款定时器处理运行结束..." + endHandelTime;
                ShowMessage(tvRefund, showmsg, recordflag);
            }
        }

        private void btRefund_Click(object sender, EventArgs e)
        {

            refundState = !refundState;
            if (refundState)
            {

                this.timerRefund.Start();
                timerRefund_Tick(sender, e);
                timerRefund.Enabled = true;
                btRefund.Text = @"停止";
            }
            else
            {

                timerRefund.Stop();
                btRefund.Text = "开启";

            }
        }

        private void timerOrderMissOut_Tick(object sender, EventArgs e)
        {
            #region 变量
            DateTime currentTime = DateTime.Now;
            string startHandelTime = currentTime.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff", DateTimeFormatInfo.InvariantInfo);
            string endHandelTime;
            string showmsg = "";

            bool recordflag = true;
            #endregion 变量
            try
            {
                showmsg += "\r\n漏单检查定时器运行开始..." + startHandelTime;
                string postStr = "";
                string strResult = HttpRequests.HttpPost(HttpRequests.order_miss_out_url, postStr);
                JObject jsonObj = JObject.Parse(strResult);
                showmsg += "\r\n" + (string)jsonObj["Message"];
            }
            catch (Exception ex)
            {
                ShowMessageError("\r\n" + startHandelTime + "\r\n漏单检查定时器处理报异常:" + ex.ToString());
            }
            finally
            {
                endHandelTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff", DateTimeFormatInfo.InvariantInfo);
                showmsg += "\r\n漏单检查定时器处理运行结束..." + endHandelTime;
                ShowMessage(tvMissOut, showmsg, recordflag);
            }
        }

        private void btOrderMissOut_Click(object sender, EventArgs e)
        {
            orderMissOutState = !orderMissOutState;
            if (orderMissOutState)
            {
                this.timerOrderMissOut.Start();
                timerOrderMissOut_Tick(this, e);
                btOrderMissOut.Text = @"停止";
            }
            else
            {
                timerOrderMissOut.Stop();
                btOrderMissOut.Text = "开启";

            }
        }

        private void btRegionCheck_Click(object sender, EventArgs e)
        {
            regionCheckState = !regionCheckState;
            if (regionCheckState)
            {
                this.timerRegionCheck.Start();
                timerRegionCheck_Tick(this, e);
                btRegionCheck.Text = @"停止";
            }
            else
            {
                timerRegionCheck.Stop();
                btRegionCheck.Text = "开启";

            }
        }

        private void timerRegionCheck_Tick(object sender, EventArgs e)
        {
            #region 变量
            DateTime currentTime = DateTime.Now;
            string startHandelTime = currentTime.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff", DateTimeFormatInfo.InvariantInfo);
            string endHandelTime;
            string showmsg = "";

            bool recordflag = true;
            #endregion 变量
            try
            {
                showmsg += "\r\n人工收费区域检查定时器运行开始..." + startHandelTime;
                string postStr = "";
                string strResult = HttpRequests.HttpPost(HttpRequests.region_interval_check_url, postStr);
                JObject jsonObj = JObject.Parse(strResult);
                showmsg += "\r\n" + (string)jsonObj["Message"];
            }
            catch (Exception ex)
            {
                ShowMessageError("\r\n" + startHandelTime + "\r\n人工收费区域检查定时器处理报异常:" + ex.ToString());
            }
            finally
            {
                endHandelTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff", DateTimeFormatInfo.InvariantInfo);
                showmsg += "\r\n人工收费区域检查定时器处理运行结束..." + endHandelTime;
                ShowMessage(tvRegionDetail, showmsg, recordflag);
            }

        }

        private void btDeviceConnection_Click(object sender, EventArgs e)
        {
            deviceConnectionState = !deviceConnectionState;
            if (deviceConnectionState)
            {
                this.timerDeviceConnection.Start();
                timerDeviceConnection_Tick(this, e);
                btDeviceConnection.Text = @"停止";
            }
            else
            {
                timerDeviceConnection.Stop();
                btDeviceConnection.Text = "开启";

            }
        }

        private void timerDeviceConnection_Tick(object sender, EventArgs e)
        {
            #region 变量
            DateTime currentTime = DateTime.Now;
            string startHandelTime = currentTime.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff", DateTimeFormatInfo.InvariantInfo);
            string endHandelTime;
            string showmsg = "";

            bool recordflag = true;
            #endregion 变量
            try
            {
                showmsg += "\r\n设备连接检查定时器运行开始..." + startHandelTime;
                string postStr = "";
                string strResult = HttpRequests.HttpPost(HttpRequests.device_connection_check_url, postStr);
                JObject jsonObj = JObject.Parse(strResult);
                showmsg += "\r\n" + (string)jsonObj["Message"];
            }
            catch (Exception ex)
            {
                ShowMessageError("\r\n" + startHandelTime + "\r\n设备连接检查定时器处理报异常:" + ex.ToString());
            }
            finally
            {
                endHandelTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff", DateTimeFormatInfo.InvariantInfo);
                showmsg += "\r\n设备连接检查定时器处理运行结束..." + endHandelTime;
                ShowMessage(tvRegionDetail, showmsg, recordflag);
            }
        }
    }
}