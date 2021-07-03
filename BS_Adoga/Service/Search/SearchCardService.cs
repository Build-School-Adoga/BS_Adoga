using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels.Search;
using BS_Adoga.Repository.Search;
using BS_Adoga.Models.ViewModels.homeViewModels;

namespace BS_Adoga.Service.Search
{
    //把拿到的資料做商業邏輯
    public class SearchCardService
    {
        private SearchCardRepository _r;

        public SearchCardService()
        {
            _r = new SearchCardRepository();
        }

        public IQueryable<SearchCardViewModel> ALLHotel()
        {
            var list = _r.ALLHotel();
            return list;
        }

        public IQueryable<SearchCardViewModel> GetHotels(string Name)
        {         
            var result = _r.GetHotel(Name);
            return result;
        }
        public IQueryable<SearchCardViewModel> GetListToFilter()
        {
            var filterList = new SearchCardViewModel
            {
                FilterSearchCityVM = _r.GetCityForFilter(),
                FilterSearchHotelVM = _r.GetHotelForFilter()
            };
            
            return (IQueryable<SearchCardViewModel>)filterList;
            
        }
    }
}