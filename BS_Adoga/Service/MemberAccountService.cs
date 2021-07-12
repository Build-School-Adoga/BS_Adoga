using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BS_Adoga.Repository;
using BS_Adoga.Models.DBContext;
using BS_Adoga.Models.ViewModels;
using BS_Adoga.Models.ViewModels.Account;

namespace BS_Adoga.Service
{
    public class MemberAccountService
    {
        private MemberAccountRepository _repository;

        public MemberAccountService()
        {
            _repository = new MemberAccountRepository();
        }

        public IQueryable<Customer> GetMember(string id)
        {
           return  _repository.GetMember(id);
        }

        public BookingDetailViewModel GetBookingDetail(string orderID, string customerID)
        {
            return _repository.GetBookingDetail(orderID,customerID);
        }
    }
}