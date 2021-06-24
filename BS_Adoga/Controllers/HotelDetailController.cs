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

            DetailVM hotelDetail;

            if (hotelId != null)
                hotelDetail = service.GetDetailVM(hotelId);
            else if (TempData["search"] != null)
                hotelDetail = service.GetDetailVM(TempData["search"].ToString());
            else
                hotelDetail = service.GetDetailVM("hotel04");

            return View(hotelDetail);
        }

        //public ActionResult DetailAlbum()
        //{
        //    return View();
        //}
    }
}