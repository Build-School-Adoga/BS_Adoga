using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Adoga.Models.ViewModels.Search
{
    public class SearchDataViewModel
    {
        public string HotelNameOrCity { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int RoomCount { get; set; }

        public int PeopleCount { get; set; }
    }
}