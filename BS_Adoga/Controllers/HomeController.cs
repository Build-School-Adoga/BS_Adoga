﻿
using BS_Adoga.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels.homeViewModels;
using BS_Adoga.Service;
using Microsoft.Ajax.Utilities;
using System.Security.Cryptography;
using System.Web.WebPages;
using BS_Adoga.Models.ViewModels.Search;

namespace BS_Adoga.Controllers
{
    public class HomeController : Controller
    {
        private HomeService _homeService;
        public HomeController()
        {
            _homeService = new HomeService();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult HomePage()
        {
            var images = _homeService.ALLImages2();

            return View(images);

        }
        [HttpPost]
        public ActionResult HomePage(string cardlocal)
        {
            var images = _homeService.ALLImages(cardlocal);
            ViewBag.Error = "這是錯誤訊息";
            return PartialView("_SimpleCardPartial", images);

        }
        [HttpPost]
        public ActionResult Search2(string search)
        {
            SearchDataViewModel info = new SearchDataViewModel
            {
                HotelNameOrCity = search
              
            };
            return RedirectToAction("Search", "Search", info);
        }
        [HttpPost]
        public ActionResult Search(string search, string date_range, string people, string room, string kid, string data)
        {
            var date = date_range.Split('-');
            var start = date[0];
            var end = date[1];

            var Hotels = from p in _homeService._homeRepository._context.Hotels
                         where p.HotelCity == search
                         select p.HotelCity;
            //Irene更新: 稍微把人數的部分改了一些
            var human = people.Split(',');
            var a = human[0].Split('位');
            var adu = int.Parse(a[0]);
            var kids = 0;
            if (human.Length > 1)
            {
                var b = human[1].Split('位');
                kids = int.Parse(b[0]);
            }

            var rmo = room.Split('間');
            var rom = int.Parse(rmo[0]);

            //Irene變更： 因為If - else裡面都會用TempData且資料都一樣 所以把它抽出來(不需要重複2次)
            TempData["start"] = start;
            TempData["end"] = end;
            TempData["ple"] = adu;
            TempData["kid"] = kids;
            TempData["rom"] = rom;
            TempData["data"] = data;
            TempData["search"] = search;
            if (Hotels.Count() > 0)
            {
                //TempData["search"] = search;

                //Irene變更：傳遞資料的型別更改至SearchDataViewModel
                SearchDataViewModel info = new SearchDataViewModel
                {
                    HotelNameOrCity = search,
                    CheckInDate = start,
                    CheckOutDate = end,
                    AdultCount = adu,
                    KidCount = kids,
                    RoomCount = rom
                };
                return RedirectToAction("Search", "Search", info);
            }
            TempData["search"] = search;

            return RedirectToAction("HotelDetail", "HotelDetail", new
            {
                hotelName = TempData["search"],
                startDate = TempData["start"],
                endDate = TempData["end"],
                orderRoom = TempData["rom"],
                adult = TempData["ple"],
                child = TempData["kid"]
            });


        }
        public ActionResult ServiceCenter()
        {

            return View();

        }
        public ActionResult CityCenter()
        {

            var images = _homeService.ALLImages2();

            return View(images);

        }
    }
}