using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels.Search;
using System.ComponentModel.DataAnnotations;

namespace BS_Adoga.Repository
{
    public class SearchCardRepository
    {
        public AdogaContext _context;

        public SearchCardRepository()
        {
            _context = new AdogaContext();
        }

        public IQueryable<HotelSearchViewModel> ALLHotel()
        {
            var hotel = from H in _context.Hotels
                        join R in _context.Rooms on H.HotelID equals R.HotelID
                        join D in _context.RoomsDetails on R.RoomID equals D.RoomID
                        select new HotelSearchViewModel
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

        public IQueryable<HotelSearchViewModel> GetHotel(string Name)
        {
            IQueryable<HotelSearchViewModel> hotel = from H in _context.Hotels
                        join R in _context.Rooms on H.HotelID equals R.HotelID
                        join D in _context.RoomsDetails on R.RoomID equals D.RoomID
                        where H.HotelCity.Contains(Name)
                        select new HotelSearchViewModel
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

        public IEnumerable<HotelSearchViewModel> GetHotelAfterSearchByCityOrName(string Name)
        {
            var data = from H in _context.Hotels
                           join R in _context.Rooms on H.HotelID equals R.HotelID
                           join D in _context.RoomsDetails on R.RoomID equals D.RoomID
                           where H.HotelCity.Contains(Name) || H.HotelName.Contains(Name)
                           select new HotelSearchViewModel
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

            return data.ToList();
        }

        public IEnumerable<HotelOptionViewModel> GetHotelOption()
        {
            var optionData = from H in _context.Hotels
                             select new HotelOptionViewModel
                             {
                                 HotelID = H.HotelID,
                                 HotelName = H.HotelName,
                                 HotelCity = H.HotelCity,
                                 HotelAddress = H.HotelAddress
                             };

            return optionData.ToList();
        }


    }
}