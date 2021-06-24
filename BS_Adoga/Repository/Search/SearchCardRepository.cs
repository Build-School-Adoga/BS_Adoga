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
        private AdogaContext _context;

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

        //public IQueryable<Hotel> Hotel()
        //{
        //    var hotel = from h in _context.Hotels
        //                select h;

        //    return hotel;
        //}
        //public IQueryable<SearchCardViewModel> ALLHotel()
        //{

        //    var hotelData = from H in _context.Hotels
        //                    join R in _context.Rooms on H.HotelID equals R.HotelID
        //                    join D in _context.RoomsDetails on R.RoomID equals D.RoomID
        //                    select new SearchCardViewModel
        //                    {
        //                        HotelID = H.HotelID,
        //                        HotelName = H.HotelName,
        //                        HotelEngName = H.HotelEngName,
        //                        HotelAddress = H.HotelAddress,
        //                        Star = H.Star,
        //                        RoomVM = (from r in _context.Rooms
        //                                                        where r.HotelID == H.HotelID
        //                                                        select new RoomViewModel {
        //                                                            RoomID = r.RoomID,
        //                                                            RoomPrice = r.RoomPrice
        //                                                        }),
        //                        RoomDetailVM = (from d in _context.RoomsDetails
        //                                        where d.RoomID == D.RoomID
        //                                        select new RoomDetailViewModel {
        //                                            CheckInDate = d.CheckInDate,
        //                                            CheckOutDate = d.CheckOutDate,
        //                                            RoomCount = d.RoomCount,
        //                                            RoomOrder = d.RoomOrder,
        //                                            RoomDiscount = d.RoomDiscount,
        //                                            OpenRoom = d.OpenRoom
        //                                        })

        //                    };

        //    return hotelData;
        //}

        //public IQueryable<Hotel> GetHotels(string Name)
        //{
        //    var hotel = from h in _context.Hotels
        //                where h.HotelName.Contains(Name)
        //                select h;

        //    return hotel;
        //}
    }
}