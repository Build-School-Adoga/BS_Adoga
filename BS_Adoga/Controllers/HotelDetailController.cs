using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels.HotelDetail;
using BS_Adoga.Service.HotelDetail;

namespace BS_Adoga.Controllers
{
    public class HotelDetailController : Controller
    {
        private AdogaContext _context;
        public HotelDetailController()
        {
            _context = new AdogaContext();
        }

        // GET: HotelDetail
        public ActionResult Detail(string hotelId)
        {
            HotelDetailService service = new HotelDetailService();

            string search = "hotel04";

            if (TempData["search"] != null)
                search = TempData["search"].ToString();

            DetailVM hotelDetail = new DetailVM()
            {
                hotelVM = service.GetHotel(search),
                roomTypeVM = service.GetRoomType(search)
            };

            return View(hotelDetail);
        }



        //public ActionResult DetailAlbum()
        //{
        //    return View();
        //}
    }
}