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
        [ValidateAntiForgeryToken]
        public ActionResult Index(CheckOutListViewModel orderVM)
        {
            if (ModelState.IsValid)
            {
                //1.View Model(RegisterViewModel) --> Data Model (HotelEmployee)
                string firstname = HttpUtility.HtmlEncode(orderVM.FirstName);
                string lastname = HttpUtility.HtmlEncode(orderVM.LastName);
                string email = HttpUtility.HtmlEncode(orderVM.Email);
                string ConfirmEmail = HttpUtility.HtmlEncode(orderVM.ConfirmEmail);
                string phonenumber = HttpUtility.HtmlEncode(orderVM.PhoneNumber);

                Order od = new Order()
                {
                    OrderID = DateTime.Now.ToString(),
                    CustomerID = "test@fff",
                    RoomID = "room01",
                    OrderDate = DateTime.Now,
                    CheckInDate = DateTime.Now,
                    CheckOutDate = DateTime.Now,
                    RoomCount = 1,
                    RoomPriceTotal = 3000,
                    FirstName = firstname,
                    LastName = lastname,
                    Email = email,
                    PhoneNumber = phonenumber,
                    Country = "台灣",
                    SmokingPreference = "禁菸房",
                    BedPreference = "大床",
                    ArrivingTime = DateTime.Now.ToString(),
                    Logging = "建立" + "," + firstname + lastname + "," + DateTime.Now.ToString(),
                };

                //EF
                try
                {
                    _context.Orders.Add(od);
                    _context.SaveChanges();

                    return Content("訂單建立成功");
                }
                catch (Exception ex)
                {
                    return Content("訂單建立失敗:" + ex.ToString());
                }
            }

            return View(orderVM);
        }
    }
}