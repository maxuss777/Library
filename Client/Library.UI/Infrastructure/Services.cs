using System.Collections.Generic;
using System.IO;
using System.Net;
using Library.UI.Models;
using Newtonsoft.Json;

namespace Library.UI.Infrastructure
{
    public class Services
    {
        protected static Dictionary<HttpStatusCode, T> RequestToApi<T>(string requestMethod, string url, string ticket, string postData = "") where T : BaseJsonObject, new()
        {
            HttpStatusCode statusCode = 0;
            var apiResponse = new Dictionary<HttpStatusCode, T>();
            var request = WebRequest.Create(url) as HttpWebRequest;
            if (request == null)
            {
                return null;
            }
            request.Method = requestMethod;

            request.Headers.Add(HttpRequestHeader.Authorization, ticket);

            if (requestMethod == "POST" || requestMethod == "PUT")
            {
                request.Accept = "application/json, text/plain, */*";
                request.Expect = "application/json";
                request.ContentType = "application/json";
                request.ContentLength = postData.Length;

                using (var sw = new StreamWriter(request.GetRequestStream()))
                {
                    sw.Write(postData);
                }
            }

            var obj = GetApiResponse<T>(request, ref statusCode);

            apiResponse.Add(statusCode, obj);

            return apiResponse;
        }
        protected static List<T> GetObjectsAsList<T>(string requestMethod, string url, string ticket) where T : BaseJsonObject, new()
        {
            var request = WebRequest.Create(url) as HttpWebRequest;

            if (request == null)
            {
                return null;
            }

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
                var exceptionStream = e.Response.GetResponseStream();
                var httpStatusCode = ((HttpWebResponse)e.Response).StatusCode;
                if (exceptionStream != null)
                {
                    var sr = new StreamReader(exceptionStream);
                    var s = sr.ReadToEnd();
                    return null;
                }
            }

            if (response == null)
            {
                return null;
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }

            var receiveStream = response.GetResponseStream();

            if (receiveStream == null)
            {
                return null;
            }

            using (var readStream = new StreamReader(receiveStream))
            {
                var stream = readStream.ReadToEnd();
                readStream.Close();
                receiveStream.Close();
                response.Close();
                return JsonConvert.DeserializeObject<List<T>>(stream);
            }
        }
        protected static IEnumerable<KeyValuePair<string, int>> GetObjectsAsDictionary(string requestMethod, string url, string ticket)
        {
            var request = WebRequest.Create(url) as HttpWebRequest;

            if (request == null)
            {
                return null;
            }

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
                var exceptionStream = e.Response.GetResponseStream();
                var httpStatusCode = ((HttpWebResponse)e.Response).StatusCode;
                if (exceptionStream != null)
                {
                    var sr = new StreamReader(exceptionStream);
                    var s = sr.ReadToEnd();
                    return null;
                }
            }

            if (response == null)
            {
                return null;
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }

            var receiveStream = response.GetResponseStream();

            if (receiveStream == null)
            {
                return null;
            }

            using (var readStream = new StreamReader(receiveStream))
            {
                var stream = readStream.ReadToEnd();
                readStream.Close();
                receiveStream.Close();
                response.Close();
                return JsonConvert.DeserializeObject<Dictionary<string, int>>(stream);
            }
        }
        private static T GetApiResponse<T>(WebRequest request, ref HttpStatusCode statusCode) where T : BaseJsonObject, new()
        {
            var apiRespose = new T();

            HttpWebResponse response = null;
            try
            {
                response = request.GetResponse() as HttpWebResponse;
            }
            catch (WebException e)
            {
                var exceptionStream = e.Response.GetResponseStream();
                statusCode = ((HttpWebResponse)e.Response).StatusCode;
                if (exceptionStream != null)
                {
                    var sr = new StreamReader(exceptionStream);
                    var s = sr.ReadToEnd();
                    return JsonConvert.DeserializeObject<T>(s);
                }
            }

            if (response == null)
            {
                return apiRespose;
            }

            var receiveStream = response.GetResponseStream();

            if (receiveStream == null)
            {
                return apiRespose;
            }

            using (var readStream = new StreamReader(receiveStream))
            {
                var stream = readStream.ReadToEnd();
                readStream.Close();
                receiveStream.Close();
                statusCode = response.StatusCode;
                response.Close();
                apiRespose = JsonConvert.DeserializeObject<T>(stream);
            }
            return apiRespose;
        }
    }
}