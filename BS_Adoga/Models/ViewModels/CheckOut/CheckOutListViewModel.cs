using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BS_Adoga.Models.ViewModels.CheckOut
{
    public class CheckOutListViewModel
    {
        //public string OrderID { get; set; }

        [DisplayName("英文名（同護照）")]
        public string FirstName { get; set; }

        [DisplayName("英文姓（同護照）")]
        public string LastName { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("手機號碼")]
        public string PhoneNumber { get; set; }

        //public SelectList Country { get; set; }

    }
}