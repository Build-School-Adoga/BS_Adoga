﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Adoga.Models.ViewModels.CheckOut
{
    public class ECPayResultsViewModel
    {
        public string OrderId { get; set; }
        public string TradeDate { get; set; }
        public string PaymentDate { get; set; }
        public string TradePrice { get; set; }
    }
}