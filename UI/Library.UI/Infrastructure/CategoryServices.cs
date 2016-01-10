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
        public bool Update(Category category)
        {
            var postData = JsonConvert.SerializeObject(category);
            var response = RequestToApi<Book>("PUT", UrlResolver.Categories_Id_Url(category.Id), postData);
            Book requestOut;
            response.TryGetValue(HttpStatusCode.OK, out requestOut);
            return requestOut != null;
        }
        public Category GetById(int id)
        {
            var response = RequestToApi<Category>("GET", UrlResolver.Categories_Id_Url(id));
            Category requestOut;
            response.TryGetValue(HttpStatusCode.OK, out requestOut);
            return requestOut;
        }
        public Category GetByName(string categoryName)
        {
            var response = RequestToApi<Category>("GET", UrlResolver.Categories_Name_Url(categoryName));
            Category requestOut;
            response.TryGetValue(HttpStatusCode.OK, out requestOut);
            return requestOut;
        }
        public bool Delete(int categoryId)
        {
            var response = RequestToApi<Book>("DELETE", UrlResolver.Categories_Id_Url(categoryId));
            return response.ContainsKey(HttpStatusCode.OK);
        }
        public bool PutBookToCategory(int categoryId, int bookId)
        {
            var response = RequestToApi<Book>("POST", UrlResolver.Categories_AddBook(categoryId,bookId));
            return response.ContainsKey(HttpStatusCode.OK);
        }
        public bool RemoveBookFromCategory(int categoryId, int bookId)
        {
            var response = RequestToApi<Book>("DELETE", UrlResolver.Categories_RemoveBook(categoryId, bookId));
            return response.ContainsKey(HttpStatusCode.OK);
        }
    }
}
