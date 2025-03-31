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
    public class Lot
    {
        private int lotID;
        private string lotNumber;
        private int lotOrderID;
        private int lotUserID;
        private int lotOrderType;
        private int lotState;
        private int lotDeviceType;
        private int lotControl;
        private int carState;
        private string arriveTime;
        private string openLockTime;
        private int infraredCrossState;//越界红外
        private int infraredHeightState;//高度红外

        private int upDownRightState;//右闸杆电机状态
        private int upDownLeftState;//左闸杆电机状态

        private int cameraState;//相机状态，0在线，1断线

        private int takePhotoState = 0;//抓拍 0 不抓拍，1抓拍


        public Lot()
        {
        }

        public int LotID { get => lotID; set => lotID = value; }
        public string LotNumber { get => lotNumber; set => lotNumber = value; }
        public int LotOrderID { get => lotOrderID; set => lotOrderID = value; }
        public int LotUserID { get => lotUserID; set => lotUserID = value; }
        public int LotOrderType { get => lotOrderType; set => lotOrderType = value; }
        public int LotState { get => lotState; set => lotState = value; }
        public int LotDeviceType { get => lotDeviceType; set => lotDeviceType = value; }
        public int LotControl { get => lotControl; set => lotControl = value; }
        public int CarState { get => carState; set => carState = value; }
        public string ArriveTime { get => arriveTime; set => arriveTime = value; }
        public string OpenLockTime { get => openLockTime; set => openLockTime = value; }
        public int InfraredCrossState { get => infraredCrossState; set => infraredCrossState = value; }
        public int InfraredHeightState { get => infraredHeightState; set => infraredHeightState = value; }
        public int UpDownRightState { get => upDownRightState; set => upDownRightState = value; }
        public int UpDownLeftState { get => upDownLeftState; set => upDownLeftState = value; }
        public int CameraState { get => cameraState; set => cameraState = value; }
        public int TakePhotoState { get => takePhotoState; set => takePhotoState = value; }

        public static Lot GetLotByParkIDLotID(int parkID,int lotID)
        {
            DataTable datatable;
            string sql;
            MySQLDBHelp mySQLDBConn = new MySQLDBHelp();
            Lot model = null;
            try
            {
                mySQLDBConn.StartMysqlCon();

                #region 获取车位数据

                sql = "select * from parkmap_" + parkID + " where ID = " + lotID;


                datatable = mySQLDBConn.ExecuteMySqlRead(sql);
                if (datatable.Rows.Count > 0)
                {

                    for (int i = 0; i < datatable.Rows.Count; i++)
                    {
                        model = new Lot();

                        model.lotID = (datatable.Rows[i]["ID"] is System.DBNull) ? 0 : Convert.ToInt32(datatable.Rows[i]["ID"].ToString());
                        model.lotNumber = (datatable.Rows[i]["ParkingLot"] is System.DBNull) ? "" : datatable.Rows[i]["ParkingLot"].ToString();
                        model.lotOrderID = (datatable.Rows[i]["OrderID"] is System.DBNull) ? 0 : Convert.ToInt32(datatable.Rows[i]["OrderID"].ToString());
                        model.lotOrderType = (datatable.Rows[i]["OrderType"] is System.DBNull) ? 0 : Convert.ToInt32(datatable.Rows[i]["OrderType"].ToString());
                        model.lotUserID = (datatable.Rows[i]["UserID"] is System.DBNull) ? 0 : Convert.ToInt32(datatable.Rows[i]["UserID"].ToString());
                        model.lotState = (datatable.Rows[i]["ParkingState"] is System.DBNull) ? 0 : Convert.ToInt32(datatable.Rows[i]["ParkingState"].ToString());
                        model.lotControl = (datatable.Rows[i]["ParkOwnerControl"] is System.DBNull) ? 0 : Convert.ToInt32(datatable.Rows[i]["ParkOwnerControl"].ToString());
                        model.lotDeviceType = (datatable.Rows[i]["DeviceVersionType"] is System.DBNull) ? 0 : Convert.ToInt32(datatable.Rows[i]["DeviceVersionType"].ToString());
                        model.carState = (datatable.Rows[i]["CarArrivedState"] is System.DBNull) ? 0 : Convert.ToInt32(datatable.Rows[i]["CarArrivedState"].ToString());
                        model.arriveTime = (datatable.Rows[i]["CarArrivedTime"] is System.DBNull) ? "2021-01-01 00:00:00" : datatable.Rows[i]["CarArrivedTime"].ToString();
                        model.openLockTime = (datatable.Rows[i]["OpenLockTime"] is System.DBNull) ? "2021-01-01 00:00:00" : datatable.Rows[i]["OpenLockTime"].ToString();
                        model.infraredCrossState = (datatable.Rows[i]["InfraredState"] is System.DBNull) ? 0 : Convert.ToInt32(datatable.Rows[i]["InfraredState"].ToString());
                        model.infraredHeightState = (datatable.Rows[i]["InfraredForHeightState"] is System.DBNull) ? 0 : Convert.ToInt32(datatable.Rows[i]["InfraredForHeightState"].ToString());

                        model.upDownRightState = (datatable.Rows[i]["UpDownState"] is System.DBNull) ? 0 : Convert.ToInt32(datatable.Rows[i]["UpDownState"].ToString());
                        model.upDownLeftState = (datatable.Rows[i]["UpDownLeftState"] is System.DBNull) ? 0 : Convert.ToInt32(datatable.Rows[i]["UpDownLeftState"].ToString());

                        model.cameraState = (datatable.Rows[i]["CarArrivedState"] is System.DBNull) ? 0 : Convert.ToInt32(datatable.Rows[i]["CarArrivedState"].ToString());
                    }
                }
                else
                {
                    ;
                }
                #endregion 获取车位数据
                return model;
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
