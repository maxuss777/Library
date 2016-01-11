using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Library.UI.Abstract;
using Library.UI.Helpers;
using Library.UI.Models;
using Newtonsoft.Json;

namespace Library.UI.Infrastructure
{
    public class BookServices : Services, IBookServices
    {
        public IEnumerable<Book> GetAll(string ticket)
        {
            var response = GetObjectsAsList<Book>("GET", UrlResolver.Books_Url,ticket:ticket);

            return !response.Any() ? null : response;
        }
        public IEnumerable<Book> GetByCategory(string category, string ticket)
        {
            var response = GetObjectsAsList<Book>("GET", UrlResolver.Books_By_Category_Name_Url(category),ticket:ticket);

            return response == null || !response.Any() ? null : response;
        }
        public Book GetById(int id, string ticket)
        {
            var response = RequestToApi<Book>("GET", UrlResolver.Books_Id_Url(id),ticket:ticket);
            Book requestOut;
            response.TryGetValue(HttpStatusCode.OK, out requestOut);
            return requestOut;
        }
        public bool Create(Book book, string ticket)
        {
            var postData = JsonConvert.SerializeObject(book);
            var response = RequestToApi<Book>("POST", UrlResolver.Books_Url, postData:postData, ticket: ticket);
            Book requestOut;
            response.TryGetValue(HttpStatusCode.Created, out requestOut);
            return requestOut != null;
        }
        public bool Delete(int bookId, string ticket)
        {
            var response = RequestToApi<Book>("DELETE", UrlResolver.Books_Id_Url(bookId), ticket: ticket);

            return response.ContainsKey(HttpStatusCode.OK);
        }
        public bool Update(Book book, string ticket)
        {
            var postData = JsonConvert.SerializeObject(book);
            var response = RequestToApi<Book>("PUT", UrlResolver.Books_Id_Url(book.Id), postData:postData,ticket:ticket);
            Book requestOut;
            response.TryGetValue(HttpStatusCode.OK, out requestOut);
            return requestOut != null;
        }
        public List<SelectListItem> BooksAsListItems(IEnumerable<Book> books)
        {
            if (!books.Any())
            {
                return null;
            }
            var listItemCollection = new List<SelectListItem>();
            listItemCollection.AddRange(books.Select(book => new SelectListItem
            {
                Text = book.Name, Value = book.Id.ToString(), Selected = false
            }));

            return listItemCollection;
        }
    }
}