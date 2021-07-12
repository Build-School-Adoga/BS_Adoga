using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Service;
using BS_Adoga.Models.ViewModels.Search;

namespace BS_Adoga.Controllers
{
    public class SearchController : Controller
    {
        private SearchCardService s;
        public SearchController()
        {
            s = new SearchCardService();
        }

        //搜尋欄裡用beginForm去收集資料，再對給Search(action)做搜尋
        public ActionResult GetTempData(string search, string date_range, string people, string room)
        {
            var human = people.Split(',');
            
            var a = human[0].Split('位');
            var adult = int.Parse(a[0]);
            var kid = 0;
            if (human.Length > 1)
            {
                var b = human[1].Split('位');
                kid = int.Parse(b[0]);
            }
            
            var r = room.Split('間');

            var date = date_range.Split('-');
            var start = date[0];
            var end = date[1];

            SearchDataViewModel info = new SearchDataViewModel
            {
                HotelNameOrCity = search,
                CheckInDate = start,
                CheckOutDate = end,
                AdultCount = adult,
                KidCount = kid,
                RoomCount = int.Parse(r[0])
            };

            

            return RedirectToAction("Search",info);
        }

        //[HttpGet]
        public ActionResult Search(SearchDataViewModel info, string sortOrder)
        {
            if(info.HotelNameOrCity==null)
            {
                info.HotelNameOrCity = TempData["CityOrName"].ToString();
                info.CheckInDate = TempData["sDate"].ToString();
                info.CheckOutDate = TempData["end"].ToString();
                info.AdultCount = (int)TempData["adult"];
                info.KidCount = (int)TempData["kid"];
                info.RoomCount = (int)TempData["room"];
                TempData.Keep();
            }
            else
            {
                TempData["CityOrName"] = info.HotelNameOrCity;
                TempData["sDate"] = info.CheckInDate;
                TempData["end"] = info.CheckOutDate;
                TempData["adult"] = info.AdultCount;
                TempData["kid"] = info.KidCount;
                TempData["room"] = info.RoomCount;
                TempData.Keep();
            }
            var data = s.GetSearchViewModelData(info);

            //排序 預設抓星級最好的
            ViewBag.StarSortParm = sortOrder == null ? "star_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";
            
            switch (sortOrder)
            {
                case "star_desc":
                    data.HotelSearchVM = data.HotelSearchVM.OrderByDescending(o => o.Star);
                    break;

                case "price_desc":
                    data.HotelSearchVM = data.HotelSearchVM.OrderByDescending(o => o.I_RoomVM.RoomPrice);
                    break;

                case "Price":
                    data.HotelSearchVM = data.HotelSearchVM.OrderBy(o => o.I_RoomVM.RoomPrice);
                    break;

                default:
                    data.HotelSearchVM = data.HotelSearchVM.OrderBy(o => o.Star);
                    break;
            }


            return View(data);

        }

        //public ViewResult Order(string sortOrder, SearchDataViewModel info)
        //{
        //    //預設抓星級最好的
        //    ViewBag.StarSortParm = sortOrder==null ? "star_desc" : "";
        //    ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";

        //    SearchCardViewModel OrderList = s.GetSearchViewModelData(info);
        //    switch (sortOrder)
        //    {
        //        case "star_desc":
        //            OrderList.HotelSearchVM = OrderList.HotelSearchVM.OrderByDescending(o => o.Star);
        //            break;

        //        case "price_desc":
        //            OrderList.HotelSearchVM = OrderList.HotelSearchVM.OrderByDescending(o => o.I_RoomVM.RoomPrice);
        //            break;

        //        case "Price":
        //            OrderList.HotelSearchVM = OrderList.HotelSearchVM.OrderBy(o => o.I_RoomVM.RoomPrice);
        //            break;

        //        default:
        //            OrderList.HotelSearchVM = OrderList.HotelSearchVM.OrderBy(o => o.Star);
        //            break;
        //    }

        //    return View(OrderList);
        //}
    }
}