using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels.HotelDetail;
using BS_Adoga.Models.ViewModels.CheckOut;
using BS_Adoga.Models.ViewModels.Search;
using BS_Adoga.Service;
using BS_Adoga.Repository;
using System.Net;

namespace BS_Adoga.Controllers
{
    public class HotelDetailController : Controller
    {
        //private AdogaContext _context;
        private HotelDetailService _service;
        private HotelDetailRepository _repository;
        public HotelDetailController()
        {
            //_context = new AdogaContext();
            _service = new HotelDetailService();
            _repository = new HotelDetailRepository();
        }

        // GET: HotelDetail
        public ActionResult Detail(string hotelName = "台中商旅", string startDate = "2021-06-20", string endDate = "2021-06-22", int orderRoom = 2, int adult = 1, int child = 0)
        {
            //ViewData["CityOrName"] = hotelName;
            //ViewData["sDate"] = startDate;
            //ViewData["end"] = endDate;
            //ViewData["adult"] = adult;
            //ViewData["kid"] = child;
            //ViewData["room"] = orderRoom;
            DateTime checkInDate = DateTime.Parse(startDate);
            DateTime checkOutDate = DateTime.Parse(endDate);
            SearchByMemberVM searchData = new SearchByMemberVM()
            {
                CityOrHotel = hotelName,
                CheckInDate = startDate,
                CheckOutDate = endDate,
                RoomOrder = orderRoom,
                CountNight = new TimeSpan(checkOutDate.Ticks - checkInDate.Ticks).Days,
                Adult = adult,
                Child = child
            };

            TempData["SearchData"] = searchData;            

            DetailVM hotelDetail;
            string hotelId = _repository.GetHotelIdByName(hotelName);

            if (hotelId != null)
                hotelDetail = _service.GetDetailVM(hotelId, startDate, endDate, orderRoom, adult, child);
            else if (TempData["search"] != null)
                hotelDetail = _service.GetDetailVM(hotelId, startDate, endDate, orderRoom, adult, child);
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //hotelDetail = _service.GetDetailVM("hotel04"); //應該做報錯

            return View(hotelDetail);
        }

        public ActionResult GetTempData(string search, string date_range, string people, string room)
        {
            var human = people.Split(',');
            var a = human[0].Split('位');
            var b = human[1].Split('位');
            var adult = int.Parse(a[0]);
            var kid = int.Parse(b[0]);

            var r = room.Split('間');

            var date = date_range.Split('-');
            var start = date[0];
            var end = date[1];

            return RedirectToAction("Detail", new
            {
                hotelName = search,
                startDate = start,
                endDate = end,
                orderRoom = int.Parse(r[0]),
                adult = adult,
                child = kid
            });
        }

        
        [HttpGet]
        public ActionResult SetCheckOutData( string hotelId, string roomId,string roomName,bool noSmoking,bool breakfast,string bedType,
                                                int roomOrder,decimal roomPrice,decimal roomDiscount, decimal roomNowPrice)
        {
            var hotel = _service.GetHotelById(hotelId);
            var searchData = (SearchByMemberVM)TempData["SearchData"];
            TempData.Keep("SearchData");
            
            //DateTime checkInDate = DateTime.Parse(searchData.CheckInDate);
            //DateTime checkOutDate = DateTime.Parse(searchData.CheckOutDate);
            //int adult = searchData.Adult;
            //int child = searchData.Child;

            OrderVM orderData = new OrderVM()
            {
                roomCheckOutViewModel = new RoomCheckOutData
                {
                    HotelID = hotel.HotelID,
                    HotelFullName = hotel.HotelName + " (" + hotel.HotelEngName + ")",
                    Address = hotel.HotelAddress,
                    RoomID = roomId,
                    RoomName = roomName,
                    NoSmoking = noSmoking,
                    Breakfast = breakfast,
                    BedType = bedType,
                    CheckInDate = searchData.CheckInDate,
                    CheckOutDate = searchData.CheckOutDate,
                    Adult = searchData.Adult,
                    Child = searchData.Child,
                    CountNight = searchData.CountNight,
                    RoomOrder = roomOrder,
                    RoomPrice = roomPrice,
                    Discount = roomDiscount,
                    TotalPrice = roomNowPrice
                }
            };

            TempData["orderData"] = orderData;

            return RedirectToAction("Index", "CheckOut");
        }

        //public ActionResult DetailAlbum()
        //{
        //    return View();
        //}
    }
}