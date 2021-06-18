
using BS_Adoga.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels.homeViewModels;

namespace BS_Adoga.Controllers
{
    public class HomeController : Controller
    {
        private AdogaContext _context;
        public HomeController()
        {
            _context = new AdogaContext();
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
      

        public ActionResult HomePage()
        {
            List<Card> cards = new List<Card>
            {
                new Card { name = "台北W酒店", area = "信義區", Evaluation = 2,  ground ="../Asset/images/Home/台北w酒店.jpg", sale = "2.5折扣", Originalprice = "NT$11,024", saleprice = "NT$5,500", fraction = "9.3", comment = "超棒", Quantity = "1198篇評鑑" },
                 new Card { name = "飛行家青年旅館", area= "苓雅區", Evaluation=  1, ground= "../Asset/images/Home/飛行家青年旅館.jpg", sale= "2.2折扣", Originalprice= "NT$2,370", saleprice= "NT$595", fraction= "8.8", comment= "很讚", Quantity= "1198篇評鑑" },
                  new Card  { name= "鈞怡大飯店", area= "高雄市", Evaluation=  3,  ground= "../Asset/images/Home/鈞怡大飯店.jpg", sale= "1.9折扣", Originalprice= "NT$6,234", saleprice= "NT$1,234", fraction= "8.9", comment= "很讚", Quantity= "1198篇評鑑" },
                  new Card  { name= "塩‧泊思行旅", area= "高雄市", Evaluation= 4,  ground= "../Asset/images/Home/塩‧泊思行旅.jpg", sale= "2折扣", Originalprice= "NT$3,611", saleprice= "NT$764", fraction= "8.4", comment= "很讚", Quantity= "1198篇評鑑"}

                 };
            //return View(cards);
            demoshopViewModels productss = new demoshopViewModels()
            {
                Cards = cards.ToList(),Hotels = _context.Hotels.ToList()
            };
            
            return View(productss);
           
        }

        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }
   
        public ActionResult Search(string HotelId)
        {
            var product = _context.Hotels.Find(HotelId);
            //var product = from p in _context.Hotels
            //              where p.HotelName == HotelName
            //              select p;
            if (product == null)
            {
                return RedirectToAction("Search");
            }
            return View(product);
        }
        public ActionResult ProductList()
        {
            var productss = _context.Hotels.ToList();
            return View(productss);
        }


        [HttpGet]
        public ActionResult SearchProduct()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SearchProduct(string productname)
        {
            var products = from p in _context.Hotels
                           where p.HotelID.Contains(productname)
                           select p;
            return View(products);
        }

        public ActionResult option()
        {
            var productss = _context.Hotels.ToList();
            return View(productss);
        }
    }
}