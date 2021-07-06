
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
            var products = _homeService.GetHomeByFilter();

            return View(products);

        }
        [HttpPost]
        public ActionResult HomePage(demoshopViewModels productss)
        {
            //string name = Request.Form["label"];

            //ViewData["w"] = "name";
            return View();

        }

        
        [HttpPost]
        public ActionResult Search(string search,string time)
        {
            if (search.Length ==3)
            {
                TempData["search"] = search;
                

                return RedirectToAction("Search", "Search", search);
            }
            else {
                var xxx = from p in _homeService._homeRepository._context.Hotels
                          where p.HotelName == search
                          select p.HotelID;


                TempData["search"] = xxx.FirstOrDefault();

                return RedirectToAction("Detail", "HotelDetail", search);
            }

           
        }
       
     
    }

}