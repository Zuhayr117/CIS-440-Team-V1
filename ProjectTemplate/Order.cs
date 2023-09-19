using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectTemplate
{
    public class Order{

        public int ID;
        public int orderID;
        public DateTime orderDate;
        public double orderamount;
        public int customerID;
        public int prodductID;
        public string orderAdress;
        public string orderState;
        public int orderZip;
        public string notes;
        public int paymentCard;
        public int paymentCCV;
        public int paymentExpire;

    }
}