using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels;

namespace BS_Adoga.Repository.Account
{
    public class MemberAccountRepository
    {
        private AdogaContext _context;

        public MemberAccountRepository()
        {
            _context = new AdogaContext();
        }

        public IQueryable<Customer> GetMember(string id)
        {
            var memberData = from c in _context.Customers
                             where c.CustomerID == id
                             select c;

            return memberData;
        }
    }
}