
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
            var date = date_range.Split('-');
            var start = date[0];
            var end = date[1];
            var peo = people.Split('位');
            var ple = peo[0];
            var rmo = room.Split('間');
            var rom = rmo[0];



            if (search.Length == 3)
            {
                TempData["search"] = search;


                TempData["start"] = start;
                TempData["end"] = end;
                TempData["ple"] = ple;
                TempData["rom"] = rom;
                TempData["data"] = data;
                return RedirectToAction("Search", "Search", search);
            }
            else
            {
                var xxx = from p in _homeService._homeRepository._context.Hotels
                          where p.HotelName == search
                          select p.HotelID;


                TempData["start"] = start;
                TempData["end"] = end;
                TempData["ple"] = ple;
                TempData["rom"] = rom;
                TempData["data"] = data;
                TempData["search"] = xxx.FirstOrDefault();

                return RedirectToAction("Detail", "HotelDetail", search);
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