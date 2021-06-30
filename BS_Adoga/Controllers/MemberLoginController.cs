using BS_Adoga.Models.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BS_Adoga.Models.ViewModels.MemberLogin;
using BS_Adoga.Service;
using System.Web.Security;
using System.Threading.Tasks;
using Google.Apis.Auth;
using Newtonsoft.Json;

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
            TempData["Picture"] = string.Empty;
            return View();
        }
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut(); //登出

            return RedirectToAction("HomePage", "Home");
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
        [MultiButton("register")]
        [ValidateAntiForgeryToken]
        public ActionResult Register(MixMemberLoginViewModel registerVM)
        {
            TempData["Picture"] = string.Empty;
            Customer customer = new Customer();

            if (ModelState.IsValid)
            {
                //1.View Model(memberregisterviewmodel) --> Data Model (customer)
                string firstname = HttpUtility.HtmlEncode(registerVM.MemberRegisterViewModel.FirstName);
                string lastname = HttpUtility.HtmlEncode(registerVM.MemberRegisterViewModel.LastName);
                string email = HttpUtility.HtmlEncode(registerVM.MemberRegisterViewModel.Email);
                string password = HttpUtility.HtmlEncode(registerVM.MemberRegisterViewModel.Password);
                Customer cust = new Customer()
                {
                    CustomerID = email,
                    FirstName = firstname,
                    LastName = lastname,
                    Email = email,
                    MD5HashPassword = HashService.MD5Hash(password),
                    Logging = "建立" + "," + firstname+lastname + "," + DateTime.Now.ToString(),
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
        }
        #endregion

        #region Login(登入)
        [HttpPost]
        [MultiButton("login")]
        [ValidateAntiForgeryToken]
        public ActionResult Login(MixMemberLoginViewModel loginVM)
        {
            TempData["Picture"] = string.Empty;
            //一.未通過Model驗證
            if (!ModelState.IsValid)
            {
              
                return View(loginVM);
            }

            //二.通過Model驗證
            string email = HttpUtility.HtmlEncode(loginVM.MemberLoginViewModel.Email);
            string password = HashService.MD5Hash(HttpUtility.HtmlEncode(loginVM.MemberLoginViewModel.Password));

            //三.EF比對資料庫帳密
            //以Name及Password查詢比對Account資料表記錄

            Customer user = _context.Customers.Where(x => x.Email == email && x.MD5HashPassword == password).FirstOrDefault();

            //找不到則彈回Login頁
            if (user == null)
            {
                ModelState.AddModelError("NotFound", "無效的帳號或密碼!");

                return View(loginVM);
            }


            //四.FormsAuthentication Class -- https://docs.microsoft.com/zh-tw/dotnet/api/system.web.security.formsauthentication?view=netframework-4.8

            //FormsAuthenticationTicket Class
            //https://docs.microsoft.com/zh-tw/dotnet/api/system.web.security.formsauthenticationticket?view=netframework-4.8


            //1.Create FormsAuthenticationTicket
            var ticket = new FormsAuthenticationTicket(
            version: 1,
            name: user.FirstName+user.LastName.ToString(), //可以放使用者Id
            issueDate: DateTime.UtcNow,//現在UTC時間
            expiration: DateTime.UtcNow.AddMinutes(30),//Cookie有效時間=現在時間往後+30分鐘
            isPersistent: loginVM.MemberLoginViewModel.Remember,// 是否要記住我 true or false
            userData: loginVM.MemberLoginViewModel.Email, //可以放使用者角色名稱
            cookiePath: FormsAuthentication.FormsCookiePath);

            //2.Encrypt the Ticket
            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            //3.Create the cookie.
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            Response.Cookies.Add(cookie);

            //4.Redirect back to original URL.
            var url = FormsAuthentication.GetRedirectUrl(email, true);

            //5.Response.Redirect
            return RedirectToAction("HomePage", "Home");
        }
        #endregion

        #region GoogleLogin(Google登入)
        [HttpPost]
        public async Task<ActionResult> GoogleLoginAPI(string id_token)
        {
            string msg = "ok";
            GoogleJsonWebSignature.Payload payload = null;

            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(id_token, new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>() { "373077817054-6c6eaqcq6nun968jq67q8epn6pbdl9bo.apps.googleusercontent.com" }//要驗證的client id，把自己申請的Client ID填進去
                });
                //測試
            }
            catch (Google.Apis.Auth.InvalidJwtException ex)
            {
                msg = ex.Message;
            }
            catch (Newtonsoft.Json.JsonReaderException ex)
            {
                msg = ex.Message;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (msg == "ok" && payload != null)
            {//都成功
                string GoogleMail = payload.Email;
                string GoogleFirstName = payload.GivenName;
                string GoogleLastName = payload.FamilyName;
                string GooglePicture = payload.Picture;
                UserCookieViewModel UserData = new UserCookieViewModel()
                {
                    Id = GoogleMail,
                    PictureUrl = GooglePicture
                };
                if(_context.Customers.Where(x => x.Email == GoogleMail).FirstOrDefault() == null)
                {
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            Customer cust = new Customer()
                            {
                                CustomerID = GoogleMail,
                                FirstName = GoogleFirstName,
                                LastName = GoogleLastName,
                                Email = GoogleMail,
                                MD5HashPassword = string.Empty,
                                Logging = "建立" + ","  + GoogleFirstName + GoogleLastName + "," + DateTime.Now.ToString(),
                                RegisterDatetime = DateTime.Now
                            };
                            _context.Customers.Add(cust);
                            _context.SaveChanges();

                            //1.Create FormsAuthenticationTicket
                            var ticket = new FormsAuthenticationTicket(
                            version: 1,
                            name: cust.FirstName + cust.LastName, //可以放使用者Id
                            issueDate: DateTime.UtcNow,//現在UTC時間
                            expiration: DateTime.UtcNow.AddMinutes(30),//Cookie有效時間=現在時間往後+30分鐘
                            isPersistent: false,// 是否要記住我 true or false
                            userData: JsonConvert.SerializeObject(UserData), //可以放使用者角色名稱
                            cookiePath: FormsAuthentication.FormsCookiePath);

                            //2.Encrypt the Ticket
                            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

                            //3.Create the cookie.
                            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                            Response.Cookies.Add(cookie);
                            TempData["Picture"] = GooglePicture;
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            //result.IsSuccessful = false;
                            //result.Exception = ex;
                            transaction.Rollback();
                        }
                    }
                }

                else 
                {
                    //1.Create FormsAuthenticationTicket
                    var ticket = new FormsAuthenticationTicket(
                    version: 1,
                    name: GoogleFirstName + GoogleLastName, //可以放使用者Id
                    issueDate: DateTime.UtcNow,//現在UTC時間
                    expiration: DateTime.UtcNow.AddMinutes(30),//Cookie有效時間=現在時間往後+30分鐘
                    isPersistent: false,// 是否要記住我 true or false
                    userData: JsonConvert.SerializeObject(UserData), //可以放使用者角色名稱
                    cookiePath: FormsAuthentication.FormsCookiePath);

                    //2.Encrypt the Ticket
                    var encryptedTicket = FormsAuthentication.Encrypt(ticket);

                    //3.Create the cookie.
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    Response.Cookies.Add(cookie);
                    TempData["Picture"] = GooglePicture;
                }



                //string user_id = payload.Subject;//取得user_id
                //msg = $@"您的 user_id :{user_id}";
            }

            return Content("OK");
        }
        #endregion

    }
}
