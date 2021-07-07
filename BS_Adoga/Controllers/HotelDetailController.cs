using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels.HotelDetail;
using BS_Adoga.Models.ViewModels.CheckOut;
using BS_Adoga.Service;
using BS_Adoga.Repository;
using System.Net;

namespace BS_Adoga.Controllers
{
    public class HotelDetailController : Controller
    {
        //private AdogaContext _context;
        private HotelDetailService _service;
        public HotelDetailController()
        {
            //_context = new AdogaContext();
            _service = new HotelDetailService();
        }

        // GET: HotelDetail
        public ActionResult Detail(string hotelId, string startDate, string endDate, int orderRoom, int adult,int child)
        {
            DetailVM hotelDetail;

            if (hotelId != null)
                hotelDetail = _service.GetDetailVM(hotelId, startDate, endDate, orderRoom, adult,child);
            else if (TempData["search"] != null)
                hotelDetail = _service.GetDetailVM(hotelId, startDate, endDate, orderRoom, adult,child);
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                //hotelDetail = _service.GetDetailVM("hotel04"); //應該做報錯

            return View(hotelDetail);
        }

        public ActionResult SetCheckOutData(string hotelId,string roomId, string roomName,bool breakfast, string bedType , int adult,int child,
                                            int roomOrder ,decimal roomPrice,decimal roomDiscount , decimal roomNowPrice)
        {
            var hotel = _service.GetHotelById(hotelId);
            OrderVM orderData = new OrderVM(){
                roomCheckOutViewModel = new RoomCheckOutData
                {
                    HotelID = hotel.HotelID,
                    HotelFullName = hotel.HotelName + " (" + hotel.HotelEngName + ")",
                    Address = hotel.HotelAddress,
                    RoomID = roomId,
                    RoomName = roomName,
                    Breakfast = breakfast,
                    BedType = bedType,
                    Adult = 12,
                    Child = 2,
                    CountNight = 2,
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