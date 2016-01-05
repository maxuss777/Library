using System;
using System.Web.Helpers;
using System.Web.Security;
using Library.API.Common.Member;
using Library.API.DAL;
using Library.API.DAL.Abstract;

namespace Library.API.Providers
{
    public class CustomMembershipPovider : MembershipProvider
    {
        private readonly IMemberRepository _memberRepository = new MemberRepository();

        public override bool ValidateUser(string username, string password)
        {
            bool isValid = false;

            try
            {
                var user = _memberRepository.Get(username);

                if (user != null && (Crypto.VerifyHashedPassword(user.Password, password)))
                {
                    isValid = true;
                }
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }

            return isValid;
        }

        public override MembershipUser GetUser(string email, bool userIsOnline)
        {
            try
            {
                var user = _memberRepository.Get(email);

                if (user == null)
                {
                    return null;
                }

                MembershipUser memberUser = new MembershipUser("CustomMembershipPovider", user.Email, null, null, null,
                    null,
                    false, false, user.CreationDate, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue,
                    DateTime.MinValue);

                return memberUser;
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }
        }

        public override MembershipUser CreateUser(string username, string password, string email,
            string passwordQuestion, string passwordAnswer,
            bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            MembershipUser membershipUser = GetUser(email, false);

            if (membershipUser == null)
            {
                try
                {
                    MemberObject member = new MemberObject
                    {
                        MemberName = username,
                        Email = email,
                        Password = Crypto.HashPassword(password)
                    };

                    member = _memberRepository.Create(member);

                    if (member == null)
                    {
                        status = MembershipCreateStatus.UserRejected;
                        return null;
                    }
                    membershipUser = GetUser(email, false);
                    if (membershipUser == null)
                    {
                        status = MembershipCreateStatus.UserRejected;
                        return null;
                    }

                    status = MembershipCreateStatus.Success;
                    return membershipUser;
                }
                catch (Exception exc)
                {
                    throw new Exception(exc.Message);
                }
            }
            status = MembershipCreateStatus.UserRejected;
            return null;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password,
            string newPasswordQuestion,
            string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize,
            out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize,
            out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override string ApplicationName { get; set; }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }
    }
}