using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels.HotelLogin;
using BS_Adoga.Models.ViewModels.MemberLogin;
using BS_Adoga.Repository;
using BS_Adoga.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


//Hotel-Back-End
namespace BS_Adoga.Controllers
{
    [HotelLoginAuthorize]
    public class FunctionController : Controller
    {
        private AdogaContext _context;
        private FunctionRepository _repository;
        private FunctionService _service;

        public FunctionController()
        {
            _context = new AdogaContext();
            _repository = new FunctionRepository(_context);
            _service = new FunctionService();
        }

        // GET: Function
        //public ActionResult Index2()
        //{
        //    string UserCookiedataJS = ((FormsIdentity)HttpContext.User.Identity).Ticket.UserData;
        //    UserCookieViewModel UserCookie = JsonConvert.DeserializeObject<UserCookieViewModel>(UserCookiedataJS);
        //    string id = UserCookie.Id;
        //    string picture = UserCookie.PictureUrl;
        //    ViewBag.id = id;
        //    ViewBag.pictureurl = picture;
        //    return View();
        //}
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HotelIndex()
        {
            return View(_repository.GetHotelList());
        }
        public ActionResult HotelCreate()
        {
            //string URL = "https://graph.facebook.com/me?access_token=";
            //string JSON = GetWebRequest(URL);
            //dynamic json = JValue.Parse(JSON);
            //string name = json.name;

            List<SimpleZipCodeVM> Citys = JsonConvert.DeserializeObject<List<SimpleZipCodeVM>>(_service.CityJSON());
            List<SelectListItem> firstitems = new List<SelectListItem>();
            List<SelectListItem> seconditems = new List<SelectListItem>();
            foreach (var item in Citys)
            {
                firstitems.Add(new SelectListItem()
                {
                    Text = item.city,
                    Value = item.city
                });
                if (item.city == "臺北市")
                {
                    foreach (var item2 in item.districts)
                        seconditems.Add(new SelectListItem()
                        {
                            Text = item2.district,
                            Value = item2.district
                        });
                }
            }
            ViewBag.firstitems = firstitems;
            ViewBag.seconditems = seconditems;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HotelCreate(HotelCreateViewModel HotelCreateVM)
        {
            string username = User.Identity.Name;
            if (ModelState.IsValid)
            {
                Hotel hotel = new Hotel() 
                {
                    HotelID = HotelCreateVM.HotelID,
                    HotelName = HotelCreateVM.HotelName,
                    HotelEngName = HotelCreateVM.HotelEngName,
                    HotelCity = HotelCreateVM.HotelCity,
                    HotelDistrict = HotelCreateVM.HotelDistrict,
                    HotelAddress = HotelCreateVM.HotelAddress,
                    HotelAbout = HotelCreateVM.HotelAbout,
                    Longitude = HotelCreateVM.Longitude,
                    Latitude = HotelCreateVM.Latitude,
                    Star = HotelCreateVM.Star,
                    Logging = "建立" + "," + username + "," + DateTime.Now.ToString()
                };

                _context.Hotels.Add(hotel);
                _context.SaveChanges();
                return RedirectToAction("HotelIndex");
            }
            return View(HotelCreateVM);
        }

        public ActionResult HotelLList()
        {
            return View(_context.Hotels.ToList());
        }

        public ActionResult SecondItems(string city)
        {
            List<SimpleZipCodeVM> Citys = JsonConvert.DeserializeObject<List<SimpleZipCodeVM>>(_service.CityJSON());
            List<SelectListItem> firstitems = new List<SelectListItem>();
            List<SelectListItem> seconditems = new List<SelectListItem>();
            foreach (var item in Citys)
            {
                firstitems.Add(new SelectListItem()
                {
                    Text = item.city,
                    Value = item.city
                });
                if (item.city == city)
                {
                    foreach (var item2 in item.districts)
                        seconditems.Add(new SelectListItem()
                        {
                            Text = item2.district,
                            Value = item2.district
                        });
                }
            }

            var sop = seconditems;
            TagBuilder tb = new TagBuilder("select");
            tb.GenerateId("Select2");
            tb.MergeAttribute("name", "HotelDistrict");
            foreach (var item in sop)
            {
                tb.InnerHtml += "<option value='" + item.Value.ToString() + "'>" + item.Text.ToString() + "</option>";

            }
            return Content(tb.ToString());

        }
    }
}