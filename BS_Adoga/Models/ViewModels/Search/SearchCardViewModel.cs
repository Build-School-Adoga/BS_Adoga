using BS_Adoga.Models.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BS_Adoga.Models.ViewModels.homeViewModels;

namespace BS_Adoga.Models.ViewModels.Search
{
    public class SearchCardViewModel
    {
        public string HotelID { get; set; }
        public string HotelName { get; set; }
        public string HotelEngName { get; set; }
        public string HotelAddress { get; set; }
        public string HotelCity { get; set; }
        public string HotelDistrict { get; set; }
        public int Star { get; set; }
        public RoomViewModel I_RoomVM { get; set; }
        public RoomDetailViewModel I_RoomDetailVM { get; set; }

        /// <summary>
        /// faker Tom
        /// </summary>
        public virtual IEnumerable<Hotel> Hotels { get; set; }
    }
}