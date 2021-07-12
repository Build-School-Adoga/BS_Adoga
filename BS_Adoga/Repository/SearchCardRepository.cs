﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels.Search;
using System.ComponentModel.DataAnnotations;
using BS_Adoga.Models.ViewModels.homeViewModels;
using BS_Adoga.Models.ViewModels.HotelDetail;

namespace BS_Adoga.Repository
{
    //資料庫帶資料
    public class SearchCardRepository
    {
        public AdogaContext _context;

        public SearchCardRepository()
        {
            _context = new AdogaContext();
        }

        //public IQueryable<HotelSearchViewModel> ALLHotel()
        //{
        //    var hotel = from H in _context.Hotels
        //                join R in _context.Rooms on H.HotelID equals R.HotelID
        //                join D in _context.RoomsDetails on R.RoomID equals D.RoomID
        //                select new HotelSearchViewModel
        //                {
        //                    I_HotelDetailVM = new HotelDetailViewModel
        //                    {
        //                        HotelID = H.HotelID,
        //                        HotelName = H.HotelName,
        //                        HotelEngName = H.HotelEngName,
        //                        HotelAddress = H.HotelAddress,
        //                        Star = H.Star,
        //                        HotelCity = H.HotelCity,
        //                        HotelDistrict = H.HotelDistrict,
        //                    },
        //                    I_RoomVM = new RoomViewModel
        //                    {
        //                        HotelID = H.HotelID,
        //                        RoomID = R.RoomID,
        //                        RoomPrice = R.RoomPrice
        //                    },
        //                    I_RoomDetailVM = new RoomDetailViewModel
        //                    {
        //                        RoomID = R.RoomID,
        //                        CheckInDate = D.CheckInDate,
        //                        CheckOutDate = D.CheckOutDate,
        //                        RoomCount = D.RoomCount,
        //                        RoomOrder = D.RoomOrder,
        //                        RoomDiscount = D.RoomDiscount
        //                    }
        //                };
        //    return hotel;
        //}
        //public IQueryable<FilterSearchHotelViewModel> GetHotelForFilter()
        //{
        //    var h = from hotel in _context.Hotels
        //            select new HotelDetailViewModel
        //            {
        //                HotelID = hotel.HotelID,
        //                HotelName = hotel.HotelName
        //            };

        //    return h;
        //}
        //public IQueryable<FilterSearchCityViewModel> GetCityForFilter()
        //{
        //    var c = from city in _context.Hotels
        //            group _context.Hotels by city.HotelCity into citylist
        //            select new HotelDetailViewModel
        //            {
        //                //HotelID = 
        //                HotelCity=citylist.Key,

        //            };

        //    return c;
        //}

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

        public IEnumerable<HotelSearchViewModel> GetHotelAfterSearchByCityOrName(SearchDataViewModel info/*string CityOrName, string startDate,string endDate,int nRoom, int nAdult, int nKid*/)
        {
            int kidcountasadult = (info.KidCount / 2)+ (info.KidCount % 2);
            int people = info.AdultCount+(kidcountasadult);
            DateTime start = DateTime.Parse(info.CheckInDate);
            DateTime end = DateTime.Parse(info.CheckOutDate).AddDays(1);

            var data = from H in _context.Hotels
                       join R in _context.Rooms on H.HotelID equals R.HotelID
                       join D in _context.RoomsDetails on R.RoomID equals D.RoomID
                       where (H.HotelCity.Contains(info.HotelNameOrCity) || H.HotelName.Contains(info.HotelNameOrCity))
                                && D.OpenRoom == true
                                && (D.RoomCount - D.RoomOrder) >= info.RoomCount
                                && (D.RoomCount - D.RoomOrder) * R.NumberOfPeople >= people
                                && D.CheckInDate >= start
                                && D.CheckOutDate <= end
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
                               RoomPrice = R.RoomPrice,
                               NoSmoking = R.NoSmoking,
                               Breakfast = R.Breakfast,
                               WiFi = R.WiFi
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

            return data;
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

        //public FacilityViewModel GetEquipmentList()
        //{
        //    var equipList = from e in _context.Facilities
        //                    select new FacilityViewModel
        //                    {
        //                        SwimmingPool = e.SwimmingPool,
        //                        AirportTransfer = e.AirportTransfer,
        //                        FamilyChildFriendly = e.FamilyChildFriendly,
        //                        Restaurants = e.Restaurants,
        //                        Nightclub = e.Nightclub,
        //                        GolfCourse = e.GolfCourse,
        //                        Gym = e.Gym,
        //                        NoSmoking=e.NoSmoking,
        //                        SmokingArea=e.SmokingArea,
        //                        FacilitiesFordisabledGuests=e.FacilitiesFordisabledGuests,
        //                        CarPark=e.CarPark,
        //                        FrontDesk=e.FrontDesk,
        //                        SpaSauna=e.SpaSauna,
        //                        BusinessFacilities=e.BusinessFacilities,
        //                        Internet=e.Internet,
        //                        PetsAllowed=e.PetsAllowed
        //                    };
        //    return equipList.ToList();
        //}
        //public RoomBedVM GetBedType()
        //{
        //    return "hi";
        //}
    }
}