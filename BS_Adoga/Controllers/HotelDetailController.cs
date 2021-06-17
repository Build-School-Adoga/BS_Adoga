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
        public ActionResult Detail(string hotelName)
        {
            //var hotel = from h in _context.Hotels
            //            join r in _context.Rooms
            //            on h.HotelID equals r.HotelID
            //            where h.HotelName == "台中商旅 (Hung's Mansion)"
            //            select new HotelViewModel { HotelID = h.HotelID,HotelName = h.HotelName,HotelAddress=h.HotelAddress,Star=h.Star };

            //return View(hotel.FirstOrDefault());

            HotelDetailService service = new HotelDetailService();

            var hotel = service.GetHotel(hotelName);

            return View(hotel);
        }

        //public ActionResult DetailAlbum()
        //{
        //    return View();
        //}
    }
}