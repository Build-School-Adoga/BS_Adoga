using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels.HotelLogin;
using BS_Adoga.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace BS_Adoga.Service
{
    public class FunctionService
    {
        public OperationResult HotelEdit(HotelCreateViewModel hotelCreateVM , string username)
        {
            var result = new OperationResult();
            try
            {
                //1.View Model(RegisterViewModel) --> Data Model (HotelEmployee)
                var repository = new DBRepository(new AdogaContext());
                Hotel basic = repository.GetAll<Hotel>().Where(x => x.HotelID == hotelCreateVM.HotelID).FirstOrDefault();
                basic.HotelName = hotelCreateVM.HotelName;
                basic.HotelEngName = hotelCreateVM.HotelEngName;
                basic.HotelCity = hotelCreateVM.HotelCity;
                basic.HotelDistrict = hotelCreateVM.HotelDistrict;
                basic.HotelAddress = hotelCreateVM.HotelAddress;
                basic.HotelAbout = hotelCreateVM.HotelAbout;
                basic.Longitude = hotelCreateVM.Longitude;
                basic.Latitude = hotelCreateVM.Latitude;
                basic.Star = hotelCreateVM.Star;
                basic.Logging = basic.Logging + ";" + "修改" + "," + username + "," + DateTime.Now.ToString();
                //Hotel entity = new Hotel()
                //{
                //    HotelID = hotelCreateVM.HotelID,
                //    HotelName = hotelCreateVM.HotelName,
                //    HotelEngName = hotelCreateVM.HotelEngName,
                //    HotelCity = hotelCreateVM.HotelCity,
                //    HotelDistrict = hotelCreateVM.HotelDistrict,
                //    HotelAddress = hotelCreateVM.HotelAddress,
                //    HotelAbout = hotelCreateVM.HotelAbout,
                //    Longitude = hotelCreateVM.Longitude,
                //    Latitude = hotelCreateVM.Latitude,
                //    Star = hotelCreateVM.Star,
                //    Logging = "修改" + "," + "Name" + "," + DateTime.Now.ToString(),

                //    //Logging = "建立" + "," + registerVM.RegisterViewModel.Name + "," + DateTime.Now.ToString(),
                //};
                repository.Update(basic);
                repository.SaveChanges();
                result.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                result.Exception = ex;
            }
            return result;
        }

        public string CityJSON()
        {
            string url = "https://raw.githubusercontent.com/apprunner/FileStorage/master/SimpleZipCode.json";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url); //request請求
            req.Timeout = 10000; //request逾時時間
            req.Method = "GET"; //request方式
            HttpWebResponse respone = (HttpWebResponse)req.GetResponse(); //接收respone
            StreamReader streamReader = new StreamReader(respone.GetResponseStream(), Encoding.UTF8); //讀取respone資料
            string result = streamReader.ReadToEnd(); //讀取到最後一行
            respone.Close();
            streamReader.Close();
            return result;
        }

        //展開一個月的RoomDetail
        public OperationResult CreateRoomDetailExpansion(string year,string month, string roomid,string username)
        {
            string CheckInDate = $"{year}/{month}/1 15:00:00";
            string CheckOutDate = $"{year}/{month}/2 11:00:00";

            DateTime CheckInDateObject = Convert.ToDateTime(CheckInDate);
            DateTime CheckOutDateObject = Convert.ToDateTime(CheckOutDate);
            DateTime NextMonthFirstDayObject = CheckInDateObject.AddMonths(1);

            var result = new OperationResult();
            try
            {
                var repository = new DBRepository(new AdogaContext());
                Room basic = repository.GetAll<Room>().Where(x => x.RoomID == roomid).FirstOrDefault();

                //一直加傳入的當月資料直到下個月的第一天為止
                while (CheckInDateObject != NextMonthFirstDayObject)
                {
                    RoomsDetail roomsDetail = new RoomsDetail()
                    {
                        RDID = CheckInDateObject.ToString("yyyy-M-d-") + roomid,
                        RoomID = basic.RoomID,
                        CheckInDate = CheckInDateObject,
                        CheckOutDate = CheckOutDateObject,
                        RoomCount = basic.RoomCount,
                        RoomOrder = 0,
                        RoomDiscount = 0,
                        OpenRoom = true,
                        Logging = "建立" + "," + username + "," + DateTime.Now.ToString()
                    };

                    repository.Create(roomsDetail);
                    repository.SaveChanges();

                    CheckInDateObject = CheckInDateObject.AddDays(1);
                    CheckOutDateObject = CheckOutDateObject.AddDays(1);
                }
                result.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                result.Exception = ex;
            }
            return result;
        }

        //儲存圖片URL
        public OperationResult HotelImageUpload_UpdataOrAdd(string hotelID,string imageID,string imageURL)
        {
            var result = new OperationResult();
            try
            {
                var repository = new DBRepository(new AdogaContext());
                HotelImageUpload basic = repository.GetAll<HotelImageUpload>().Where(x => x.ImageID == imageID && x.HotelID == hotelID).FirstOrDefault();
                if(basic != null)
                {
                    basic.ImageURL = imageURL;
                    repository.Update(basic);
                    repository.SaveChanges();
                }
                else
                {
                    var hotelImg = new HotelImageUpload()
                    {
                        HotelID = hotelID,
                        ImageID = imageID,
                        ImageURL = imageURL
                    };                    

                    AdogaContext _context = new AdogaContext();
                    _context.HotelImageUploads.Add(hotelImg);
                    _context.SaveChanges();
                }
                result.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                result.Exception = ex;
            }
            return result;
        }
    }
}