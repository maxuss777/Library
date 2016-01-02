using System;
using System.Web.Security;
using Library.API.Abstract;
using Library.API.Common.User;

namespace Library.API.Providers
{
    public class AuthonticateProvider : ILoginProvider, IRegistrationProvider
    {

        public bool Login(LogOnModel model)
        {
            try
            {
                return Membership.ValidateUser(model.Email, model.Password);
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }
        }

        public bool Register(RegisterModel model)
        {
            try
            {
                MembershipUser membershipUser =
                    ((CustomMembershipPovider)Membership.Provider).CreateUser(model.Email, model.Password);

                return membershipUser != null;
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }
        }
    }
}