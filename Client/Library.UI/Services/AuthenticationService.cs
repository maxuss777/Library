namespace Library.UI.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Library.UI.Abstract;
    using Library.UI.Helpers;
    using Library.UI.Infrastructure;
    using Library.UI.Models.Account;
    using Newtonsoft.Json;

    public class AuthenticationService : Service, IAuthenticationService
    {
        public MyAuthorizationHeader Login(LoginModel loginModel)
        {
            if (loginModel == null)
            {
                return null;
            }

            try
            {
                string postData = JsonConvert.SerializeObject(loginModel);

                Dictionary<HttpStatusCode, MyAuthorizationHeader> response = RequestToApi<MyAuthorizationHeader>("POST", UrlResolver.Api_Login, null, postData);

                MyAuthorizationHeader requestOut;
                response.TryGetValue(HttpStatusCode.OK, out requestOut);

                return requestOut == null || string.IsNullOrEmpty(requestOut.Ticket)
                    ? null
                    : requestOut;
            }
            catch (Exception exc)
            {
                Logger.Write(exc.Message + DateTime.Now);
            }

            return null;
        }

        public Dictionary<HttpStatusCode, MyAuthorizationHeader> Register(RegistrationModel regModel)
        {
            if (regModel == null)
            {
                return null;
            }

            try
            {
                string postData = JsonConvert.SerializeObject(regModel);

                return RequestToApi<MyAuthorizationHeader>("POST", UrlResolver.Api_Registration, null, postData);
            }
            catch (Exception exc)
            {
                Logger.Write(exc.Message + DateTime.Now);
            }

            return null;
        }
    }
}