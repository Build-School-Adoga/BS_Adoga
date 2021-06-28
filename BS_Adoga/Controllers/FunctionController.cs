using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BS_Adoga.Service;

namespace BS_Adoga.Controllers
{
    public class FunctionController : Controller
    {
        [HotelLoginAuthorize]
        // GET: Function
        public ActionResult Index()
        {
            return View();
        }
    }
}