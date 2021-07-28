using BS_Adoga.Models.DBContext;
using BS_Adoga.Repository;
using BS_Adoga.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BS_Adoga.APIController
{
    public class FunctionApiController : ApiController
    {
        private AdogaContext _context;
        private FunctionRepository _repository;
        private FunctionService _service;
        public FunctionApiController()
        {
            _context = new AdogaContext();
            _repository = new FunctionRepository(_context);
            _service = new FunctionService();
        }

        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetRoomDetailMonth(string year,string month,string roomid)
        {
            List<RoomsDetail> RoomDetailMonth = _repository.GetAllRoomDetailMonth(year,month,roomid).ToList();

            return Json(RoomDetailMonth);
        }

        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult EditRoomDetail(string RDID, decimal RoomDiscount, bool OpenRoom ,string username)
        {
            var check = _service.RoomDetailEdit(RDID, RoomDiscount, OpenRoom, username);

            if (check.IsSuccessful == true) { return Ok("OK"); }
            else { return Ok("NOTOK"); }
        }
    }
}
