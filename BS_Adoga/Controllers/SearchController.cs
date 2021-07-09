using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Service;
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
        public ActionResult GetTempData(string search, string start, string end, string people, int room)
        {
            var human = people.Split(',');
            var a = human[0].Split('位');
            var b = human[1].Split('位');
            var adult = int.Parse(a[0]);
            var kid = int.Parse(b[0]);

            //TempData["CityOrName"] = search;
            //TempData["sdate"] = start;
            //TempData["end"] = end;
            //TempData["adult"] = int.Parse(adult);
            //TempData["kid"] = int.Parse(kid);
            //TempData["room"] = int.Parse(room);

            SearchDataViewModel info = new SearchDataViewModel
            {
                HotelNameOrCity = search,
                CheckInDate = start,
                CheckOutDate = end,
                AdultCount = adult,
                KidCount = kid,
                RoomCount = room
            };

            return RedirectToAction("Search",info);
        }

        //[HttpGet]
        public ActionResult Search(SearchDataViewModel info/*string cityOrName, string startDate, string endDate, int nRoom, int nAdult, int nKid*/)
        {
            ViewData["CityOrName"] = info.HotelNameOrCity;
            ViewData["sDate"] = info.CheckInDate;
            ViewData["end"] = info.CheckOutDate;
            ViewData["adult"] = info.AdultCount;
            ViewData["kid"] = info.KidCount;
            ViewData["room"] = info.RoomCount;

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
            var data = s.GetSearchViewModelData(info/*cityOrName, startDate,endDate,nRoom,nAdult,nKid*/);
                return View(data);
            //}

        }

    }
}