using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels.HotelDetail;

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
        public ActionResult Detail()
        {
            var s = TempData["search"];
            var hotel = from h in _context.Hotels
                        join r in _context.Rooms
                        on h.HotelID equals r.HotelID
                        where h.HotelName == s
                        select new HotelDetail { HotelID = h.HotelID,HotelName = h.HotelName,HotelAddress=h.HotelAddress,Star=h.Star };

            return View(hotel.FirstOrDefault());
        }

        //public ActionResult DetailAlbum()
        //{
        //    return View();
        //}
    }
}