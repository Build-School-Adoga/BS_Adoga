using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BS_Adoga.Repository;
using BS_Adoga.Service;
using BS_Adoga.Models.ViewModels.Search;

namespace BS_Adoga.APIController
{
    public class SearchApiController : ApiController
    {
        private SearchCardService _s;
        private SearchCardRepository _r;
        public SearchApiController()
        {
            _s = new SearchCardService();
            _r = new SearchCardRepository();
        }

        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetHotelFromCity(string city, string start, string end, int adult, int kid, int room)
        {
            var allHotel = _s.GetHotelAfterSearchByCity(city,start,end,adult,kid,room);
            return Json(allHotel);
        }
    }
}
