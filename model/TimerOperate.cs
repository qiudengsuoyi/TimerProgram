using MySqlX.XDevAPI.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimerOnTime.tool;

namespace TimerOnTime.model
{
    public class TimerOperate
    {
        public static ReturnResult MonthRentCheck()
        {
            #region 变量定义
            DataTable dataTable;
            string nowTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
            string sql;
            MySQLDBHelp mySQLDBConn_timer = new MySQLDBHelp();
       
            List<Rent> monthRentTimeouts = new List<Rent>();
            List<JObject> jObjects = new List<JObject>();
            ReturnResult returnResult = new ReturnResult();


            #endregion 变量定义

            mySQLDBConn_timer.StartMysqlCon();
            try
            {
                sql = "select * from connect_rent_park_carowner  where ExpiredState = 0 and ParkState = 0 and RentType = 1 and EndTime < NOW() order by ID desc";
                dataTable = mySQLDBConn_timer.ExecuteMySqlRead(sql);

                if (dataTable.Rows.Count > 0)
                {
                    #region 获取未处理的月租过期数据
                    Rent rent;
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        rent = new Rent();
                        rent.RentID = Convert.ToInt32(dataTable.Rows[i]["ID"].ToString());
                        rent.ParkID = Convert.ToInt32(dataTable.Rows[i]["ParkingID"].ToString());
                        rent.CompanyID = Convert.ToInt32(dataTable.Rows[i]["CompanyID"].ToString());
                        rent.RentType = Convert.ToInt32(dataTable.Rows[i]["RentType"].ToString());
                        rent.LotID = Convert.ToInt32(dataTable.Rows[i]["LotID"].ToString());
                        rent.UserID = Convert.ToInt32(dataTable.Rows[i]["UserID"].ToString());
                        rent.ParkState = Convert.ToInt32(dataTable.Rows[i]["ParkState"].ToString());
                        rent.Phone = (dataTable.Rows[i]["PhoneNumber"] is System.DBNull) ? "" : dataTable.Rows[i]["PhoneNumber"].ToString();
                        monthRentTimeouts.Add(rent);
                        #region  固定月租到期,更新车位表

                        string url = HttpRequests.device_status_report;
                        string postStr = "&parkID=" + rent.ParkID + "&lotID=" + rent.LotID + "&orderID=" + rent.RentID + "&eventType=" + ApiConst.DEVICE_RENT_RECYCLE;
                        HttpRequests.HttpPost(url, postStr);

                       

                        #endregion 固定月租到期,更新车位表
                    }

                    returnResult.Code = 1;
                    returnResult.Msg = "success";
                    returnResult.NumCount = monthRentTimeouts.Count;
                    return returnResult;
                }
                else
                {
                    returnResult.Code = 1;
                    returnResult.Msg = "无月租到期记录";
                    returnResult.NumCount = 0;
                    return returnResult;
                }
                #endregion 获取未处理的月租过期数据
            }
            catch (Exception ex)
            {
                throw new Exception(nowTime + "=>\r\nMonthrentCheck函数报异常：" + ex.Message + "\r\n异常信息：" + ex.ToString());
            }
            finally
            {
                mySQLDBConn_timer.CloseMysqlCon();
            }
        }

        public static ReturnResult IncomeRevenue(int year, int month, int day)
        {
            #region 变量定义
            string nowTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
            DataTable datatable;

            string beginTime_today = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
            MySQLDBHelp mySQLDBHelp = new MySQLDBHelp();
            string sql, postStr;
            List<Revenue> revenues = new List<Revenue>();
            Revenue revenue;
            ReturnResult returnResult = new ReturnResult();
          
            //type(ParkingType):1-单独车场数据 2-充值数据 3-联合车场数据
            #endregion 变量定义

            try
            {
                mySQLDBHelp.StartMysqlCon();

                #region 获取到单独车场数据(车位控制设备车场和人工pos设备车场)
                sql = "select ID,ParkingName,RevenueStatisticsTime,InfoTransformation from parkinginfo " +
                    "where InfoTransformation in (1,6) and (RevenueStatisticsTime is null or RevenueStatisticsTime<'" + beginTime_today
                    + "') order by ID desc";
                datatable = mySQLDBHelp.ExecuteMySqlRead(sql);
                if (datatable.Rows.Count > 0)
                {
                    for (int i = 0; i < datatable.Rows.Count; i++)
                    {
                        revenue = new Revenue();
                        revenue.RevenueID = Convert.ToInt32(datatable.Rows[i]["ID"].ToString());
                        revenue.RevenueName = datatable.Rows[i]["ParkingName"].ToString();
                        if (Convert.ToInt32(datatable.Rows[i]["InfoTransformation"].ToString()) == 1)
                        {
                            revenue.RevenueType = 1;
                        }
                        else {
                            revenue.RevenueType = 4;
                        }
                        revenues.Add(revenue);
                    }
                }
                #endregion 获取到单独车场数据(车位控制设备车场和人工pos设备车场)

                #region 获取联合车场数据
                sql = "select ID,AreaParkName,RevenueStatisticsTime from connect_park_area " +
                    "where DeleteState =0 and (RevenueStatisticsTime is null or RevenueStatisticsTime<'" + beginTime_today
                    + "') order by ID desc";
                datatable = mySQLDBHelp.ExecuteMySqlRead(sql);
                if (datatable.Rows.Count > 0)
                {
                    for (int i = 0; i < datatable.Rows.Count; i++)
                    {
                        revenue = new Revenue();
                        revenue.RevenueID = Convert.ToInt32(datatable.Rows[i]["ID"].ToString());
                        revenue.RevenueName = datatable.Rows[i]["AreaParkName"].ToString();
                        revenue.RevenueType = 3;
                        revenues.Add(revenue);
                      
                    }
                }
                #endregion 获取联合车场数据

               

                #region 充值数据
            
                sql = "select RevenueStatisticsTime from recharge_income_recent_time where ID =1  and RevenueStatisticsTime<'" + beginTime_today + "'";
                datatable = mySQLDBHelp.ExecuteMySqlRead(sql);
                if (datatable.Rows.Count > 0)
                {
                    revenue = new Revenue();
                    revenue.RevenueID = 0;
                    revenue.RevenueName = "用户充值营收";
                    revenue.RevenueType = 2;
                    revenues.Add(revenue);
                   
                }


                #endregion 充值数据

               

                #region  公司临停营收
                sql = "select ID,CompanyName,RevenueStatisticsTime from company_cooperation " +
                    "where DeleteState =0 and RevenueState = 1 and (RevenueStatisticsTime is null or RevenueStatisticsTime<'" + beginTime_today
                    + "') order by ID desc";
                datatable = mySQLDBHelp.ExecuteMySqlRead(sql);
                if (datatable.Rows.Count > 0)
                {
                    for (int i = 0; i < datatable.Rows.Count; i++)
                    {
                        revenue = new Revenue();
                        revenue.RevenueID = Convert.ToInt32(datatable.Rows[i]["ID"].ToString());
                        revenue.RevenueName = datatable.Rows[i]["CompanyName"].ToString();
                        revenue.RevenueType = 5;
                        revenues.Add(revenue);
                     
                    }

               
                }
                #endregion 公司临停营收

                #region  收费员营收

                sql = "select ID,Name,RevenueStatisticsTime from pos_toll_collector where DeleteState =0 and RoleState in (0,2) and (RevenueStatisticsTime is null or RevenueStatisticsTime<'"
                    + beginTime_today + "') order by ID desc";
                //RoleState角色，0收费员，1管理员，2顶班人员 需要统计0/2
                datatable = mySQLDBHelp.ExecuteMySqlRead(sql);
                if (datatable.Rows.Count > 0)
                {
                    for (int i = 0; i < datatable.Rows.Count; i++)
                    {
                        revenue = new Revenue();
                        revenue.RevenueID = Convert.ToInt32(datatable.Rows[i]["ID"].ToString());
                        revenue.RevenueName = datatable.Rows[i]["Name"].ToString();
                        revenue.RevenueType = 6;
                        revenues.Add(revenue);
                       
                    }

                  
                }
                #endregion 收费员营收
            
                int handleCount = 10;
                if (revenues.Count < handleCount) {
                    handleCount = revenues.Count;
                }
               
                for (int i = 0; i < handleCount; i++)
                {
                    string postData = "&type="+ revenues[i].RevenueType +"&year=" + year + "&month=" + month + "&day=" + day + "&ParkingID=" + revenues[i].RevenueID
                         + "&ParkingName=" + revenues[i].RevenueName;
                    HttpRequests.HttpPost(HttpRequests.income_revenue_url, postData);
                }
             
                returnResult.Code = 1;
                returnResult.NumCount = handleCount;
                returnResult.Msg = "操作成功";
                return returnResult;
            }
            catch (Exception ex)
            {
                throw new Exception("函数IncomeOperate报异常:" + ex.ToString());
            }
            finally
            {
                mySQLDBHelp.CloseMysqlCon();
            }
        }




    }
}
