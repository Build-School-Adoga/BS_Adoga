using BS_Adoga.Models.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BS_Adoga.Models.ViewModels.CheckOut;

namespace BS_Adoga.Controllers
{
    public class CheckOutController : Controller
    {
        private AdogaContext _context;
        public CheckOutController()
        {
            _context = new AdogaContext();
        }

        // GET: CheckOut
        public ActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Index(CheckOutListViewModel model)
        {
            return View();
        }
    }
}