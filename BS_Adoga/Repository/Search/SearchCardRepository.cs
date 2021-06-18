using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BS_Adoga.Models.DBContext;
using System.ComponentModel.DataAnnotations;

namespace BS_Adoga.Repository.Search
{
    public class SearchCardRepository
    {
        private AdogaContext _context;

        public SearchCardRepository()
        {
            _context = new AdogaContext();
        }

        public IQueryable<Hotel> GetHotel()
        {
            var hotel = from h in _context.Hotels
                        select h;

            return hotel;
        }
    }
}