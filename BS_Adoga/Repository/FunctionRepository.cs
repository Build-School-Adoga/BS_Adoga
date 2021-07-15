using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels.HotelLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Adoga.Repository
{
    public class FunctionRepository : DBRepository
    {
        private AdogaContext _context;

        
        public FunctionRepository(AdogaContext context) : base(context)
        {
            _context = new AdogaContext();
        }

        public IEnumerable<HotelListViewModel> GetHotelList()
        {
            var table = (from h in _context.Hotels
                         select new HotelListViewModel
                         {
                             HotelID = h.HotelID,
                             HotelName = h.HotelName,
                             HotelEngName = h.HotelEngName,
                             HotelCity = h.HotelCity,
                             HotelDistrict = h.HotelDistrict,
                             HotelAddress = h.HotelAddress
                         });
            return table;
        }

    }
    
}