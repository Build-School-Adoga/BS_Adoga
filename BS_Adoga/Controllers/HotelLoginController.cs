using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels;
using BS_Adoga.Models.ViewModels.HotelLogin;
using BS_Adoga.Service;

namespace BS_Adoga.Controllers
{
    public class HotelLoginController : Controller
    {
        private AdogaContext _context;

        public HotelLoginController()
        {
            _context = new AdogaContext();
        }
        // GET: HotelLogin
        public ActionResult HotelLogin()
        {
            return View();
        }

        #region HotelLogin方法一(未驗證欄位)
        /*[HttpPost]
        public ActionResult HotelLogin(string Name,string Email,string Password1, string Password2)
        {
            HotelEmployee hotelEmployee = new HotelEmployee();
            if(Password1 != Password2)
            {
                return View();
            }

            else
            {
                hotelEmployee.HotelEmployeeID = Email;
                hotelEmployee.Name = Name;
                hotelEmployee.Email = Email;
                hotelEmployee.Password = Password1;
                hotelEmployee.RegisterDatetime = DateTime.Now;
                _context.HotelEmployees.Add(hotelEmployee);
                _context.SaveChanges();
                return RedirectToAction("HomePage", "Home");
            }

        }*/
        #endregion

        #region HotelLogin方法二(驗證欄位)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HotelLogin(RegisterViewModel registerVM)
        {
            if (ModelState.IsValid)
            {
                //1.View Model(RegisterViewModel) --> Data Model (HotelEmployee)
                string name = HttpUtility.HtmlEncode(registerVM.Name);
                string email = HttpUtility.HtmlEncode(registerVM.Email);
                string password = HttpUtility.HtmlEncode(registerVM.Password);

                HotelEmployee emp = new HotelEmployee()
                {
                    HotelEmployeeID = email,
                    Name = name,
                    Email = email,
                    MD5HashPassword = HashService.MD5Hash(password),
                    Logging = "建立" + ","+ name + "," + DateTime.Now.ToString(),
                    RegisterDatetime = DateTime.Now
                };

                //EF
                try
                {
                    _context.HotelEmployees.Add(emp);
                    _context.SaveChanges();

                    return Content("新增帳號成功");
                }
                catch (Exception ex)
                {
                    return Content("新增帳號失敗:" + ex.ToString());
                }
            }

            return View(registerVM);
        }
        #endregion
    }
}