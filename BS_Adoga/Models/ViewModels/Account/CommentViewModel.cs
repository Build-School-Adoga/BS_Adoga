using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Adoga.Models.ViewModels.Account
{
    public class CommentViewModel
    {
        public string OrderID { get; set; }

        public string HotelID { get; set; }

        public string CustomerID { get; set; }

        public string HotelName { get; set; }

        public string Title { get; set; }

        public DateTime MessageDate { get; set; }

        public string MessageText { get; set; }

        public decimal? Score { get; set; }
    }
}