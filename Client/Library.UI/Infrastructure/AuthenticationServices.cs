using System.Net;
using Library.UI.Abstract;
using Library.UI.Helpers;
using Library.UI.Models;
using Newtonsoft.Json;

namespace Library.UI.Infrastructure
{
    public class AuthenticationServices : Services, IAuthenticationController
    {
        public MyAuthorizationHeader LogIn(LogInModel logInModel)
        {
            if (logInModel == null)
                return null;

            var postData = JsonConvert.SerializeObject(logInModel);
            var response = RequestToApi<MyAuthorizationHeader>("POST", UrlResolver.Api_Login, null, postData: postData);
            MyAuthorizationHeader requestOut;
            response.TryGetValue(HttpStatusCode.OK, out requestOut);
            return requestOut == null || requestOut.Ticket == null || requestOut.Ticket == ""
                ? null
                : requestOut;
        }

        public MyAuthorizationHeader Register(RegistrationModel regModel)
        {
            if (regModel == null)
                return null;

            var postData = JsonConvert.SerializeObject(regModel);
            var response = RequestToApi<MyAuthorizationHeader>("POST", UrlResolver.Api_Login,null, postData: postData);
            MyAuthorizationHeader requestOut;
            response.TryGetValue(HttpStatusCode.OK, out requestOut);
            return requestOut == null || requestOut.Ticket == null || requestOut.Ticket == ""
                ? null
                : requestOut;
        }
    }
}