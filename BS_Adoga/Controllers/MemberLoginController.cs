using BS_Adoga.Models.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BS_Adoga.Models.ViewModels.MemberLogin;
using BS_Adoga.Service;

namespace BS_Adoga.Controllers
{
    public class MemberLoginController : Controller
    {
        private AdogaContext _context;
        public MemberLoginController()
        {
            _context = new AdogaContext();
        }
        // GET: MemberLogin
        public ActionResult MemberLogin()
        {
            return View();
        }
        #region memberlogin方法一未驗證欄位
        //[HttpPost]
        //public ActionResult MemberLogin(Customer log, String FirstName,String LastName,String Email,String Password1,String Password2)
        //{

        //Customer customer = new Customer();



        /*if (Password1 != Password2)
            {
                return View();
            }
            else
            {
                customer.CustomerID = Email;
                customer.FirstName = FirstName;
                customer.LastName = LastName;
                customer.Email = Email;
                customer.Password = Password1;
                customer.RegisterDatetime = DateTime.Now;
                customer.Logging = DateTime.Now.ToString();
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return RedirectToAction("HomePage", "Home");

            }*/
        #endregion


        #region 方法二  欄位驗證
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MemberLogin(MemberRegisterViewModel registerVM)
        {
            Customer customer = new Customer();

            if (ModelState.IsValid)
            {
                //1.View Model(memberregisterviewmodel) --> Data Model (customer)
                string firstname = HttpUtility.HtmlEncode(registerVM.FirstName);
                string lastname = HttpUtility.HtmlEncode(registerVM.LastName);
                string email = HttpUtility.HtmlEncode(registerVM.Email);
                string password = HttpUtility.HtmlEncode(registerVM.Password);
                Customer cust = new Customer()
                {
                    CustomerID = email,
                    FirstName = firstname,
                    LastName = lastname,
                    Email = email,
                    MD5HashPassword = HashService.MD5Hash(password),
                    Logging = "建立" + "," + email + "," + DateTime.Now.ToString(),
                    RegisterDatetime = DateTime.Now
                };
                //EF
                try
                {
                    _context.Customers.Add(cust);
                    _context.SaveChanges();
                    return Content("新增帳號成功");
                }
                catch (Exception ex)
                {
                    return Content("新增帳號失敗:" + ex.ToString());
                }
            }

            return View(registerVM);
            #endregion
        }
    }
}
