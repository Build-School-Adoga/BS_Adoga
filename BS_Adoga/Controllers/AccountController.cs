using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BS_Adoga.Service.Account;
using System.Web.Security;
using BS_Adoga.Service.Account;

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


            string id = ((FormsIdentity)HttpContext.User.Identity).Ticket.UserData;

            return View();
        }
    }
}