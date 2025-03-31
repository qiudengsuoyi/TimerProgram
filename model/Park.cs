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
    class Park
    {
        private int id;
        private int companyID;
        private string workLeaveTime;
        private string parkingName;
        private string runTime;

        private int parkID;
        private string parkName;
        private string tbOrder;
        private string tbLot;
        private int lockupTime;

        public int Id { get => id; set => id = value; }
        public string WorkLeaveTime { get => workLeaveTime; set => workLeaveTime = value; }
        public int CompanyID { get => companyID; set => companyID = value; }
        public string ParkingName { get => parkingName; set => parkingName = value; }
        public string RunTime { get => runTime; set => runTime = value; }
        public int ParkID { get => parkID; set => parkID = value; }
        public string ParkName { get => parkName; set => parkName = value; }
        public string TbOrder { get => tbOrder; set => tbOrder = value; }
        public string TbLot { get => tbLot; set => tbLot = value; }
        public int LockupTime { get => lockupTime; set => lockupTime = value; }

        public static Park GetParkByParkID(int parkID)
        {
            DataTable datatable;
            string sql;
            MySQLDBHelp mySQLDBConn = new MySQLDBHelp();
            Park park = null;
            try
            {
                mySQLDBConn.StartMysqlCon();

                #region 获取停车场数据

                sql = "select * from Parkinginfo where ID = " + parkID;


                datatable = mySQLDBConn.ExecuteMySqlRead(sql);
                if (datatable.Rows.Count > 0)
                {

                    for (int i = 0; i < datatable.Rows.Count; i++)
                    {
                        park = new Park();

                        park.ParkID = (datatable.Rows[i]["ID"] is System.DBNull) ? 0 : Convert.ToInt32(datatable.Rows[i]["ID"].ToString());
                        park.ParkName = (datatable.Rows[i]["ParkingName"] is System.DBNull) ? "" : datatable.Rows[i]["ParkingName"].ToString();
                        park.TbLot = (datatable.Rows[i]["ParkingLot"] is System.DBNull) ? "" : datatable.Rows[i]["ParkingLot"].ToString();
                        park.TbOrder = (datatable.Rows[i]["ParkingReserve"] is System.DBNull) ? "" : datatable.Rows[i]["ParkingReserve"].ToString();
                        park.LockupTime = (datatable.Rows[i]["LockupTime"] is System.DBNull) ? 0 : Convert.ToInt32(datatable.Rows[i]["LockupTime"].ToString());

                    }
                }
                else
                {
                    ;
                }
                #endregion 获取停车场数据
                return park;
            }
            catch (Exception ex)
            {
                string nowTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
                throw new Exception(nowTime + "=>\r\nGetParkinginfoList函数报Exception异常:" + ex.Message + "\r\n异常字符串" + ex.ToString());
            }
            finally
            {
                mySQLDBConn.CloseMysqlCon();
            }

        }
    }
}
