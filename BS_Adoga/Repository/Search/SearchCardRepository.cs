using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels.Search;
using System.ComponentModel.DataAnnotations;
using BS_Adoga.Models.ViewModels.homeViewModels;

namespace BS_Adoga.Repository.Search
{
    //資料庫帶資料
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
                            HotelCity = H.HotelCity,
                            HotelDistrict = H.HotelDistrict,
                            I_RoomVM = new RoomViewModel
                            {
                                HotelID = H.HotelID,
                                RoomID = R.RoomID,
                                RoomPrice = R.RoomPrice
                            },
                            I_RoomDetailVM = new RoomDetailViewModel
                            {
                                RoomID = R.RoomID,
                                CheckInDate = D.CheckInDate,
                                CheckOutDate = D.CheckOutDate,
                                RoomCount = D.RoomCount,
                                RoomOrder = D.RoomOrder,
                                RoomDiscount = D.RoomDiscount
                            }
                        };
            return hotel;
        }
        public SearchCardViewModel GetHotelForFilter()
        {
            SearchCardViewModel productss = new SearchCardViewModel()
            {
                Hotels = _context.Hotels.ToList()
            };

            return productss;
        }

        public IQueryable<SearchCardViewModel> GetHotel(string Name)
        {
            var hotel = from H in _context.Hotels
                        join R in _context.Rooms on H.HotelID equals R.HotelID
                        join D in _context.RoomsDetails on R.RoomID equals D.RoomID
                        where H.HotelCity.Contains(Name)
                        select new SearchCardViewModel
                        {
                            HotelID = H.HotelID,
                            HotelName = H.HotelName,
                            HotelEngName = H.HotelEngName,
                            HotelAddress = H.HotelAddress,
                            Star = H.Star,
                            HotelCity = H.HotelCity,
                            HotelDistrict = H.HotelDistrict,
                            I_RoomVM = new RoomViewModel
                            {
                                HotelID = H.HotelID,
                                RoomID = R.RoomID,
                                RoomPrice = R.RoomPrice
                            },
                            I_RoomDetailVM = new RoomDetailViewModel
                            {
                                RoomID = R.RoomID,
                                CheckInDate = D.CheckInDate,
                                CheckOutDate = D.CheckOutDate,
                                RoomCount = D.RoomCount,
                                RoomOrder = D.RoomOrder,
                                RoomDiscount = D.RoomDiscount
                            }
                        };

            return hotel;
        }

    }
}