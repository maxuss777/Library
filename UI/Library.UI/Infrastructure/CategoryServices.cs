using System.Collections.Generic;
using System.IO;
using System.Net;
using Library.UI.Abstract;
using Library.UI.Models;
using Newtonsoft.Json;

namespace Library.UI.Infrastructure
{
    public class CategoryServices : ICategoryServices
    {
        public List<Category> GetAll()
        {
            List<Category> categoryList = new List<Category>();

            WebRequest request = WebRequest.Create("http://localhost:1690/api/categories");
            request.Credentials = CredentialCache.DefaultCredentials;

            using (WebResponse response = request.GetResponse())
            {
                Stream dataStream = response.GetResponseStream();

                if (dataStream != null)
                {
                    using (StreamReader reader = new StreamReader(dataStream))
                    {
                        categoryList = JsonConvert.DeserializeObject<List<Category>>(reader.ReadToEnd());
                    }
                }
            }
            return categoryList;
        }
    }
}
