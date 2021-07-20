using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BS_Adoga.Service;
using System.Web.Security;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels.MemberLogin;
using Newtonsoft.Json;
using BS_Adoga.Repository;
using BS_Adoga.Models.ViewModels.Account;

namespace BS_Adoga.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private MemberAccountService _service;
        private MemberAccountRepository _memberacoountrepository;

        public AccountController()
        {
            _service = new MemberAccountService();
            _memberacoountrepository = new MemberAccountRepository();
        }

        [AcceptVerbs("GET")]
        public ActionResult GetMemberBookingList(string filterOption,string sortOption)
        {
            string UserCookiedataJS = ((FormsIdentity)HttpContext.User.Identity).Ticket.UserData;
            UserCookieViewModel UserCookie = JsonConvert.DeserializeObject<UserCookieViewModel>(UserCookiedataJS);
            string user_id = UserCookie.Id;

            var data = _service.GetBookingOrder_FilterSort(user_id, filterOption, sortOption);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // GET: Account
        public ActionResult MemberBooking()
        {
            ViewBag.MemberCurrentPage = "booking";
            string UserCookiedataJS = ((FormsIdentity)HttpContext.User.Identity).Ticket.UserData;
            UserCookieViewModel UserCookie = JsonConvert.DeserializeObject<UserCookieViewModel>(UserCookiedataJS);
            string user_id = UserCookie.Id;

            return View(_memberacoountrepository.GetBookingDESC(user_id));
        }
        public ActionResult MemberProfile()
        {
            //string member = User.Identity.Name;
            //var data = _service.GetMember(member);
            ViewBag.MemberCurrentPage = "profile";
            string UserCookiedataJS = ((FormsIdentity)HttpContext.User.Identity).Ticket.UserData;
            UserCookieViewModel UserCookie = JsonConvert.DeserializeObject<UserCookieViewModel>(UserCookiedataJS);
            string user_id = UserCookie.Id;
            AdogaContext db = new AdogaContext();
            //string user_id = ((FormsIdentity)HttpContext.User.Identity).Ticket.UserData;
            //var data = from m in db.Customers
            //           where m.CustomerID == user_id
            //           select m;

            var data = db.Customers.Where(x => x.CustomerID == user_id).FirstOrDefault();
            return View(data);



            //使用表單驗證來進行使用者身份識別，當Http的連線字串等於使用者的真確性，回傳表單驗證的使用者特定資料(使用者特定字串)
            //字串 id = ((使用表單驗證來進行使用者的身份識別) Http連接字串.使用者.身份識別).取得表單驗證的使用者身份識別.取得存放使用者的特定字串
            //string id = ((FormsIdentity)HttpContext.User.Identity).Ticket.UserData;

            //return View();
        }

        
        public ActionResult BookingDetail(string orderid)
        {
            string UserCookiedataJS = ((FormsIdentity)HttpContext.User.Identity).Ticket.UserData;
            UserCookieViewModel UserCookie = JsonConvert.DeserializeObject<UserCookieViewModel>(UserCookiedataJS);
            string user_id = UserCookie.Id;


            return View(_memberacoountrepository.GetBookingDetail(orderid, user_id));
        }


        public ActionResult RePayOrder(RePayViewModel rePay, string orderid)
        {
            string UserCookiedataJS = ((FormsIdentity)HttpContext.User.Identity).Ticket.UserData;
            UserCookieViewModel UserCookie = JsonConvert.DeserializeObject<UserCookieViewModel>(UserCookiedataJS);
            string user_id = UserCookie.Id;

            var reOdId = _memberacoountrepository.GetReOrder(orderid, user_id);
            rePay.OrderID = orderid;
            rePay.HotelName = reOdId.HotelName;
            rePay.RoomPriceTotal = reOdId.RoomPriceTotal;
            rePay.RoomQuantity = reOdId.RoomQuantity;


            TempData["ReOrderData"] = rePay;

            if (ModelState.IsValid)
            {
                //EF
                try
                {
                    return RedirectToAction("PayAPI", "CheckOut");
                }
                catch (Exception ex)
                {
                    return Content("訂單建立失敗:" + ex.ToString());
                }
            }
            return View();
        }
    }
}