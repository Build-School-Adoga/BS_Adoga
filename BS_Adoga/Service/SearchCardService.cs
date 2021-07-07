using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels.Search;
using BS_Adoga.Repository;

namespace BS_Adoga.Service
{
    public class SearchCardService
    {
        private SearchCardRepository _r;

        public SearchCardService()
        {
            _r = new SearchCardRepository();
        }

        public IQueryable<HotelSearchViewModel> GetHotels(string Name)
        {
            var result = _r.GetHotel(Name);
            return result;
        }

        public SearchCardViewModel GetSearchViewModelData(string Name)
        {
            var data = new SearchCardViewModel
            {
                HotelSearchVM = _r.GetHotelAfterSearchByCityOrName(Name),
                HotelOptionVM = _r.GetHotelOption()
            };

            return data;
        }

    }
}