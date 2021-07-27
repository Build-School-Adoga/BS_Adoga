using BS_Adoga.Models.DBContext;
using BS_Adoga.Repository;
using BS_Adoga.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

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

        [AcceptVerbs("POST")]
        public IHttpActionResult UploadImage()//string File,string PublicId
        {
            var req = HttpContext.Current.Request;
            if (req.HttpMethod == "OPTIONS")
            {
                var res = HttpContext.Current.Response;
                res.StatusCode = 200;
                res.End();
            }

            int form_Length = int.Parse(req.Form["Length"]);

            Account account = new Account("dodoko", "982417256118774", "qBbM16bl0CdYKEYv8NxfA-LdKW4");
            Cloudinary cloudinary = new Cloudinary(account);
            cloudinary.Api.Secure = true;

            ImageUploadResult[] result = new ImageUploadResult[form_Length];
            for (int i = 0; i < form_Length; i++)
            {
                string publicId = req.Form["PublicId" + i];
                string file = req.Form["File" + i];
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file),
                    PublicId = publicId,
                    Folder = "/Adoga/Hotel/Hotel01",
                    Overwrite = true,
                };
                var uploadResult = cloudinary.Upload(uploadParams);
                result[i] = uploadResult;
            }
            return Json(result);
        }
    }
}
