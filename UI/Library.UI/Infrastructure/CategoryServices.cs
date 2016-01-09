using System.Collections.Generic;
using System.IO;
using System.Net;
using Library.UI.Abstract;
using Library.UI.Helpers;
using Library.UI.Models;
using Newtonsoft.Json;

namespace Library.UI.Infrastructure
{
    public class CategoryServices : Services, ICategoryServices
    {
        public IEnumerable<Category> GetAll()
        {
            List<Category> categoryList = new List<Category>();

            WebRequest request = WebRequest.Create(UrlResolver.Categories_GetAll);

            request.Credentials = CredentialCache.DefaultCredentials;

            using (WebResponse response = request.GetResponse())
            {
                Stream dataStream = response.GetResponseStream();

                if (dataStream == null) return categoryList;

                using (StreamReader reader = new StreamReader(dataStream))
                {
                    categoryList = JsonConvert.DeserializeObject<List<Category>>(reader.ReadToEnd());
                }
            }
            return categoryList;
        }

        public bool Create(Category category)
        {
            var postData = JsonConvert.SerializeObject(category);
            var response = RequestToApi<Book>("POST", UrlResolver.Categories_Url, postData);
            Book requestOut;
            response.TryGetValue(HttpStatusCode.Created, out requestOut);
            return requestOut != null;
        }
    }
}
