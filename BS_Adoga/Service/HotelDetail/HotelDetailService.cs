using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels.HotelDetail;
using BS_Adoga.Repository.HotelDetail;

namespace BS_Adoga.Service.HotelDetail
{
    public class HotelDetailService
    {
        private HotelDetailRepository _repository;

        public HotelDetailService()
        {
            _repository = new HotelDetailRepository();
        }

        public HotelViewModel GetHotel(string hotelName)
        {
            if (hotelName == null) hotelName = "台中商旅 (Hung's Mansion)";

            var source = _repository.GetHotel(hotelName);

            var result = source.Select(s => new HotelViewModel
            {
                HotelID = s.HotelID,
                HotelName = s.HotelName,
                HotelEngName = s.HotelEngName,
                HotelAddress = s.HotelCity + "," + s.HotelDistrict + "," + s.HotelAddress,
                HotelAbout = s.HotelAbout,
                Longitude = s.Longitude,
                Latitude = s.Latitude,
                Star = s.Star
            }).FirstOrDefault();

            return result;
        }
    }
}