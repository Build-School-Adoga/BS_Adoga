using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BS_Adoga.Models.DBContext;
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

        //public IQueryable<Hotel> Hotel()
        //{
        //    var hotel = from h in _context.Hotels
        //                select h;

        //    return hotel;
        //}
        //public IQueryable<Room> Room()
        //{
        //    IQueryable<Hotel> h = Hotel();
        //    var room = from r in _context.Rooms
        //               //where r.HotelID == h.
        //               select r;

        //    return room;
        //}
        //public IQueryable<RoomsDetail> RoomDetail()
        //{
        //    var hotel = from h in _context.Hotels
        //                select h;

        //    return hotel;
        //}
        public IQueryable<Hotel> ALLHotel()
        {
            
            var hotel = from h in _context.Hotels
                        select h;

            return hotel;
        }

        public IQueryable<Hotel> GetHotels(string Name)
        {
            var hotel = from h in _context.Hotels
                        where h.HotelName.Contains(Name)
                        select h;

            return hotel;
        }
    }
}