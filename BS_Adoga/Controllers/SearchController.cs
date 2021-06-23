using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BS_Adoga.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        //[Authorize]
        public ActionResult Search()
        {
            return View();
        }
    }
}