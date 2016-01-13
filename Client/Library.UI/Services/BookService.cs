using Library.UI.Interfaces;

namespace Library.UI.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Library.UI.Helpers;
    using Library.UI.Infrastructure;
    using Library.UI.Models.Books;
    using Newtonsoft.Json;

    public class BookService : Service, IBookService
    {
        public List<Book> GetAll()
        {
            try
            {
                return GetObjectsAsList<Book>("GET", UrlResolver.GetApiBooksUrl);
            }
            catch (Exception exc)
            {
                Logger.Write(exc.Message);
            }

            return new List<Book>();
        }

        public List<Book> GetByCategory(string category)
        {
            try
            {
                return GetObjectsAsList<Book>("GET", UrlResolver.GetApiBooksByCategoryNameUrl(category));
            }
            catch (Exception exc)
            {
                Logger.Write(exc.Message);
            }

            return new List<Book>();
        }

        public Book GetById(int id)
        {
            try
            {
                Dictionary<HttpStatusCode, Book> response = RequestToApi<Book>("GET", UrlResolver.GetApiBooksByIdUrl(id));

                Book book;
                response.TryGetValue(HttpStatusCode.OK, out book);

                return book;
            }
            catch (Exception exc)
            {
                Logger.Write(exc.Message);
            }

            return null;
        }

        public bool Create(Book book)
        {
            try
            {
                string postData = JsonConvert.SerializeObject(book);

                Dictionary<HttpStatusCode, Book> response = RequestToApi<Book>("POST", UrlResolver.GetApiBooksUrl, postData);

                Book tempBook;
                response.TryGetValue(HttpStatusCode.Created, out tempBook);

                return tempBook != null;
            }
            catch (Exception exc)
            {
                Logger.Write(exc.Message);
            }

            return false;
        }

        public bool Delete(int bookId)
        {
            try
            {
                Dictionary<HttpStatusCode, Book> response = RequestToApi<Book>("DELETE", UrlResolver.GetApiBooksByIdUrl(bookId));

                return response.ContainsKey(HttpStatusCode.OK);
            }
            catch (Exception exc)
            {
                Logger.Write(exc.Message);
            }

            return false;
        }

        public bool Update(Book book)
        {
            try
            {
                string postData = JsonConvert.SerializeObject(book);
                Dictionary<HttpStatusCode, Book> response = RequestToApi<Book>("PUT", UrlResolver.GetApiBooksByIdUrl(book.Id), postData);

                Book requestOut;
                response.TryGetValue(HttpStatusCode.OK, out requestOut);

                return requestOut != null;
            }
            catch (Exception exc)
            {
                Logger.Write(exc.Message);
            }

            return false;
        }

        public List<SelectListItem> BooksAsListItems(List<Book> books)
        {
            if (books.Count == 0)
            {
                return new List<SelectListItem>();
            }

            try
            {
                List<SelectListItem> listItemCollection = new List<SelectListItem>();

                listItemCollection.AddRange(books.Select(book => new SelectListItem
                {
                    Text = book.Name,
                    Value = book.Id.ToString(CultureInfo.InvariantCulture),
                    Selected = false
                }));

                return listItemCollection;
            }
            catch (Exception exc)
            {
                Logger.Write(exc.Message);
            }

            return new List<SelectListItem>();
        }
    }
}