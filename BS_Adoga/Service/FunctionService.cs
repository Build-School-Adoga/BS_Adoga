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