using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels.homeViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Adoga.Repository.Home
{
    public class HomeRepository
    {
        public AdogaContext _context;
        public HomeRepository()
        {
            _context = new AdogaContext();
        }
        List<Card> cards = new List<Card>
        {
            new Card { name = "台北W酒店", area = "信義區", Evaluation = 2,  ground ="../Asset/images/Home/台北w酒店.jpg", sale = "2.5折扣", Originalprice = "NT$11,024", saleprice = "NT$5,500", fraction = "9.3", comment = "超棒", Quantity = "1198篇評鑑" },
            new Card { name = "飛行家青年旅館", area= "苓雅區", Evaluation=  1, ground= "../Asset/images/Home/飛行家青年旅館.jpg", sale= "2.2折扣", Originalprice= "NT$2,370", saleprice= "NT$595", fraction= "8.8", comment= "很讚", Quantity= "1198篇評鑑" },
            new Card  { name= "鈞怡大飯店", area= "高雄市", Evaluation=  3,  ground= "../Asset/images/Home/鈞怡大飯店.jpg", sale= "1.9折扣", Originalprice= "NT$6,234", saleprice= "NT$1,234", fraction= "8.9", comment= "很讚", Quantity= "1198篇評鑑" },
            new Card  { name= "塩‧泊思行旅", area= "高雄市", Evaluation= 4,  ground= "../Asset/images/Home/塩‧泊思行旅.jpg", sale= "2折扣", Originalprice= "NT$3,611", saleprice= "NT$764", fraction= "8.4", comment= "很讚", Quantity= "1198篇評鑑"}

        };
        public IQueryable<CardViewModels> GetCardModels()
        {
      
            var images = from p in _context.Hotels
                         join s in _context.HotelImages on p.HotelID equals s.HotelID
                         join d in _context.Rooms on p.HotelID equals d.HotelID
                         join z in _context.RoomsDetails on d.RoomID equals z.RoomID
                         select new CardViewModels
                         {
                             HotelID = p.HotelID,
                             HotelName = p.HotelName,
                             HotelCity = p.HotelCity,
                             Star =p.Star,

                             My_HotelImages = new MyHoteiImages
                             {
                                 HotelID = p.HotelID,
                                 ImageID = s.ImageID,
                                 ImageURL = s.ImageURL
                             },
                             My_Rooms = new MyRoom
                             {
                                 HotelID = p.HotelID,
                                 RoomPrice = d.RoomPrice
                             },
                             My_RoomsDetails = new MyRoomsDetails
                             {
                                 RoomID = d.RoomID,
                                 RoomDiscount = z.RoomDiscount
                             }
                         };
            return images;
        }
        public IEnumerable<MyHotels> GetHotelModels()
        {
            var hotel = from p in _context.Hotels
                        select new MyHotels
                        {
                            HotelID = p.HotelID,
                            HotelCity = p.HotelCity,
                            HotelName = p.HotelName,
                            HotelAddress = p.HotelAddress,
                            Star = p.Star
                        };
            return hotel.ToList();
        }
        //public demoshopViewModels Getcards()
        //{
        //    demoshopViewModels productss = new demoshopViewModels()
        //    {
        //        Cards = cards,
        //        Hotels = _context.Hotels.ToList()
        //    };            

        //    return productss;
        //}

    }
}