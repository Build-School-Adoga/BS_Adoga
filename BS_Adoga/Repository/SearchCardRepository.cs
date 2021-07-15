using System;
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

        public IEnumerable<HotelSearchViewModel> GetHotelAfterSearchByCityOrName(SearchDataViewModel info)
        {
            int kidcountasadult = (info.KidCount / 2)+ (info.KidCount % 2);
            int people = info.AdultCount+(kidcountasadult);
            DateTime start = DateTime.Parse(info.CheckInDate);
            DateTime end = DateTime.Parse(info.CheckOutDate).AddDays(1);

            //var data = from h in _context.Hotels
            //           join r in _context.Rooms on h.HotelID equals r.HotelID
            //           join rd in _context.RoomsDetails on r.RoomID equals rd.RoomID
            //           where h.HotelID.Contains(from H in _context.Hotels
            //                                    where H.HotelCity == info.HotelNameOrCity
            //                                    select new { HotelID = H.HotelID })
            //                 and rd.CheckInDate in ('2021-06-20', '2021-06-21')--使用者選擇的日期
            //                    and rd.RoomCount - rd.RoomOrder >= 2--房間數量
            //                    and r.NumberOfPeople * (rd.RoomCount - rd.RoomOrder) >= 2
            //            group by h.HotelID,r.RoomID
            //            order by OneNightPrice


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