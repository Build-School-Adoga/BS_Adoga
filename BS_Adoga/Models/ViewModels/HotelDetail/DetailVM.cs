using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Adoga.Models.ViewModels.HotelDetail
{
    public class DetailVM
    {
        public HotelVM hotelVM{ get; set; }

        //public IQueryable<RoomTypeVM> roomTypeVM { get; set; }
        public IEnumerable<RoomTypeVM> roomTypeVM { get; set; }
    }
}