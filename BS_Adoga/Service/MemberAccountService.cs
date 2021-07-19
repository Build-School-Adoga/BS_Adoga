using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BS_Adoga.Repository;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels;
using BS_Adoga.Models.ViewModels.Account;

namespace BS_Adoga.Service
{
    public class MemberAccountService
    {
        private MemberAccountRepository _repository;

        public MemberAccountService()
        {
            _repository = new MemberAccountRepository();
        }

        public IQueryable<Customer> GetMember(string id)
        {
           return  _repository.GetMember(id);
        }

        public BookingDetailViewModel GetBookingDetail(string orderID, string customerID)
        {
            return _repository.GetBookingDetail(orderID,customerID);
        }

        public IEnumerable<BookingOrderViewModel> GetBookingOrderDESC (string customerID)
        {
            var data = _repository.GetBookingDESC(customerID);

            var result = from item in data
                         select new BookingOrderViewModel
                         {
                             OrderID = item.OrderID,
                             HotelID = item.HotelID,
                             HotelName = item.HotelName,
                             HotelEngName = item.HotelEngName,
                             RoomBed = item.RoomBed,
                             RoomPriceTotal = item.RoomPriceTotal,
                             
                             OrderDate= item.OrderDate,
                             CheckInDate= item.CheckInDate,
                             CheckOutDate = item.CheckOutDate,
                             
                             FewDaysAgo = new TimeSpan(DateTime.Now.Ticks - item.OrderDate.Ticks).Days,
                             
                             OrderDateStr = item.OrderDate.ToString("yyyy年MM月dd日"),
                             
                             CheckInDay = item.CheckInDate.ToString("dd"),
                             CheckInWeek = item.CheckInDate.ToString("MMM"),
                             CheckInMonth = item.CheckInDate.ToString("ddd"),
                             
                             CheckOutDay = item.CheckOutDate.ToString("dd"),
                             CheckOutWeek = item.CheckOutDate.ToString("MMM"),
                             CheckOutMonth = item.CheckOutDate.ToString("ddd"),
                             
                             CheckCheckOut = item.CheckOutDate.CompareTo(DateTime.Now),
                             City= item.City,
                             Breakfast= item.Breakfast,
                         };

            return result;
        }
    }
}