using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
            List<Facility> facilities = _context.Facilities.ToList();
            ViewBag.Facilities = facilities;

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

        public ActionResult HotelList()
        {
            return View(_context.Hotels.ToList());
        }

        public ActionResult HotelDetails(string hotelid)
        {
            if (hotelid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = _context.Hotels.Find(hotelid);

            if (hotel == null)
            {
                return HttpNotFound();
            }

            return View(hotel);
        }

        public ActionResult HotelEdit(string hotelid)
        {
            if (hotelid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = _context.Hotels.Find(hotelid);
            HotelCreateViewModel hotelCreateVM = new HotelCreateViewModel() 
            {
                HotelID = hotel.HotelID,
                HotelName = hotel.HotelName,
                HotelEngName = hotel.HotelEngName,
                HotelCity = hotel.HotelCity,
                HotelDistrict = hotel.HotelDistrict,
                HotelAddress = hotel.HotelAddress,
                HotelAbout = hotel.HotelAbout,
                Longitude = hotel.Longitude,
                Latitude = hotel.Latitude,
                Star = hotel.Star,
                Logging = hotel.Logging
            };
            if (hotel == null)
            {
                return HttpNotFound();
            }

            List<SimpleZipCodeVM> Citys = JsonConvert.DeserializeObject<List<SimpleZipCodeVM>>(_service.CityJSON());
            List<SelectListItem> firstitems = new List<SelectListItem>();
            List<SelectListItem> seconditems = new List<SelectListItem>();
            foreach (var item in Citys)
            {
                firstitems.Add(new SelectListItem()
                {
                    Text = item.city,
                    Value = item.city,
                    Selected = item.city.Equals(hotel.HotelCity)
                });
                if (item.city == hotel.HotelCity)
                {
                    foreach (var item2 in item.districts)
                        seconditems.Add(new SelectListItem()
                        {
                            Text = item2.district,
                            Value = item2.district,
                            Selected = item2.district.Equals(hotel.HotelDistrict)
                        });
                }
            }
            ViewBag.firstitems = firstitems;
            ViewBag.seconditems = seconditems;

            return View(hotelCreateVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HotelEdit(HotelCreateViewModel hotelCreateVM)
        {
            #region 重構後的作法
            if (ModelState.IsValid)
            {
                var service = new FunctionService();
                var result = service.HotelEdit(hotelCreateVM, User.Identity.Name);
                if (result.IsSuccessful)
                {
                    return RedirectToAction("HotelIndex");
                }
                else
                {
                    var Log = result.WriteLog();
                    return Content("編輯失敗:" + Log);
                }
                //return View(hotelCreateVM);
            }
            else
            {
                return View(hotelCreateVM);
            }
            #endregion
        }

        //第二個區動態取得資料
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

        public ActionResult HotelFacilityIndex()
        {
            var facilities = _context.Facilities.Include(f => f.Hotel);
            return View(facilities.ToList());
        }

        public ActionResult HotelFacilityCreate(string hotelids)
        {
            if(string.IsNullOrEmpty(hotelids))
            {
                ViewBag.HotelID = new SelectList(_context.Hotels, "HotelID", "HotelName");
            }
            else
            {
                ViewBag.HotelID = new SelectList(_context.Hotels.Where(x => x.HotelID == hotelids), "HotelID", "HotelName");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HotelFacilityCreate(Facility facility)
        {
            
            facility.Logging = "建立" + "," + User.Identity.Name + "," + DateTime.Now.ToString();
            if (facility.Logging != null)
            {
                _context.Facilities.Add(facility);
                _context.SaveChanges();
                return RedirectToAction("HotelIndex");
            }
            ViewBag.HotelID = new SelectList(_context.Hotels.Where(x => x.HotelID == facility.HotelID), "HotelID", "HotelName");
            return View(facility);
        }

        public ActionResult HotelFacilityDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facility facility = _context.Facilities.Find(id);
            if (facility == null)
            {
                return HttpNotFound();
            }
            return View(facility);
        }

        public ActionResult HotelFacilityEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facility facility = _context.Facilities.Find(id);
            if (facility == null)
            {
                return HttpNotFound();
            }
            ViewBag.HotelID = new SelectList(_context.Hotels.Where(x => x.HotelID == facility.HotelID), "HotelID", "HotelName", facility.HotelID);
            return View(facility);
        }

        // POST: Facilities/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HotelFacilityEdit([Bind(Include = "FacilitieID,HotelID,SwimmingPool,AirportTransfer,FamilyChildFriendly,Restaurants,Nightclub,GolfCourse,Internet,Gym,NoSmoking,SmokingArea,FacilitiesFordisabledGuests,CarPark,FrontDesk,SpaSauna,PetsAllowed,BusinessFacilities,Logging")] Facility facility)
        {
            facility.Logging = facility.Logging + ";" + "修改" + "," + User.Identity.Name + "," + DateTime.Now.ToString();
            if (ModelState.IsValid)
            {
                _context.Entry(facility).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("HotelFacilityEdit", facility);
            }
            ViewBag.HotelID = new SelectList(_context.Hotels.Where(x => x.HotelID == facility.HotelID), "HotelID", "HotelName", facility.HotelID);
            return View(facility);
        }


        public ActionResult HotelRoomIndex()
        {
            //List<Facility> facilities = _context.Facilities.ToList();
            //ViewBag.Facilities = facilities;

            return View(_repository.GetHotelRoomCount());
        }

        // GET: Hotel/Room/{hotelid}
        public ActionResult HotelRoomsIndex(string hotelid)
        {
            //List<Facility> facilities = _context.Facilities.ToList();
            //ViewBag.Facilities = facilities;
            var test = _repository.GetHotelRoomAll(hotelid);
            return View(_repository.GetHotelRoomAll(hotelid));
        }

        public ActionResult HotelRoomCreate(string hotelids)
        {
            if (string.IsNullOrEmpty(hotelids))
            {
                ViewBag.HotelID = new SelectList(_context.Hotels, "HotelID", "HotelName");
            }
            else
            {
                ViewBag.HotelID = new SelectList(_context.Hotels.Where(x => x.HotelID == hotelids), "HotelID", "HotelName");
            }
            ViewBag.TypesOfBathroomID = new SelectList(_context.BathroomTypes, "TypesOfBathroomID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HotelRoomCreate(HotelRoomCreateViewModel hotelRoomCreateVM)
        {
            if (ModelState.IsValid)
            {
                Room room = new Room()
                {
                    RoomID = hotelRoomCreateVM.HotelID + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    HotelID = hotelRoomCreateVM.HotelID,
                    RoomName = hotelRoomCreateVM.RoomName,
                    NumberOfPeople = hotelRoomCreateVM.NumberOfPeople,
                    RoomCount = hotelRoomCreateVM.RoomCount,
                    RoomPrice = hotelRoomCreateVM.RoomPrice,
                    TypesOfBathroomID = hotelRoomCreateVM.TypesOfBathroomID,
                    NoSmoking = hotelRoomCreateVM.NoSmoking,
                    Breakfast = hotelRoomCreateVM.Breakfast,
                    WiFi = hotelRoomCreateVM.WiFi,
                    TV = hotelRoomCreateVM.TV,
                    Logging = "建立" + "," + User.Identity.Name + "," + DateTime.Now.ToString()
                };
                _context.Rooms.Add(room);
                _context.SaveChanges();
                return RedirectToAction("HotelRoomIndex");
            }

            ViewBag.HotelID = new SelectList(_context.Hotels.Where(x => x.HotelID == hotelRoomCreateVM.HotelID), "HotelID", "HotelName");
            ViewBag.TypesOfBathroomID = new SelectList(_context.BathroomTypes, "TypesOfBathroomID", "Name");
            return View(hotelRoomCreateVM);
        }

        public ActionResult HotelRoomDetails(string roomid)
        {
            if (roomid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = _context.Rooms.Find(roomid);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        public ActionResult HotelRoomEdit(string hotelids,string roomid)
        {
            TempData["roomid"] = roomid;
            Room room = _context.Rooms.Find(roomid);
            HotelRoomCreateViewModel hotelRoomCreateVM = new HotelRoomCreateViewModel()
            {
                HotelID = room.HotelID,
                RoomName = room.RoomName,
                NumberOfPeople = room.NumberOfPeople,
                RoomCount = room.RoomCount,
                RoomPrice = room.RoomPrice,
                TypesOfBathroomID = room.TypesOfBathroomID,
                NoSmoking = room.NoSmoking,
                Breakfast = room.Breakfast,
                WiFi = room.WiFi,
                TV = room.TV
            };

            TempData["Logging"] = room.Logging;

            if (string.IsNullOrEmpty(hotelids))
            {
                ViewBag.HotelID = new SelectList(_context.Hotels, "HotelID", "HotelName");
            }
            else
            {
                ViewBag.HotelID = new SelectList(_context.Hotels.Where(x => x.HotelID == hotelids), "HotelID", "HotelName");
            }
            ViewBag.TypesOfBathroomID = new SelectList(_context.BathroomTypes.Where(x => x.TypesOfBathroomID == room.TypesOfBathroomID), "TypesOfBathroomID", "Name");
            return View(hotelRoomCreateVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HotelRoomEdit(HotelRoomCreateViewModel hotelRoomCreateVM)
        {
            var roomid = TempData["roomid"].ToString();
            string Logging = TempData["Logging"].ToString();

            if (ModelState.IsValid)
            {
                Room room = new Room()
                {
                    RoomID = roomid,
                    HotelID = hotelRoomCreateVM.HotelID,
                    RoomName = hotelRoomCreateVM.RoomName,
                    NumberOfPeople = hotelRoomCreateVM.NumberOfPeople,
                    RoomCount = hotelRoomCreateVM.RoomCount,
                    RoomPrice = hotelRoomCreateVM.RoomPrice,
                    TypesOfBathroomID = hotelRoomCreateVM.TypesOfBathroomID,
                    NoSmoking = hotelRoomCreateVM.NoSmoking,
                    Breakfast = hotelRoomCreateVM.Breakfast,
                    WiFi = hotelRoomCreateVM.WiFi,
                    TV = hotelRoomCreateVM.TV,
                    Logging = Logging + ";" + "修改" + "," + User.Identity.Name + "," + DateTime.Now.ToString()
                };

                _context.Entry(room).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("HotelRoomEdit", hotelRoomCreateVM);
            }

            ViewBag.HotelID = new SelectList(_context.Hotels.Where(x => x.HotelID == hotelRoomCreateVM.HotelID), "HotelID", "HotelName");
            ViewBag.TypesOfBathroomID = new SelectList(_context.BathroomTypes.Where(x => x.TypesOfBathroomID == hotelRoomCreateVM.TypesOfBathroomID), "TypesOfBathroomID", "Name");
            return View(hotelRoomCreateVM);
        }

    }
}