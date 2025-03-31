using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TimerOnTime.tool
{
    class HttpRequests
    {
        public static int record_http_flag = Convert.ToInt32(ConfigurationManager.ConnectionStrings["record_http_flag"].ConnectionString);

        public static string base_url = ConfigurationManager.ConnectionStrings["base_url"].ConnectionString;
        public static string lock_command_url = base_url + ConfigurationManager.ConnectionStrings["lock_command_url"].ConnectionString;
        public static string device_status_report = base_url + "/tp5/public/index.php/api/timer/lock/device_status_report/status_report";
        public static string income_revenue_url = base_url + "/adminyunpark/public/index.php/api/income/income_record/income_record";
        public static string msg_rent_url = base_url + "/adminyunpark/public/index.php/api/rent/rent/rent_msg";
        public static string refund_url = base_url + "/tp5/public/index.php/api/timer/refund/refund/refund_park_order";
        public static string order_miss_out_url = base_url + "/tp5/public/index.php/api/timer/lock/lock_command/order_miss_out";
        public static string order_leave_url = base_url + "/tp5/public/index.php/api/pos/order/order/order_leave_auto_by_company";
        public static string region_interval_check_url = base_url + "/adminyunpark/public/index.php/api/region/region/region_interval_check";
        public static string device_connection_check_url = base_url + "/adminyunpark/public/index.php/api/device/device/device_connection_check";

        public static string HttpPost(string Url, string postDataStr)
        {

            string retString = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                Stream myRequestStream = request.GetRequestStream();
                StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("utf-8"));
                myStreamWriter.Write(postDataStr);
                myStreamWriter.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

                return retString;
            }
            catch (Exception ex)
            {
                return "httppostException"+ ex.Message;
            }
            finally
            {
                #region 记录执行日志
                if (record_http_flag == 1)
                {
                    string nowTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff", DateTimeFormatInfo.InvariantInfo);
                    RecordLog.AppendHttpLog("\r\n\r\n" + nowTime + "\r\nHttpPost请求IP =>" + Url + "\r\n发送数据 =>" + postDataStr + "\r\n接收数据 =>" + retString);
                }
                #endregion
            }

        }


        public static string HttpGet(string Url)
        {
            string retString = "";
            //return retString;//debug
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "GET";
                request.ContentType = "text/html;charset=UTF-8";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                retString = myStreamReader.ReadToEnd();

                myStreamReader.Close();
                myResponseStream.Close();

                return retString;
            }
            catch (Exception ex)
            {
                retString = ex.ToString();
                return "httpgetException";
            }
            finally
            {
                #region 记录执行日志
                if (record_http_flag == 1)
                {
                    string nowTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
                    RecordLog.AppendHttpLog("\r\n\r\n" + nowTime + "\r\nHttpGet请求IP =>" + Url + "\r\n接收数据 =>" + retString);
                }
                #endregion
            }
        }
    }
}
