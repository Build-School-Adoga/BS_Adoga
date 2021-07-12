using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
//using System.Web.Mvc;
using BS_Adoga.Service;
using BS_Adoga.Models.ViewModels.HotelDetail;
using System.Web.Http.Description;
using Newtonsoft.Json;

namespace BS_Adoga.APIController.HotelDetail
{
    public class DetailApiController : ApiController
    {
        private HotelDetailService _service;

        public DetailApiController()
        {
            _service = new HotelDetailService();
        }

        // GET: DetailApi
        //[HttpGet]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllRoom()
        {
            var data = _service.GetRoomTypeByFilter("hotel04", "2021-06-20", "2021-06-22", 1, 2, 0);

            return Json(data);
        }


        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetSpecificRoom(bool freeBreakfast, bool noSmoking, bool family)
        {            
            var data = _service.GetSpecificRoomType("hotel04", "2021-06-20", "2021-06-22", 1, 2, 0, freeBreakfast, noSmoking, family);

            return Json(data);
            //return Ok(JsonConvert.SerializeObject(a));
        }

    


    }
}