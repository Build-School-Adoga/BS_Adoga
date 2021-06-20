using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels.HotelDetail;
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

        public IQueryable<Hotel> GetHotel(string hotelId)
        {
            var hotel = from h in _context.Hotels
                        where h.HotelID == hotelId
                        select h;

            return hotel;

        }

        public IQueryable<RoomTypeVM> GetRoomType(string hotelId)
        {
            var roomTypeData = from h in _context.Hotels
                               join r in _context.Rooms on h.HotelID equals r.HotelID
                               join bath in _context.BathroomTypes on r.TypesOfBathroomID equals bath.TypesOfBathroomID
                               join r_detail in _context.RoomsDetails on r.RoomID equals r_detail.RoomID
                               where h.HotelID == hotelId && r_detail.RoomCount - r_detail.RoomOrder >= 1
                               select new RoomTypeVM
                               {
                                   RoomID = r.RoomID,
                                   RoomName = r.RoomName,
                                   WiFi = r.WiFi,
                                   Breakfast = r.Breakfast,
                                   NoSmoking = r.NoSmoking,
                                   BathroomName = bath.Name,
                                   RoomBed = (from r_bed in _context.RoomBeds
                                              join bed in _context.BedTypes on r_bed.TypesOfBedsID equals bed.TypesOfBedsID
                                              where r_bed.RoomID == r.RoomID
                                              select new RoomBedVM
                                              {
                                                  Name = bed.Name,
                                                  Amount = r_bed.Amount
                                              }),
                                   Adult = 0,
                                   Child = 0,
                                   RoomPrice = r.RoomPrice,
                                   RoomDiscount = r_detail.RoomDiscount,
                                   RoomNowPrice = (r.RoomPrice * r_detail.RoomDiscount),
                                   RoomLeft = (r_detail.RoomCount - r_detail.RoomOrder)
                               };
            return roomTypeData;
        }

    }
}