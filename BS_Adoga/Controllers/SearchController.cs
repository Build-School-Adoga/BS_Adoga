using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Service.Search;
using BS_Adoga.Models.ViewModels.homeViewModels;
using BS_Adoga.Models.ViewModels.Search;

namespace BS_Adoga.Controllers
{
    public class SearchController : Controller
    {
        private SearchCardService s;
        public SearchController()
        {
            s = new SearchCardService();
        }

        //public ActionResult Search()
        //{
        //    //var products = s.GetHomeByFilter();
        //    //return View(products);
        //    return View();
        //}

        //[HttpPost]
        public ActionResult Search(SearchCardViewModel searchVM)
        {
            //string search = TempData["search"].ToString();
            //SearchCardViewModel a = new SearchCardViewModel()
            //{
            //    var room = s.GetHomeByFilter(search);
            //    return View(room);
            //}
            s.GetListToFilter();

            var a=1;
            if (TempData["search"] == null)
            {
                var hotels = s.ALLHotel();
                return View(hotels);
            }
            else
            {
                var hotels = s.GetHotels(TempData["search"].ToString());
                return View(hotels);
            }

        }

    }
}