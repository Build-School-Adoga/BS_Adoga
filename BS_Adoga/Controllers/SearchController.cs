﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Service;

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
            //string search = TempData["search"].ToString();

            if(TempData["search"] == null)
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