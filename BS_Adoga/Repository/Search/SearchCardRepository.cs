using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels.Search;
using System.ComponentModel.DataAnnotations;

namespace BS_Adoga.Repository.Search
{
    public class SearchCardRepository
    {
        public AdogaContext _context;

        public SearchCardRepository()
        {
            _context = new AdogaContext();
        }

        public IQueryable<SearchCardViewModel> ALLHotel()
        {
            var hotel = from H in _context.Hotels
                        join R in _context.Rooms on H.HotelID equals R.HotelID
                        join D in _context.RoomsDetails on R.RoomID equals D.RoomID
                        select new SearchCardViewModel
                        {
                            HotelID = H.HotelID,
                            HotelName = H.HotelName,
                            HotelEngName = H.HotelEngName,
                            HotelAddress = H.HotelAddress,
                            Star = H.Star,
                            RoomVM = new RoomViewModel
                            {
                                HotelID = H.HotelID,
                                RoomID = R.RoomID,
                                RoomPrice = R.RoomPrice
                            },
                            RoomDetailVM = new RoomDetailViewModel
                            {
                                RoomID = R.RoomID,
                                CheckInDate = D.CheckInDate,
                                CheckOutDate = D.CheckOutDate,
                                RoomCount = D.RoomCount,
                                RoomOrder = D.RoomOrder,
                                RoomDiscount = D.RoomDiscount
                            }
                        };
            var list = hotel;

            return list;
        }

    }
}