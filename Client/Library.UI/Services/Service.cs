using System.Web;
using Library.UI.Infrastructure;

namespace Library.UI.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using Library.UI.Models;
    using Newtonsoft.Json;

    public class Service
    {
        protected static Dictionary<HttpStatusCode, T> RequestToApi<T>(string requestMethod, string url, string postData = "")
            where T : BaseJsonObject, new()
        {
            HttpStatusCode statusCode = 0;

            Dictionary<HttpStatusCode, T> apiResponse = new Dictionary<HttpStatusCode, T>();

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            if (request == null)
            {
                return new Dictionary<HttpStatusCode, T>();
            }

            string ticket = GetAuthorizationCookies();
            request.Method = requestMethod;
            request.Headers.Add(HttpRequestHeader.Authorization, ticket);

            if (requestMethod == "POST" || requestMethod == "PUT")
            {
                request.Accept = "application/json, text/plain, */*";
                request.Expect = "application/json";
                request.ContentType = "application/json";
                request.ContentLength = postData.Length;

                using (StreamWriter sw = new StreamWriter(request.GetRequestStream()))
                {
                    sw.Write(postData);
                }
            }

            T obj = GetApiResponse<T>(request, ref statusCode);

            apiResponse.Add(statusCode, obj);

            return apiResponse;
        }

        protected static List<T> GetObjectsAsList<T>(string requestMethod, string url) where T : BaseJsonObject, new()
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

            if (request == null)
            {
                return new List<T>();
            }

            string ticket = GetAuthorizationCookies();
            request.Method = requestMethod;
            request.ContentType = "application/json";
            request.Accept = "application/json, text/plain, #1#*";
            request.Expect = "application/json";

            request.Headers.Add(HttpRequestHeader.Authorization, ticket);

            HttpWebResponse response = null;

            try
            {
                response = request.GetResponse() as HttpWebResponse;
            }
            catch (WebException exception)
            {
                Stream exceptionStream = exception.Response.GetResponseStream();
                HttpStatusCode httpStatusCode = ((HttpWebResponse) exception.Response).StatusCode;
                if (exceptionStream != null)
                {
                    StreamReader sr = new StreamReader(exceptionStream);
                    string s = sr.ReadToEnd();
                    Logger.Write(httpStatusCode + s);
                    return new List<T>();
                }
            }

            if (response == null)
            {
                return new List<T>();
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return new List<T>();
            }

            Stream receiveStream = response.GetResponseStream();

            if (receiveStream == null)
            {
                return new List<T>();
            }

            using (StreamReader readStream = new StreamReader(receiveStream))
            {
                string stream = readStream.ReadToEnd();
                readStream.Close();
                receiveStream.Close();
                response.Close();

                return JsonConvert.DeserializeObject<List<T>>(stream);
            }
        }

        protected static List<KeyValuePair<string, int>> GetObjectsAsListKeyValuePairs(string requestMethod, string url)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

            if (request == null)
            {
                return new List<KeyValuePair<string, int>>();
            }

            string ticket = GetAuthorizationCookies();
            request.Method = requestMethod;
            request.ContentType = "application/json";
            request.Accept = "application/json, text/plain, #1#*";
            request.Expect = "application/json";

            request.Headers.Add(HttpRequestHeader.Authorization, ticket);

            HttpWebResponse response = null;

            try
            {
                response = request.GetResponse() as HttpWebResponse;
            }
            catch (WebException e)
            {
                Stream exceptionStream = e.Response.GetResponseStream();
                HttpStatusCode httpStatusCode = ((HttpWebResponse) e.Response).StatusCode;
                if (exceptionStream != null)
                {
                    StreamReader sr = new StreamReader(exceptionStream);
                    string s = sr.ReadToEnd();
                    Logger.Write(httpStatusCode + " " + s);
                    return new List<KeyValuePair<string, int>>();
                }
            }

            if (response == null)
            {
                return new List<KeyValuePair<string, int>>();
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return new List<KeyValuePair<string, int>>();
            }

            Stream receiveStream = response.GetResponseStream();

            if (receiveStream == null)
            {
                return new List<KeyValuePair<string, int>>();
            }

            using (StreamReader readStream = new StreamReader(receiveStream))
            {
                string stream = readStream.ReadToEnd();
                readStream.Close();
                receiveStream.Close();
                response.Close();

                return JsonConvert.DeserializeObject<List<KeyValuePair<string, int>>>(stream);
            }
        }

        private static T GetApiResponse<T>(WebRequest request, ref HttpStatusCode statusCode) where T : BaseJsonObject, new()
        {
            T apiRespose = new T();

            HttpWebResponse response = null;
            try
            {
                response = request.GetResponse() as HttpWebResponse;
            }
            catch (WebException e)
            {
                Stream exceptionStream = e.Response.GetResponseStream();
                statusCode = ((HttpWebResponse) e.Response).StatusCode;
                if (exceptionStream != null)
                {
                    StreamReader sr = new StreamReader(exceptionStream);
                    string s = sr.ReadToEnd();
                    Logger.Write(statusCode + " " + s);
                    return null;
                }
            }

            if (response == null)
            {
                return apiRespose;
            }

            Stream receiveStream = response.GetResponseStream();

            if (receiveStream == null)
            {
                return apiRespose;
            }

            using (StreamReader readStream = new StreamReader(receiveStream))
            {
                string stream = readStream.ReadToEnd();
                readStream.Close();
                receiveStream.Close();
                statusCode = response.StatusCode;
                response.Close();

                apiRespose = JsonConvert.DeserializeObject<T>(stream);
            }

            return apiRespose;
        }

        private static string GetAuthorizationCookies()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["_auth"];

            return cookie != null ? cookie.Value : null;
        }
    }
}