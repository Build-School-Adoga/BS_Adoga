using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace BS_Adoga.Models.ViewModels.MemberLogin
{
    public class MixMemberLoginViewModel
    {
        public virtual MemberLoginViewModel MemberLoginViewModel { get; set; }
        public virtual MemberRegisterViewModel MemberRegisterViewModel { get; set; }

    }
}