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

        public DetailVM GetDetailVM (string hotelId)
        {
            DetailVM hotelDetail = new DetailVM()
            {
                hotelVM = GetHotelById(hotelId),
                roomTypeVM = GetRoomTypeByFilter(hotelId)
            };
            return hotelDetail;
        }

        public HotelVM GetHotelById(string hotelId)
        {
            //if (hotelId == null) hotelId = "hotel04"; //controller已處理，這裡不用再寫

            var source = _repository.GetHotelById(hotelId);

            //先first 再轉 view model
            //如果用 first or default要處理default 否則用 first就好
            var result = source.Select(s => new HotelVM
            {
                HotelID = s.HotelID,
                HotelName = s.HotelName,
                HotelEngName = s.HotelEngName,
                HotelCity = s.HotelCity,
                HotelAddress =  s.HotelAddress,/*s.HotelCity + "," + s.HotelDistrict + "," +*/
                HotelAbout = s.HotelAbout,
                Longitude = s.Longitude,
                Latitude = s.Latitude,
                Star = s.Star
            }).FirstOrDefault();

            return result;
        }

        //public IQueryable<RoomTypeVM> GetRoomType(string hotelId)
        public IEnumerable<RoomTypeVM> GetRoomTypeByFilter(string hotelId)
        {
            if (hotelId == null) hotelId = "hotel04";

            var result = _repository.GetRoomTypeByFilter(hotelId);
            foreach (var item in result)
            {
                foreach (var bed in item.RoomBed)
                {
                    switch (bed.Name)
                    {
                        case "雙人床":
                        case "加大雙人床":
                        case "單人床(兩床)":
                            item.Adult = item.Adult + (2 * bed.Amount);
                            item.Child = item.Child + (1 * bed.Amount);
                            break;

                        case "特大雙人床":
                            item.Adult = item.Adult + (2 * bed.Amount);
                            item.Child = item.Child + (2 * bed.Amount);
                            break;

                        case "上下舖":
                            item.Adult = item.Adult + (2 * bed.Amount);
                            item.Child = item.Child + 0;
                            break;

                        default:
                            break;
                    }
                }
            }


            return result;
        }

        //public RoomCheckOutData GetCheckOutData(string hotelId,string roomId)
        //{
        //    var hotel = _repository.GetHotel(hotelId);
        //    var allRoomType = GetRoomType(roomId);
        //    var selectedRoomType = from rt in allRoomType
        //                           where rt.RoomID == roomId
        //                           select rt;

        //    var checkOutData = from h in hotel
        //                       join rt in selectedRoomType on h.HotelID equals rt.HotelID
        //                       select new RoomCheckOutData
        //                       {
        //                           HotelID = h.HotelID,
        //                           HotelFullName = h.HotelName + " (" + h.HotelEngName + ")",
        //                           HotelAddress = h.HotelAddress,
        //                           RoomID = rt.RoomID,
        //                           RoomName = rt.RoomName,
        //                           BedType = "雙人床",
        //                           Breakfast = rt.Breakfast,
        //                           NoSmoking = rt.NoSmoking,
        //                           RoomPrice = rt.RoomPrice,
        //                           RoomDiscount = rt.RoomDiscount,
        //                           RoomNowPrice = rt.RoomNowPrice,
        //                           Adult = rt.Adult,
        //                           Child = rt.Child
        //                       };
        //    var B = checkOutData.FirstOrDefault();
        //    return checkOutData.FirstOrDefault();
        //}
    }
}