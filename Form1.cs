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
                btOrderLeave.Text = @"ֹͣ";
            }
            else
            {
                timerSecond.Stop();
                btOrderLeave.Text = "����";

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

            #region ����

            string startHandelTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff", DateTimeFormatInfo.InvariantInfo);
            string endHandelTime;
            string showmsg = "";
            bool recordflag = true;
            ReturnResult returnResult;
            #endregion ����
            showmsg += "\r\n�����볡��ʱ�����п�ʼ..." + startHandelTime;

            OrderLeaveCompanyList();
            showmsg += "\r\n��ǰ��ѯ��..." + orderLeaveCompanyList.Count;
            DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            string strTime = DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Second.ToString().PadLeft(2, '0');
            //�����ǰʱ�������趨ʱ����ͬ�������볡
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
            showmsg += "\r\n�����볡��ʱ�����н���..." + startHandelTime;
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
                " and NOW()> CONCAT('" + currentTime.ToString("yyyy-MM-dd") + " ', WorkLeaveTime,'00') and AutoEndOrderTime < CONCAT('" + currentTime.ToString("yyyy-MM-dd") + " ', WorkLeaveTime,'00')";//��ѯ��Ҫ�Զ����������ĳ���
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
                    string sql = "update pos_set_system set AutoEndOrderTime = '" + currentTime.ToString("yyyy-MM-dd HH:mm:ss") + "' where CompanyID = " + model.Id;//��ѯ��Ҫ�Զ����������ĳ���
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
                btCommand.Text = @"ֹͣ";
            }
            else
            {
                timerLockCommand.Stop();
                btCommand.Text = "����";

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
            #region ����
            DateTime currentTime = DateTime.Now;
            string startHandelTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff", DateTimeFormatInfo.InvariantInfo);
            string endHandelTime;
            string showmsg = "";
            bool recordflag = true;
            ReturnResult returnResult;
            #endregion ����
            try
            {
                showmsg += "\r\n�̶�������մ���ʱ�����п�ʼ..." + startHandelTime;
                if (currentTime.Hour >= 0 && currentTime.Hour < 1)
                {
                    returnResult = TimerOperate.MonthRentCheck();
                    showmsg += "\r\n������:" + returnResult.Msg;
                    showmsg += "\r\n������Ŀ:" + returnResult.NumCount;
                }
                else
                {
                    showmsg += "\r\n�Ƕ�ʱ������ʱ��";
                }
            }
            catch (Exception ex)
            {
                ShowMessageError("\r\n" + startHandelTime + "\r\n�̶�������մ����쳣:" + ex.ToString());
            }
            finally
            {
                endHandelTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff", DateTimeFormatInfo.InvariantInfo);
                showmsg += "\r\n�̶�������մ������н���..." + endHandelTime;
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
                    btRent.Text = "��ʼ";

                }
                else
                {

                    btRent.Text = "ֹͣ";
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
                    btRevenue.Text = "��ʼ";

                }
                else
                {
                    if (!string.IsNullOrEmpty(tvRevenueInterval.Text))
                    {
                        btRevenue.Text = "ֹͣ";
                        tvRevenueInterval.Enabled = false;

                        timerRevenue.Interval = Convert.ToInt32(tvRevenueInterval.Text) * 1000;
                        timerRevenue.Enabled = true;
                        timerRevenue.Start();
                    }
                    else
                    {
                        MessageBox.Show("��������ʱ�䣡");
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
            #region ����
            DateTime currentTime = DateTime.Now;
            string startHandelTime = currentTime.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff", DateTimeFormatInfo.InvariantInfo);
            DateTime yesterdayTime = currentTime.AddDays(-1);
            string endHandelTime;
            string showmsg = "";

            bool recordflag = true;
            ReturnResult returnResult;
            #endregion ����
            try
            {
                showmsg += "\r\nӪ�ն�ʱ�����п�ʼ..." + startHandelTime;
                if (currentTime.Hour >= 0 && currentTime.Hour < 1)
                {
                    returnResult = TimerOperate.IncomeRevenue(yesterdayTime.Year, yesterdayTime.Month, yesterdayTime.Day);
                    showmsg += "\r\n������:" + returnResult.Msg;
                    showmsg += "\r\n������Ŀ:" + returnResult.NumCount;
                }
                else
                {
                    showmsg += "\r\n�Ƕ�ʱ������ʱ��";
                }

            }
            catch (Exception ex)
            {
                ShowMessageError("\r\n" + startHandelTime + "\r\nӪ�ն�ʱ�������쳣:" + ex.ToString());
            }
            finally
            {
                endHandelTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff", DateTimeFormatInfo.InvariantInfo);
                showmsg += "\r\nӪ�ն�ʱ���������н���..." + endHandelTime;
                ShowMessage(tvRevenueDetail, showmsg, recordflag);
            }
        }

        private void timerNearDate_Tick(object sender, EventArgs e)
        {
            #region ����
            DateTime currentTime = DateTime.Now;
            string startHandelTime = currentTime.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff", DateTimeFormatInfo.InvariantInfo);
            string endHandelTime;
            string showmsg = "";
            string postStr = "";

            bool recordflag = true;
            ReturnResult returnResult;
            #endregion ����
            try
            {
                showmsg += "\r\n����쵽��..." + startHandelTime;



                postStr = "&type=" + ApiConst.RENT_NEAR_DATE;
                string strResult = HttpRequests.HttpPost(HttpRequests.msg_rent_url, postStr);
                JObject jsonObj = JObject.Parse(strResult);
                int timeInterval = 2000;
                if ((int)jsonObj["Result"] == 1)
                {
                    int countHandle = (int)jsonObj["Data"]["HandleCount"];
                    if (countHandle == 0)
                    {
                        //�Ѿ�û����Ҫ������¼��ˣ���ʱ�����õ�����8�������
                        // ��ȡ��ǰʱ��
                        currentTime = DateTime.Now;

                        // ��ȡ��������8���ʱ��
                        DateTime tomorrowMorning8AM = DateTime.Today.AddDays(1).AddHours(8).AddMinutes(0).AddSeconds(0);

                        // ����ʱ����
                        TimeSpan timeUntilTomorrowMorning8AM = tomorrowMorning8AM - currentTime;

                        // ת��Ϊ����
                        timeInterval = (int)timeUntilTomorrowMorning8AM.TotalMilliseconds;

                    }

                    timerNearDate.Interval = timeInterval;
                    timerNearDate.Stop();
                    timerNearDate.Start();

                    showmsg += "\r\n����ɹ���������Ŀ��" + countHandle + "�����ö�ʱ��ʱ������" + timeInterval;
                }
                else
                {
                    showmsg += "\r\n����ʧ�ܣ�";
                }
            }
            catch (Exception ex)
            {
                ShowMessageError("\r\n" + startHandelTime + "\r\n����쵽�ڶ�ʱ�������쳣:" + ex.ToString());
            }
            finally
            {
                endHandelTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff", DateTimeFormatInfo.InvariantInfo);
                showmsg += "\r\n����쵽�ڶ�ʱ���������н���..." + endHandelTime;
                ShowMessage(tvRentNearDate, showmsg, recordflag);
            }
        }

        private void btRentNearDate_Click(object sender, EventArgs e)
        {
            rentNearDateState = !rentNearDateState;
            if (rentNearDateState)
            {
                this.timerNearDate.Start();
                btRentNearDate.Text = @"ֹͣ";
            }
            else
            {
                timerNearDate.Stop();
                btRentNearDate.Text = "����";

            }
        }

        private void btTimeOut_Click(object sender, EventArgs e)
        {
            rentTimeoutState = !rentTimeoutState;
            if (rentTimeoutState)
            {
                this.timerTimeout.Start();
                btTimeOut.Text = @"ֹͣ";
            }
            else
            {
                timerTimeout.Stop();
                btTimeOut.Text = "����";

            }
        }

        private void timerTimeout_Tick(object sender, EventArgs e)
        {
            #region ����
            DateTime currentTime = DateTime.Now;
            string startHandelTime = currentTime.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff", DateTimeFormatInfo.InvariantInfo);
            string endHandelTime;
            string showmsg = "";
            string postStr = "";

            bool recordflag = true;
            ReturnResult returnResult;
            #endregion ����
            try
            {
                showmsg += "\r\n����ת��ͣ..." + startHandelTime;



                postStr = "&type=" + ApiConst.RENT_TIMEOUT;
                string strResult = HttpRequests.HttpPost(HttpRequests.msg_rent_url, postStr);
                JObject jsonObj = JObject.Parse(strResult);
                int timeInterval = 2000;
                if ((int)jsonObj["Result"] == 1)
                {
                    int countHandle = (int)jsonObj["Data"]["HandleCount"];
                    if (countHandle == 0)
                    {
                        //�Ѿ�û����Ҫ������¼��ˣ���ʱ�����õ�����0�������
                        // ��ȡ��ǰʱ��
                        currentTime = DateTime.Now;

                        // ��ȡ��������0���ʱ��
                        DateTime tomorrow = DateTime.Today.AddDays(1).AddHours(0).AddMinutes(0).AddSeconds(0);

                        // ����ʱ����
                        TimeSpan timeUntilTomorrow = tomorrow - currentTime;

                        // ת��Ϊ����
                        timeInterval = (int)timeUntilTomorrow.TotalMilliseconds;

                    }

                    timerTimeout.Interval = timeInterval;
                    timerTimeout.Stop();
                    timerTimeout.Start();

                    showmsg += "\r\n����ɹ���������Ŀ��" + countHandle + "�����ö�ʱ��ʱ������" + timeInterval;
                }
                else
                {
                    showmsg += "\r\n����ʧ�ܣ�";
                }
            }
            catch (Exception ex)
            {
                ShowMessageError("\r\n" + startHandelTime + "\r\n����ת��ͣ��ʱ�������쳣:" + ex.ToString());
            }
            finally
            {
                endHandelTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff", DateTimeFormatInfo.InvariantInfo);
                showmsg += "\r\n����ת��ͣ��ʱ���������н���..." + endHandelTime;
                ShowMessage(tvRentTimeout, showmsg, recordflag);
            }
        }

        private void timerRefund_Tick(object sender, EventArgs e)
        {

            #region ����
            DateTime currentTime = DateTime.Now;
            string startHandelTime = currentTime.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff", DateTimeFormatInfo.InvariantInfo);
            string endHandelTime;
            string showmsg = "";

            bool recordflag = true;
            #endregion ����
            try
            {
                timerRefund.Stop();
                showmsg += "\r\n�˿ʱ�����п�ʼ..." + startHandelTime;
                string postStr = "";
                string strResult = HttpRequests.HttpPost(HttpRequests.refund_url, postStr);
                JObject jsonObj = JObject.Parse(strResult);
                int timeInterval = 2000;
                if ((int)jsonObj["Result"] == 1)
                {


                    // ��ȡ��������8���ʱ��
                    DateTime tomorrowMorning8AM = DateTime.Today.AddDays(1).AddHours(8).AddMinutes(0).AddSeconds(0);

                    // ����ʱ����
                    TimeSpan timeUntilTomorrowMorning8AM = tomorrowMorning8AM - currentTime;

                    // ת��Ϊ����
                    timeInterval = (int)timeUntilTomorrowMorning8AM.TotalMilliseconds;
                }
                timerRefund.Interval = timeInterval;
                timerNearDate.Start();
                showmsg += "\r\n" + (string)jsonObj["Message"] + "�����ö�ʱ��ʱ������" + timeInterval;
            }
            catch (Exception ex)
            {
                ShowMessageError("\r\n" + startHandelTime + "\r\n�˿ʱ�������쳣:" + ex.ToString());
            }
            finally
            {
                endHandelTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff", DateTimeFormatInfo.InvariantInfo);
                showmsg += "\r\n�˿ʱ���������н���..." + endHandelTime;
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
                btRefund.Text = @"ֹͣ";
            }
            else
            {

                timerRefund.Stop();
                btRefund.Text = "����";

            }
        }

        private void timerOrderMissOut_Tick(object sender, EventArgs e)
        {
            #region ����
            DateTime currentTime = DateTime.Now;
            string startHandelTime = currentTime.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff", DateTimeFormatInfo.InvariantInfo);
            string endHandelTime;
            string showmsg = "";

            bool recordflag = true;
            #endregion ����
            try
            {
                showmsg += "\r\n©����鶨ʱ�����п�ʼ..." + startHandelTime;
                string postStr = "";
                string strResult = HttpRequests.HttpPost(HttpRequests.order_miss_out_url, postStr);
                JObject jsonObj = JObject.Parse(strResult);
                showmsg += "\r\n" + (string)jsonObj["Message"];
            }
            catch (Exception ex)
            {
                ShowMessageError("\r\n" + startHandelTime + "\r\n©����鶨ʱ�������쳣:" + ex.ToString());
            }
            finally
            {
                endHandelTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff", DateTimeFormatInfo.InvariantInfo);
                showmsg += "\r\n©����鶨ʱ���������н���..." + endHandelTime;
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
                btOrderMissOut.Text = @"ֹͣ";
            }
            else
            {
                timerOrderMissOut.Stop();
                btOrderMissOut.Text = "����";

            }
        }

        private void btRegionCheck_Click(object sender, EventArgs e)
        {
            regionCheckState = !regionCheckState;
            if (regionCheckState)
            {
                this.timerRegionCheck.Start();
                timerRegionCheck_Tick(this, e);
                btRegionCheck.Text = @"ֹͣ";
            }
            else
            {
                timerRegionCheck.Stop();
                btRegionCheck.Text = "����";

            }
        }

        private void timerRegionCheck_Tick(object sender, EventArgs e)
        {
            #region ����
            DateTime currentTime = DateTime.Now;
            string startHandelTime = currentTime.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff", DateTimeFormatInfo.InvariantInfo);
            string endHandelTime;
            string showmsg = "";

            bool recordflag = true;
            #endregion ����
            try
            {
                showmsg += "\r\n�˹��շ������鶨ʱ�����п�ʼ..." + startHandelTime;
                string postStr = "";
                string strResult = HttpRequests.HttpPost(HttpRequests.region_interval_check_url, postStr);
                JObject jsonObj = JObject.Parse(strResult);
                showmsg += "\r\n" + (string)jsonObj["Message"];
            }
            catch (Exception ex)
            {
                ShowMessageError("\r\n" + startHandelTime + "\r\n�˹��շ������鶨ʱ�������쳣:" + ex.ToString());
            }
            finally
            {
                endHandelTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff", DateTimeFormatInfo.InvariantInfo);
                showmsg += "\r\n�˹��շ������鶨ʱ���������н���..." + endHandelTime;
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
                btDeviceConnection.Text = @"ֹͣ";
            }
            else
            {
                timerDeviceConnection.Stop();
                btDeviceConnection.Text = "����";

            }
        }

        private void timerDeviceConnection_Tick(object sender, EventArgs e)
        {
            #region ����
            DateTime currentTime = DateTime.Now;
            string startHandelTime = currentTime.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff", DateTimeFormatInfo.InvariantInfo);
            string endHandelTime;
            string showmsg = "";

            bool recordflag = true;
            #endregion ����
            try
            {
                showmsg += "\r\n�豸���Ӽ�鶨ʱ�����п�ʼ..." + startHandelTime;
                string postStr = "";
                string strResult = HttpRequests.HttpPost(HttpRequests.device_connection_check_url, postStr);
                JObject jsonObj = JObject.Parse(strResult);
                showmsg += "\r\n" + (string)jsonObj["Message"];
            }
            catch (Exception ex)
            {
                ShowMessageError("\r\n" + startHandelTime + "\r\n�豸���Ӽ�鶨ʱ�������쳣:" + ex.ToString());
            }
            finally
            {
                endHandelTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff", DateTimeFormatInfo.InvariantInfo);
                showmsg += "\r\n�豸���Ӽ�鶨ʱ���������н���..." + endHandelTime;
                ShowMessage(tvRegionDetail, showmsg, recordflag);
            }
        }
    }
}