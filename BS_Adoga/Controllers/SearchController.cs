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
        public ActionResult GetTempData(string CityOrName)
        {
            
            TempData["CityOrName"]=CityOrName;
            return RedirectToAction("Search", CityOrName);
        }

        //[HttpGet]
        public ActionResult Search(string cityOrName/*, string startDate, string endDate, int nRoom, int nAdult, int nKid*/)
        {
            cityOrName = (string)TempData["search"];
            ViewData["con"] = cityOrName;
            ViewData["sdate"] = TempData["start"];
            ViewData["end"] = TempData["end"];
            ViewData["room"] = TempData["rom"];
            ViewData["people"] = TempData["ple"];
            //ViewBag["cityorname"] = TempData["CityOrName"];
            //string search = TempData["search"].ToString();
            //SearchCardViewModel a = new SearchCardViewModel()
            //{
            //    var room = s.GetHomeByFilter(search);
            //    return View(room);
            //}
            //s.GetListToFilter();

            //if (TempData["search"] == null)
            //{
            //    //var hotels = s.ALLHotel();
            //    //return View(hotels);

            //    var hotels = s.GetSearchViewModelData("");
            //    return View(hotels);

            //}
            //else
            //{
            //var hotels = s.GetHotels(TempData["search"].ToString());
            //return View(hotels);



            //var data = s.GetSearchViewModelData(TempData["Search"].ToString());
            var data = s.GetSearchViewModelData(cityOrName/*, startDate,endDate,nRoom,nAdult,nKid*/);
                return View(data);
            //}

        }

    }
}