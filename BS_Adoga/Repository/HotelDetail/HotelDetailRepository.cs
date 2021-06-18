using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BS_Adoga.Models.DBContext;
using System.ComponentModel.DataAnnotations;

namespace BS_Adoga.Repository.HotelDetail
{
    public class HotelDetailRepository
    {
        private AdogaContext _context;

        public HotelDetailRepository()
        {
            _context = new AdogaContext();
        }

        public IQueryable<Hotel> GetHotel(string hotelName)
        {
            var hotel = from h in _context.Hotels
                        where h.HotelName == hotelName
                        select h;

            return hotel;

        }
    }
}