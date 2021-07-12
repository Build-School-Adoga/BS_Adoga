
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
        public ActionResult Search(string search, string date_range, string people, string room,string kid, string data, string cardlocal)
        {
            var date = date_range.Split('-');
            var start = date[0];
            var end = date[1];
            var peo = people.Split('位');
            var ple = peo[0];

          
            var rmo = room.Split('間');
            var rom = rmo[0];
            var dik = kid.Split('位');
            var kkk = dik[0];
            var ddd = kkk.Split(',');
            var kids = ddd[1];
            var Hotels = from p in _homeService._homeRepository._context.Hotels
                     where p.HotelCity == search
                     select p.HotelCity;

            //var peo = people.Split('位');
            //var adu = int.Parse(peo[0]);
            //var str = peo[1].Split(',');
            //var kid = int.Parse(str[1]);

            //Irene更新: 稍微把人數的部分改了一些
            var human = people.Split(',');
            var a = human[0].Split('位');
            var adu = int.Parse(a[0]);
            var kid = 0;
            
            // if (Hotels.Count() >0)
            if (human.Length > 1)
            {
                var b = human[1].Split('位');
                kid = int.Parse(b[0]);
            }

            var rmo = room.Split('間');
            var rom = int.Parse(rmo[0]);

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
                    /*TempData["search"] = search;                              
                    TempData["start"] = start;
                    TempData["end"] = end;
                    TempData["ple"] = ple;
                    TempData["rom"] = rom;
                    TempData["data"] = data;
                    TempData["kids"] = kids;
            
            
               
                return RedirectToAction("Search", "Search", new { search = TempData["search"] ,
                    start= TempData["start"] ,
                    end= TempData["end"],
                    ple= TempData["ple"],
                    rom= TempData["rom"],
                    data= TempData["data"],
                    kids=TempData["kids"]
            });*/
            }
            else
            {
                var xxx = from p in _homeService._homeRepository._context.Hotels
                          where p.HotelName == search
                          select p.HotelID;

                   
                    TempData["kids"] = kids;
            
                TempData["start"] = start;
                TempData["end"] = end;
                TempData["ple"] = ple;
                TempData["rom"] = rom;
                TempData["data"] = data;
                
                TempData["search"] = xxx.FirstOrDefault();

                return RedirectToAction("Detail", "HotelDetail",new {
                    hotelId = TempData["search"],
                    startDate = TempData["start"],
                    endDate = TempData["end"],
                    orderRoom = TempData["rom"],
                    adult = TempData["ple"],
                    child = TempData["kids"]
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