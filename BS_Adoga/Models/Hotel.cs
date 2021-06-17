using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Adoga.Models
{
    public class Hotel
    {
        public string HotelID { get; set; }
        public string HotelName { get; set; }
        public string HotelAddress { get; set; }
        public string HotelAbout { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public int Star { get; set; }
    }
}