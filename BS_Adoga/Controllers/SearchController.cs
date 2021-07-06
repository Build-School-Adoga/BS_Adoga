using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Service;
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

        //[HttpPost]
        public ActionResult Search(SearchCardViewModel searchVM)
        {
            //string search = TempData["search"].ToString();

            if (TempData["search"] == null)
            {
                //var hotels = s.ALLHotel();
                //return View(hotels);

                var hotels = s.GetSearchViewModelData("");
                return View(hotels);

            }
            else
            {
                //var hotels = s.GetHotels(TempData["search"].ToString());
                //return View(hotels);

                var data = s.GetSearchViewModelData(TempData["Search"].ToString());
                return View(data);
            }

        }

    }
}