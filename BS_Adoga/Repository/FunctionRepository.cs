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

        public IEnumerable<HotelRoomViewModel> GetHotelRoomCount()
        {
            var table = (from h in _context.Hotels
                         select new HotelRoomViewModel
                         {
                             HotelID = h.HotelID,
                             HotelName = h.HotelName,
                             RoomCount = (from r in _context.Rooms
                                          where r.HotelID == h.HotelID
                                          select r.RoomID).Count()
                         });
            return table;
        }

        public IEnumerable<Room> GetHotelRoomAll(string hotelid)
        {
            var table =  from r in _context.Rooms
                         where r.HotelID == hotelid
                         select r;
            return table;
        }

        public IEnumerable<RoomsDetail> GetAllRoomDetailThisMonth(string roomid)
        {
            string YearMonth = DateTime.Now.ToString("yyyy/M");
            string ThisMonthFirstDay = $"{YearMonth}/1 00:00:00";

            DateTime ThisMonthFirstDayObject = Convert.ToDateTime(ThisMonthFirstDay);
            DateTime NextMonthFirstDayObject = ThisMonthFirstDayObject.AddMonths(1);

            var table = from rd in _context.RoomsDetails
                        where rd.RoomID == roomid && rd.CheckInDate >= ThisMonthFirstDayObject && rd.CheckInDate < NextMonthFirstDayObject
                        orderby rd.CheckInDate ascending
                        select rd;
            return table;
        }

        public IEnumerable<RoomsDetail> GetAllRoomDetailMonth(string year, string month, string roomid)
        {
            string MonthFirstDay = $"{year}/{month}/1 00:00:00";

            DateTime MonthFirstDayObject = Convert.ToDateTime(MonthFirstDay);
            DateTime NextMonthFirstDayObject = MonthFirstDayObject.AddMonths(1);

            var table = from rd in _context.RoomsDetails
                        where rd.RoomID == roomid && rd.CheckInDate >= MonthFirstDayObject && rd.CheckInDate < NextMonthFirstDayObject
                        orderby rd.CheckInDate ascending
                        select rd;
            return table;
        }

    }
    
}