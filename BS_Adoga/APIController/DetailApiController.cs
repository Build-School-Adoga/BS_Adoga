using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
//using System.Web.Mvc;
using BS_Adoga.Service.HotelDetail;
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
        public IHttpActionResult Index()
        {
           var a =  _service.GetRoomTypeByFilter("hotel04","2021-06-20","2021-06-22",2,12);
            return Json(a);
            //return Ok(JsonConvert.SerializeObject(a));
        }
    }
}