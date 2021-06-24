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

        //public IQueryable<SearchCardViewModel> ALLHotel()
        //{
        //    var list = _r.ALLHotel();

        //    var result = list.Select(h => new SearchCardViewModel
        //    {
        //        HotelID = h.HotelID,
        //        HotelName = h.HotelName,
        //        HotelEngName = h.HotelEngName,
        //        HotelAddress = h.HotelAddress,
        //        Star = h.Star
        //    });

        //    return result;
        //}
        public IQueryable<SearchCardViewModel> ALLHotel()
        {
            var list = _r.ALLHotel();

            //因为把资料指定放进哪里的叙述在repository已经做过了，所以没必要Service再做多一次
            //var result = list.Select(h => new SearchCardViewModel
            //{
            //    HotelID = h.HotelID,
            //    HotelName = h.HotelName,
            //    HotelEngName = h.HotelEngName,
            //    HotelAddress = h.HotelAddress,
            //    Star = h.Star,
            //    RoomDetailVM = h.RoomDetailVM,
            //    RoomVM = h.RoomVM
            //});


            return list;
        }
        //public IQueryable<SearchCardViewModel> GetHotels(string Name)
        //{
        //    var list = _r.ALLHotel();

        //    var result = list.Select(h => new SearchCardViewModel
        //    {
        //        HotelID = h.HotelID,
        //        HotelName = h.HotelName,
        //        HotelEngName = h.HotelEngName,
        //        HotelAddress = h.HotelAddress,
        //        Star = h.Star
        //    });

        //    return result;
        //}
    }
}