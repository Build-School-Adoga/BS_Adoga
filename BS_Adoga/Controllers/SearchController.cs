using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Service.Search;

namespace BS_Adoga.Controllers
{
    public class SearchController : Controller
    {
        private SearchCardService s;
        public SearchController()
        {
            s = new SearchCardService();
        }
        
        
        public ActionResult Search()
        {
            var hotels = s.ALLHotel();

            return View(hotels);
        }


        //public ActionResult SearchHotel()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult SearchHotel(string Name)
        //{
        //    var hotels = s.GetHotels(Name);

        //    //防呆
        //    if (hotels == null)
        //    {
        //        //这里的Search是会回去找Get的那个Search
        //        return RedirectToAction("Search");
        //    }

        //    return View();
        //}
    }
}