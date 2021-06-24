using BS_Adoga.Models.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Adoga.Models.ViewModels.Search
{
    public class SearchCardViewModel
    {
        public string HotelID { get; set; }
        public string HotelName { get; set; }
        public string HotelEngName { get; set; }
        public string HotelAddress { get; set; }
        public int Star { get; set; }
        public List<RoomDetailViewModel> RoomDetailVM { get; set; }
        public List<RoomViewModel> RoomVM { get; set; }
        public virtual IEnumerable<Hotel> Hotels { get; set; }
    }
}