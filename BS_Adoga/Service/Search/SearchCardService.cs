using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels.Search;
using BS_Adoga.Repository.Search;

namespace BS_Adoga.Service.Search
{
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

            var result = list.Select(h => new SearchCardViewModel
            {
                HotelID = h.HotelID,
                HotelName = h.HotelName,
                HotelEngName = h.HotelEngName,
                HotelAddress = h.HotelAddress,
                Star = h.Star
            });

            return result;
        }
        public IQueryable<SearchCardViewModel> GetHotels(string Name)
        {
            var list = _r.ALLHotel();

            var xxx = from p in _r._context.Hotels
                      where p.HotelCity == Name
                      select p;

            var result = xxx.Select(h => new SearchCardViewModel
            {
                HotelID = h.HotelID,
                HotelName = h.HotelName,
                HotelEngName = h.HotelEngName,
                HotelAddress = h.HotelAddress,
                Star = h.Star
            });

            return result;
        }
    }
}