using BS_Adoga.Models.ViewModels.homeViewModels;
using BS_Adoga.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Adoga.Service
{
    public class HomeService
    {
        public HomeRepository _homeRepository;
        public HomeService() 
        {
            _homeRepository = new HomeRepository();
        }
        //public demoshopViewModels GetHomeByFilter()
        //{
        //    var result = _homeRepository.Getcards();
        //    return result;
        //}
        public demoshopViewModels  ALLImages()
        {
           var productss = new demoshopViewModels() {
                My_MyHotels = _homeRepository.GetHotelModels(),
              My_CardViewModels  = _homeRepository.GetCardModels()
                  };

            return productss;
        }


    }
}