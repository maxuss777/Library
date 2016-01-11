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
        public IEnumerable<Category> GetAll(string ticket)
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
        public bool Create(Category category, string ticket)
        {
            var postData = JsonConvert.SerializeObject(category);
            var response = RequestToApi<Book>("POST", UrlResolver.Categories_Url,postData: postData,ticket:ticket);
            Book requestOut;
            response.TryGetValue(HttpStatusCode.Created, out requestOut);
            return requestOut != null;
        }
        public bool Update(Category category, string ticket)
        {
            var postData = JsonConvert.SerializeObject(category);
            var response = RequestToApi<Book>("PUT", UrlResolver.Categories_Id_Url(category.Id), postData: postData, ticket: ticket);
            Book requestOut;
            response.TryGetValue(HttpStatusCode.OK, out requestOut);
            return requestOut != null;
        }
        public Category GetById(int id, string ticket)
        {
            var response = RequestToApi<Category>("GET", UrlResolver.Categories_Id_Url(id), ticket: ticket);
            Category requestOut;
            response.TryGetValue(HttpStatusCode.OK, out requestOut);
            return requestOut;
        }
        public Category GetByName(string categoryName, string ticket)
        {
            var response = RequestToApi<Category>("GET", UrlResolver.Categories_Name_Url(categoryName), ticket: ticket);
            Category requestOut;
            response.TryGetValue(HttpStatusCode.OK, out requestOut);
            return requestOut;
        }
        public bool Delete(int categoryId, string ticket)
        {
            var response = RequestToApi<Book>("DELETE", UrlResolver.Categories_Id_Url(categoryId), ticket: ticket);
            return response.ContainsKey(HttpStatusCode.OK);
        }
        public bool PutBookToCategory(int categoryId, int bookId, string ticket)
        {
            var response = RequestToApi<Book>("POST", UrlResolver.Categories_AddBook(categoryId, bookId), ticket: ticket);
            return response.ContainsKey(HttpStatusCode.OK);
        }
        public bool RemoveBookFromCategory(int categoryId, int bookId, string ticket)
        {
            var response = RequestToApi<Book>("DELETE", UrlResolver.Categories_RemoveBook(categoryId, bookId), ticket: ticket);
            return response.ContainsKey(HttpStatusCode.OK);
        }
    }
}
