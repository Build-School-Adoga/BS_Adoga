using BS_Adoga.Models.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Adoga.Models.ViewModels.Search
{
    public class SearchCardViewModel
    {
        public IEnumerable<HotelSearchViewModel> HotelSearchVM { get; set; }

        
        public IEnumerable<HotelOptionViewModel>  HotelOptionVM { get; set; }

    }
}