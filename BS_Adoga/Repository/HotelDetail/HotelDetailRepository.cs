using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.SqlClient;
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

        //func名稱:GetHotelById
        public IQueryable<Hotel> GetHotelById(string hotelId)
        {
            var hotel = from h in _context.Hotels
                        where h.HotelID == hotelId
                        select h;

            return hotel;

        }

        public IEnumerable<RoomTypeVM> GetRoomTypeByFilter(string hotelId)
        {
            int orderDay = 3;
            string[] pickDate = new string[] { "2021-06-20", "2021-06-21", "2021-06-22" };
            int orderPerson = 6;
            int orderRoom = 3;

            var table = (from h in _context.Hotels
                            join r in _context.Rooms on h.HotelID equals r.HotelID
                            join r_detail in _context.RoomsDetails on r.RoomID equals r_detail.RoomID
                            where h.HotelID == hotelId
                            select new { HotelID = h.HotelID, r, r_detail }).AsEnumerable();

            //t 裡面除了HotelID 還有  r_detail (RoomDetail) 這2個表格  
            var table_2 = from t in table
                        join pick in pickDate on t.r_detail.CheckInDate.ToString("yyyy-MM-dd") equals pick
                        group new { t.HotelID, t.r_detail } by new { t.HotelID, t.r_detail.RoomID, } into roomGroup
                        where roomGroup.Count() >= orderDay && roomGroup.Min(r => r.r_detail.RoomCount - r.r_detail.RoomOrder)>=orderRoom
                        select new
                        {
                            HotelID = roomGroup.Key.HotelID,
                            RoomID = roomGroup.Key.RoomID,
                            MinRoom = roomGroup.Min(r=>r.r_detail.RoomCount-r.r_detail.RoomOrder),
                            Discount = roomGroup.Sum(r => r.r_detail.RoomDiscount) / orderDay,
                        };

            foreach (var x in table_2)
            {
                string h = x.HotelID;
                string room = x.RoomID;
                int min = x.MinRoom;
                decimal dis = x.Discount;
            }

            var finalTable =  from t2 in table_2
                                 join r in _context.Rooms on t2.HotelID equals r.HotelID
                                 join bath in _context.BathroomTypes on r.TypesOfBathroomID equals bath.TypesOfBathroomID
                                 where r.RoomID == t2.RoomID
                                 select new RoomTypeVM
                                 {
                                     HotelID = r.HotelID,
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
                                     Adult = r.NumberOfPeople,
                                     Child = r.NumberOfPeople/2,
                                     RoomPrice = r.RoomPrice,
                                     RoomDiscount = 1 - t2.Discount,
                                     RoomNowPrice = (r.RoomPrice * (1 - t2.Discount)),
                                     RoomLeft = t2.MinRoom
                                 };

            var roomTypeData = from h in _context.Hotels
                               join r in _context.Rooms on h.HotelID equals r.HotelID
                               join bath in _context.BathroomTypes on r.TypesOfBathroomID equals bath.TypesOfBathroomID
                               join r_detail in _context.RoomsDetails on r.RoomID equals r_detail.RoomID
                               where h.HotelID == hotelId && r_detail.RoomCount - r_detail.RoomOrder >= orderRoom && r.NumberOfPeople >= (orderPerson / orderRoom)
                               select new RoomTypeVM
                               {
                                   HotelID = h.HotelID,
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
                                   RoomDiscount = 1 - r_detail.RoomDiscount,
                                   RoomNowPrice = (r.RoomPrice * (1 - r_detail.RoomDiscount)),
                                   RoomLeft = (r_detail.RoomCount - r_detail.RoomOrder)
                               };

            //return roomTypeData; //Iqueryable型別
            return finalTable;
        }


    }
}