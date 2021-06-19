﻿using BS_Adoga.Models.ViewModels.homeViewModels;
using BS_Adoga.Repository.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Adoga.Service.Home
{
    public class HomeService
    {
        private HomeRepository _homeRepository;
        public HomeService() 
        {
            _homeRepository = new HomeRepository();
        }
        public demoshopViewModels GetHomeByFilter()
        {
            var result = _homeRepository.Getcards();
            return result;
        }

    }
}