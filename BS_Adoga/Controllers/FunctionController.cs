using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BS_Adoga.Models.ViewModels.MemberLogin;
using BS_Adoga.Service;
using Newtonsoft.Json;

namespace BS_Adoga.Controllers
{
    public class FunctionController : Controller
    {
        [HotelLoginAuthorize]
        // GET: Function
        public ActionResult Index()
        {
            string UserCookiedataJS = ((FormsIdentity)HttpContext.User.Identity).Ticket.UserData;
            UserCookieViewModel UserCookie = JsonConvert.DeserializeObject<UserCookieViewModel>(UserCookiedataJS);
            string id = UserCookie.Id;
            string picture = UserCookie.PictureUrl;
            ViewBag.id = id;
            ViewBag.pictureurl = picture;
            return View();
        }
    }
}