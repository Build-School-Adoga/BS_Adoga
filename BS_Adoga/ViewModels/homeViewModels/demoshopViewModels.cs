using BS_Adoga.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BS_Adoga.ViewModels
{
    public class demoshopViewModels
    {
        public virtual IEnumerable<Hotel> Hotels { get; set; }
        public virtual IEnumerable<Card> Cards { get; set; }
       
    }   
}