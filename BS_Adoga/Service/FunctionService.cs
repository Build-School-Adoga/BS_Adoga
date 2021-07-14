﻿using BS_Adoga.Models.DBContext;
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
    }
}