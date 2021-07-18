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
            //int kidcountasadult = (info.KidCount / 2) + (info.KidCount % 2);
            //int people = info.AdultCount + (kidcountasadult);
            //DateTime start = DateTime.Parse(info.CheckInDate);
            //DateTime end = DateTime.Parse(info.CheckOutDate);

            var countNight = (DateTime.Parse(info.CheckOutDate) - DateTime.Parse(info.CheckInDate)).TotalDays;

            var a = GetEachHotelOneRoom(info.HotelNameOrCity, info.CheckInDate, info.CheckOutDate, (int)countNight, info.RoomCount, info.AdultCount, info.KidCount);
            return a;
        }

        public IEnumerable<HotelSearchViewModel> GetEachHotelOneRoom(string CityName, string startDate, string endDate, int countNight, int orderRoom, int adult, int child)
        {
            int kidcountasadult = (child / 2) + (child % 2);
            int people = adult + (kidcountasadult);
            DateTime start = DateTime.Parse(startDate);
            DateTime end = DateTime.Parse(endDate);

            var allhotel = (from H in _context.Hotels
                            where (H.HotelCity.Contains(CityName) || H.HotelName.Contains(CityName))
                            group H by H.HotelID into hid
                            select hid.Key).ToArray();


            var table = (from H in _context.Hotels
                         join R in _context.Rooms on H.HotelID equals R.HotelID
                         join D in _context.RoomsDetails on R.RoomID equals D.RoomID
                         where allhotel.Contains(H.HotelID)
                         && R.NumberOfPeople * orderRoom >= people
                         && D.CheckInDate >= start
                         && D.CheckInDate < end
                         && D.OpenRoom == true
                         && (D.RoomCount - D.RoomOrder) >= orderRoom
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
                         }).AsEnumerable();

            var table_2 = from t in table
                          group new { t } by new { t.HotelID, t.I_RoomVM.RoomID } into Group
                          select new
                          {
                              HotelID = Group.Key.HotelID,
                              RoomID = Group.Key.RoomID,
                              Discount = Group.Sum(r => r.t.I_RoomDetailVM.RoomDiscount) / countNight,
                          };

            var table_3 = (from t in table
                           join t2 in table_2 on t.HotelID equals t2.HotelID
                           where t.I_RoomVM.RoomID == t2.RoomID
                           select new HotelSearchViewModel
                           {
                               HotelID = t.HotelID,
                               HotelName = t.HotelName,
                               HotelEngName = t.HotelEngName,
                               HotelAddress = t.HotelAddress,
                               Star = t.Star,
                               HotelCity = t.HotelCity,
                               HotelDistrict = t.HotelDistrict,
                               I_RoomVM = new RoomViewModel
                               {
                                   HotelID = t.HotelID,
                                   RoomID = t.I_RoomVM.RoomID,
                                   RoomPrice = t.I_RoomVM.RoomPrice,
                                   NoSmoking = t.I_RoomVM.NoSmoking,
                                   Breakfast = t.I_RoomVM.Breakfast,
                                   WiFi = t.I_RoomVM.WiFi
                               },
                               I_RoomDetailVM = new RoomDetailViewModel
                               {
                                   RoomID = t.I_RoomDetailVM.RoomID,
                                   CheckInDate = t.I_RoomDetailVM.CheckInDate,
                                   CheckOutDate = t.I_RoomDetailVM.CheckOutDate,
                                   RoomCount = t.I_RoomDetailVM.RoomCount,
                                   RoomOrder = t.I_RoomDetailVM.RoomOrder,
                                   RoomDiscount = t2.Discount
                               }
                           }).GroupBy(x => x.HotelID, (key, g) => g.OrderBy(x => x.I_RoomVM.RoomPrice * (1 - x.I_RoomDetailVM.RoomDiscount)).First());

            //RoomNowPrice = (r.RoomPrice * (1 - t2.Discount)),

            foreach (var a in table_3)
            {
                var aa = a;
                decimal total = a.I_RoomVM.RoomPrice * (1 - a.I_RoomDetailVM.RoomDiscount);
                var x = 0;
                //foreach (var b in a)
                //{
                //    var bb = b;
                //    decimal total = b.I_RoomVM.RoomPrice * (1 - b.I_RoomDetailVM.RoomDiscount);
                //    
                //}
            }
            return table_3;

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