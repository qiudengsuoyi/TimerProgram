using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerOnTime.model.order
{
    class LockPreOrder
    {
        private int parkID;
        private int lotID;
        private int orderID;
        private int eventType;

        public LockPreOrder(int parkID, int lotID, int orderID, int eventType)
        {
            this.parkID = parkID;
            this.lotID = lotID;
            this.orderID = orderID;
            this.eventType = eventType;

        }

        public int ParkID { get => parkID; set => parkID = value; }
        public int LotID { get => lotID; set => lotID = value; }
        public int OrderID { get => orderID; set => orderID = value; }
        public int EventType { get => eventType; set => eventType = value; }
    }
}
