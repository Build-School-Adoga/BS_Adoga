using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels.HotelDetail;
using BS_Adoga.Models.ViewModels.CheckOut;
using BS_Adoga.Service.HotelDetail;

namespace BS_Adoga.Controllers
{
    public class HotelDetailController : Controller
    {
        private AdogaContext _context;
        private HotelDetailService _service;
        public HotelDetailController()
        {
            _context = new AdogaContext();
            _service = new HotelDetailService();
        }

        // GET: HotelDetail
        public ActionResult Detail(string hotelId)
        {
            DetailVM hotelDetail;

            if (hotelId != null)
                hotelDetail = _service.GetDetailVM(hotelId);
            else if (TempData["search"] != null)
                hotelDetail = _service.GetDetailVM(TempData["search"].ToString());
            else
                hotelDetail = _service.GetDetailVM("hotel04");

            return View(hotelDetail);
        }

        public ActionResult SetCheckOutData(string hotelId,string roomId, string roomName,bool breakfast, string bedType , int adult,int child,
                                            int roomOrder ,decimal roomPrice,decimal roomDiscount , decimal roomNowPrice)
        {
            //var a = _service.GetCheckOutData(hotelId, roomId);
            //TempData["Order"] = _service.GetCheckOutData(hotelId,roomId);
            var hotel = _service.GetHotel(hotelId);
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
                    Adult = adult,
                    Child = child,
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