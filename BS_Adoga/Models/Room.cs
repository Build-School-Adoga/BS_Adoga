using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Adoga.Models
{
    public class Room
    {
        public string RoomID { get; set; }
        public string HotelID { get; set; }
        public string RoomName { get; set; }
        public int NumberOfPeople { get; set; }
        public int RoomCount { get; set; }
        public decimal RoomPrice { get; set; }
        public int TypesOfBedsID { get; set; }
        public bool Breakfast { get; set; }
        public bool WiFi { get; set; }
        public bool TV { get; set; }
    }
}