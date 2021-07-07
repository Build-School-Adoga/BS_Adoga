﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BS_Adoga.Service.Account;
using System.Web.Security;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels.MemberLogin;
using Newtonsoft.Json;

namespace BS_Adoga.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private MemberAccountService _service;

        public AccountController()
        {
            _service = new MemberAccountService();
        }

        // GET: Account
        public ActionResult MemberBooking()
        {
            ViewBag.MemberCurrentPage = "booking";
            return View();
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

        public ActionResult BookingDetail(string id)
        {
            return View();
        }
    }
}