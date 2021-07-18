using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels.HotelDetail;
using BS_Adoga.Repository;

namespace BS_Adoga.Service
{
    public class HotelDetailService
    {
        private HotelDetailRepository _repository;

        public HotelDetailService()
        {
            _repository = new HotelDetailRepository();
        }

        public DetailVM GetDetailVM(string hotelId, string startDate, string endDate, int orderRoom, int adult, int child)
        {
            DetailVM hotelDetail = new DetailVM()
            {
                hotelVM = GetHotelById(hotelId),
                roomTypeVM = GetRoomTypeByFilter(hotelId, startDate, endDate, orderRoom, adult, child),
                hotelOptionVM = new SearchCardRepository().GetHotelOption()
            };
            return hotelDetail;
        }

        public HotelVM GetHotelById(string hotelId)
        {
            var source = _repository.GetHotelById(hotelId);

            //先first 再轉 view model
            //如果用 first or default要處理default 否則用 first就好
            var result = source.Select(s => new HotelVM
            {
                HotelID = s.HotelID,
                HotelName = s.HotelName,
                HotelEngName = s.HotelEngName,
                HotelCity = s.HotelCity,
                HotelAddress = s.HotelAddress,/*s.HotelCity + "," + s.HotelDistrict + "," +*/
                HotelAbout = s.HotelAbout,
                Longitude = s.Longitude,
                Latitude = s.Latitude,
                Star = s.Star
            }).First();

            return result;
        }

        public object GetHotelFacilityById(string hotelId)
        {
            var source = _repository.GetHotelFacilityById(hotelId);

            var result = source.Select(x => new
            {
                x.AirportTransfer,
                x.BusinessFacilities,
                x.CarPark,
                x.FacilitiesFordisabledGuests,
                x.FamilyChildFriendly,
                x.FrontDesk,
                x.GolfCourse,
                x.Gym,
                x.Internet,
                x.Nightclub,
                x.NoSmoking,
                x.PetsAllowed,
                x.Restaurants,
                x.SmokingArea,
                x.SpaSauna,
                x.SwimmingPool
            }).First();

            return result;
        }

        //根據床型判斷房間可以有多少個大人和小孩
        public List<RoomTypeVM> Helper_CountAdultChild(List<RoomTypeVM> data)
        {
            data.ForEach((x) =>
            {
                foreach (var bed in x.RoomBed)
                {
                    switch (bed.Name)
                    {
                        case "雙人床":
                        case "加大雙人床":
                        case "單人床(兩床)":
                            //result.Where((x,index)=>index==count).
                            x.Adult = x.Adult + (2 * bed.Amount);
                            x.Child = x.Child + (1 * bed.Amount);
                            break;

                        case "特大雙人床":
                            x.Adult = x.Adult + (2 * bed.Amount);
                            x.Child = x.Child + (2 * bed.Amount);
                            break;

                        case "上下舖":
                            x.Adult = x.Adult + (2 * bed.Amount);
                            x.Child = x.Child + 0;
                            break;

                        default:
                            break;
                    }
                }
            });

            return data;
        }
        public IEnumerable<RoomTypeVM> GetRoomTypeByFilter(string hotelId, string startDate, string endDate, int orderRoom, int adult, int child)
        {
            //設定好傳給repository的引數。
            if (hotelId == null) hotelId = "hotel04";
            DateTime startDate_p = DateTime.Parse(startDate);
            DateTime endDate_p = DateTime.Parse(endDate);
            int countNight = new TimeSpan(endDate_p.Ticks - startDate_p.Ticks).Days;//2;
            //int orderRoom = 2;
            int totalPerson = adult + child;//12

            var result = _repository.GetRoomTypeByFilter(hotelId, startDate_p, endDate_p, countNight, orderRoom, adult, child, totalPerson).ToList();

            result = Helper_CountAdultChild(result);

            return result;
        }

        public IEnumerable<RoomTypeVM> GetSpecificRoomType(string hotelId, string startDate, string endDate, int orderRoom, int adult, int child, bool breakfast, bool noSmoking, bool family)
        {
            //這隻service要做的事情跟別的service有重複到，就直接讓那個service處理先
            var allRoom = GetRoomTypeByFilter(hotelId, startDate, endDate, orderRoom, adult, child).ToList();

            var result = from room in allRoom
                         select room;
            if (breakfast)
            {
                result = from room in result
                         where room.Breakfast == breakfast
                         select room;
            }
            if (noSmoking)
            {
                result = from room in result
                         where room.NoSmoking == noSmoking
                         select room;
            }
            if (family)
            {
                int ppl = 4;
                result = from room in result
                         where room.Adult + room.Child >= ppl
                         select room;
            }

            return result;
        }

    }
}