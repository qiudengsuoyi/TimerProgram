using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerOnTime.model
{
    public class Rent
    {
        private int rentID;
        private int userID;
        private int lotID;
        private int parkID;
        private int rentType;
        private int expiredTime;
        private int startTime;
        private int endTime;
        private int companyID;
        private string phone;
        private int parkState;

        public Rent()
        {
        }

        public int RentID { get => rentID; set => rentID = value; }
        public int UserID { get => userID; set => userID = value; }
        public int LotID { get => lotID; set => lotID = value; }
        public int ParkID { get => parkID; set => parkID = value; }
        public int RentType { get => rentType; set => rentType = value; }
        public int ExpiredTime { get => expiredTime; set => expiredTime = value; }
        public int StartTime { get => startTime; set => startTime = value; }
        public int EndTime { get => endTime; set => endTime = value; }
        public int CompanyID { get => companyID; set => companyID = value; }
        public string Phone { get => phone; set => phone = value; }
        public int ParkState { get => parkState; set => parkState = value; }
    }
}
