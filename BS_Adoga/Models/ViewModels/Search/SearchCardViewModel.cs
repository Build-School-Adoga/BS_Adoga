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
        public HotelDetailViewModel I_HotelDetailVM { get; set; }
        public RoomViewModel I_RoomVM { get; set; }
        public RoomDetailViewModel I_RoomDetailVM { get; set; }
        public IQueryable<FilterSearchCityViewModel> FilterSearchCityVM { get; set; }
        public IQueryable<FilterSearchHotelViewModel> FilterSearchHotelVM { get; set; }

        /// <summary>
        /// faker Tom
        /// </summary>
        public virtual IEnumerable<Hotel> FilterHotels { get; set; }
    }
}