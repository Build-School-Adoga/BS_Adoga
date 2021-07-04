using BS_Adoga.Models.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Adoga.Models.ViewModels.homeViewModels
{
    public class demoshopViewModels
    {

        public  MyHotels My_MyHotels { get; set; }
        public virtual IEnumerable<CardViewModels> My_CardViewModels { get; set; }


        //public virtual IEnumerable<HotelImage> HotelImages { get; set; }
        //public virtual IEnumerable<Card> Cards { get; set; }
        //public virtual IEnumerable<Room> Rooms { get; set; }
        //public virtual IEnumerable<RoomsDetail> RoomsDetails { get; set; }
    }
}