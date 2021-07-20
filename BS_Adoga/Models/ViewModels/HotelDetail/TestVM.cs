using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Adoga.Models.ViewModels.HotelDetail
{
    public class TestVM
    {
        public string HotelID { get; set; }

        public string HotelName { get; set; }

        public string RoomID { get; set; }

        public string RoomName { get; set; }

        public decimal RoomPrice { get; set; }

        public decimal RoomDiscount { get; set; }

        public decimal RoomNowPrice { get; set; }
    }
}