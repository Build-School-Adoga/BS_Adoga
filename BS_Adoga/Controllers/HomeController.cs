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
            return View(images);

        }


        [HttpPost]
        public ActionResult Search(string search, string date_range, string people, string room, string data, string cardlocal)
        {
            string[] date = date_range.Split('-');
            string start = date[0];
            string end = date[1];

            string[] peo = people.Split('位');
            int adu = int.Parse(peo[0]);

            int kid = 0;
            if (peo[1].Split(',')[0] != "大人")
            {
                string[] str = peo[1].Split(',');
                kid = int.Parse(str[1]);
            }

            string[] rmo = room.Split('間');
            int rom = int.Parse(rmo[0]);

            //Irene變更： 因為If-else裡面都會用TempData且資料都一樣 所以把它抽出來(不需要重複2次)
            TempData["start"] = start;
            TempData["end"] = end;
            TempData["ple"] = adu;
            TempData["kid"] = kid;
            TempData["rom"] = rom;
            TempData["data"] = data;

            if (search.Length == 3)
            {
                TempData["search"] = search;

                //Irene變更：傳遞資料的型別更改至SearchDataViewModel
                SearchDataViewModel info = new SearchDataViewModel
                {
                    HotelNameOrCity = search,
                    CheckInDate = start,
                    CheckOutDate = end,
                    AdultCount = adu,
                    KidCount = kid,
                    RoomCount = rom
                };
                return RedirectToAction("Search", "Search", info);
            }
            else
            {
                //var xxx = from p in _homeService._homeRepository._context.Hotels
                //          where p.HotelName == search
                //          select p.HotelID;


                //TempData["search"] = xxx.FirstOrDefault();
                TempData["search"] = search;

                return RedirectToAction("Detail", "HotelDetail", new
                {
                    hotelName = TempData["search"],
                    startDate = TempData["start"],
                    endDate = TempData["end"],
                    orderRoom = TempData["rom"],
                    adult = TempData["ple"],
                    child = TempData["kid"]
                });

            }

            var card = _homeService.ALLImages(cardlocal);
            return View(card);
        }

        [HttpPost]
        public ActionResult Searchtwo(string cardlocal)
        {

            var card = _homeService.ALLImages(cardlocal);
            return View(card);
        }
    }

}