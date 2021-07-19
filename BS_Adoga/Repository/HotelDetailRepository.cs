using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.SqlClient;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels.HotelDetail;
using BS_Adoga.Models.ViewModels.Search;
using System.ComponentModel.DataAnnotations;

namespace BS_Adoga.Repository
{
    public class HotelDetailRepository
    {
        private AdogaContext _context;

        public HotelDetailRepository()
        {
            _context = new AdogaContext();
        }

        public IQueryable<Hotel> GetHotelById(string hotelId)
        {
            var hotel = from h in _context.Hotels
                        where h.HotelID == hotelId
                        select h;

            return hotel;

        }

        public string GetHotelIdByName(string hotelName)
        {
            var hotelId = (from h in _context.Hotels
                        where h.HotelName.Contains(hotelName)
                        select h.HotelID).First();

            return hotelId;

        }

        public IQueryable<Facility> GetHotelFacilityById(string hotelId)
        {
            var facility = from h in _context.Hotels
                        join f in _context.Facilities on h.HotelID equals f.HotelID
                        where h.HotelID == hotelId
                        select f;

            return facility;

        }

        public IEnumerable<HotelSearchViewModel> GetEachHotelOneRoom(string CityName, string startDate, string endDate, int countNight, int orderRoom, int adult, int child)
        {
            int kidcountasadult = (child/ 2) + (child % 2);
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
                          group new { t } by new { t.HotelID,t.I_RoomVM.RoomID} into Group
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
                          }).GroupBy(x => x.HotelID, (key, g) => g.OrderBy(x => x.I_RoomVM.RoomPrice * (1-x.I_RoomDetailVM.RoomDiscount)).First());

            //RoomNowPrice = (r.RoomPrice * (1 - t2.Discount)),

            foreach (var a in table_3)
            {
                var aa= a;
                decimal total = a.I_RoomVM.RoomPrice * (1 - a.I_RoomDetailVM.RoomDiscount);
                var x = 0;
                //foreach (var b in a)
                //{
                //    var bb = b;
                //    decimal total = b.I_RoomVM.RoomPrice * (1 - b.I_RoomDetailVM.RoomDiscount);
                //    
                //}
            }
            return table;

        }


        public IEnumerable<RoomTypeVM> GetRoomTypeByFilter(string hotelId,DateTime startDate,DateTime endDate,int countNight, int orderRoom,int adult,int child,int totalPerson)
        {
            //1. 先找出符合條件的hotel 和 room
            var table = (from h in _context.Hotels
                            join r in _context.Rooms on h.HotelID equals r.HotelID
                            join r_detail in _context.RoomsDetails on r.RoomID equals r_detail.RoomID
                            where h.HotelID == hotelId && r.NumberOfPeople*orderRoom >= totalPerson && r_detail.CheckInDate >= startDate && r_detail.CheckInDate < endDate
                            select new { HotelID = h.HotelID, r, r_detail }).AsEnumerable();


            //2. 再針對找出來的hotel和room去計算那段日期內的剩餘房間數和折扣。
                //t 裡面除了HotelID 還有  r_detail (RoomDetail) 這2個表格  
            var table_2 = from t in table
                        group new { t.HotelID, t.r_detail } by new { t.HotelID, t.r_detail.RoomID, } into roomGroup
                        where roomGroup.Count() >= countNight && roomGroup.Min(r => r.r_detail.RoomCount - r.r_detail.RoomOrder)>=orderRoom 
                        select new
                        {
                            HotelID = roomGroup.Key.HotelID,
                            RoomID = roomGroup.Key.RoomID,
                            MinRoom = roomGroup.Min(r=>r.r_detail.RoomCount-r.r_detail.RoomOrder),
                            Discount = roomGroup.Sum(r => r.r_detail.RoomDiscount) / countNight,
                            //d= roomGroup.Sum(r => r.r_detail.RoomDiscount)
                        };

            //查看裡面每筆的總discount
            //var a = table_2.Select(d => d.d).ToList();
            //a.ForEach(s => { var b = s;
            //    var c = s; });

            //3.查詢出最終的結果並放進view Model
            var finalTable =  from t2 in table_2
                                 join r in _context.Rooms on t2.HotelID equals r.HotelID
                                 join bath in _context.BathroomTypes on r.TypesOfBathroomID equals bath.TypesOfBathroomID
                                 where r.RoomID == t2.RoomID
                                 orderby (r.RoomPrice * (1 - t2.Discount))
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
                                     Adult = 0,
                                     Child = 0,
                                     RoomOrder = orderRoom,
                                     RoomPrice = r.RoomPrice,
                                     RoomDiscount = 1 - t2.Discount,
                                     RoomNowPrice = (r.RoomPrice * (1 - t2.Discount)),
                                     RoomLeft = t2.MinRoom
                                 };
            return finalTable; 
            
            //foreach (var x in table_2)
            //{
            //    string h = x.HotelID;
            //    string room = x.RoomID;
            //    int min = x.MinRoom;
            //    decimal dis = x.Discount;
            //}
        }


    }
}