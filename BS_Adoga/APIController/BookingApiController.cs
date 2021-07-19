using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Security;
using BS_Adoga.Service;
using BS_Adoga.Repository;
using BS_Adoga.Models.ViewModels.Account;
using BS_Adoga.Models.ViewModels.MemberLogin;
using Newtonsoft.Json;

namespace BS_Adoga.APIController
{
    public class BookingApiController : ApiController
    {
        private MemberAccountService _service;
        private MemberAccountRepository _repository;

        public BookingApiController()
        {
            _service = new MemberAccountService();
            _repository = new MemberAccountRepository();
        }

        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult MemberBookingList(string id)
        {
            var a = _repository.GetBookingDESC(id);
            return Json(a);
        }
        //// GET: DetailApi
        ////[HttpGet]
        //[AcceptVerbs("GET", "POST")]
        //public IHttpActionResult GetAllRoom(string hotelName, string startDate, string endDate, int orderRoom, int adult, int child)
        //{
        //    string UserCookiedataJS = ((FormsIdentity)HttpContext.User.Identity).Ticket.UserData;
        //    UserCookieViewModel UserCookie = JsonConvert.DeserializeObject<UserCookieViewModel>(UserCookiedataJS);
        //    string user_id = UserCookie.Id;

        //    return View(_repository.GetBookingDESC(user_id));
    }
}
