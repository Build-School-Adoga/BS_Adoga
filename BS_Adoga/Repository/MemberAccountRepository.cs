using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels;
using BS_Adoga.Models.ViewModels.Account;
using BS_Adoga.Models.ViewModels.HotelDetail;

namespace BS_Adoga.Repository
{
    public class MemberAccountRepository
    {
        private AdogaContext _context;

        public MemberAccountRepository()
        {
            _context = new AdogaContext();
        }

        public IQueryable<Customer> GetMember(string id)
        {
            var memberData = from c in _context.Customers
                             where c.CustomerID == id
                             select c;

            return memberData;
        }

        public IEnumerable<MemberBookingViewModel> GetBookingDESC(string customerID)
        {
            var table = (from o in _context.Orders
                         join r in _context.Rooms on o.RoomID equals r.RoomID
                         join h in _context.Hotels on r.HotelID equals h.HotelID
                         where o.CustomerID == customerID
                         orderby o.OrderID descending
                         select new MemberBookingViewModel
                         {
                             OrderID = o.OrderID,
                             HotelID = h.HotelID,
                             HotelName = h.HotelName,
                             HotelEngName = h.HotelEngName,
                             RoomBed = ((from rb in _context.RoomBeds
                                         join bt in _context.BedTypes on rb.TypesOfBedsID equals bt.TypesOfBedsID
                                         where rb.RoomID == o.RoomID
                                         select new RoomBedVM
                                         {
                                             Name = bt.Name,
                                             Amount = rb.Amount
                                         })),
                             RoomPriceTotal = o.RoomPriceTotal,
                             OrderDate = o.OrderDate,
                             CheckInDate = o.CheckInDate,
                             CheckOutDate = o.CheckOutDate,
                             Breakfast = r.Breakfast,
                             City = h.HotelCity,
                             PaymentStatus = o.PaymentStatus
                         });
            return table;
        }

        
        public BookingDetailViewModel GetBookingDetail(string orderID,string customerID)
        {
            var table = (from o in _context.Orders
                         join r in _context.Rooms on o.RoomID equals r.RoomID
                         join h in _context.Hotels on r.HotelID equals h.HotelID
                         where o.CustomerID == customerID && o.OrderID == orderID
                         orderby o.OrderID descending
                         select new BookingDetailViewModel
                         {
                             HotelID = h.HotelID,
                             HotelName = h.HotelName,
                             HotelEngName = h.HotelEngName,
                             Star = h.Star,
                             HotelCity = h.HotelCity,
                             HotelDistrict = h.HotelDistrict,
                             HotelAddress = h.HotelAddress,
                             OrderID = o.OrderID,
                             OrderDate = o.OrderDate,
                             CheckInDate = o.CheckInDate,
                             CheckOutDate = o.CheckOutDate,
                             Email = o.Email,
                             Name = o.FirstName + " " +o.LastName,
                             PhoneNumber = o.PhoneNumber,
                             RoomBed = ((from rb in _context.RoomBeds
                                         join bt in _context.BedTypes on rb.TypesOfBedsID equals bt.TypesOfBedsID
                                         where rb.RoomID == o.RoomID
                                         select new RoomBedVM
                                         {
                                             Name = bt.Name,
                                             Amount = rb.Amount
                                         })),
                             RoomPriceTotal = o.RoomPriceTotal
                         }).FirstOrDefault();
            return table;
        }

    }
}