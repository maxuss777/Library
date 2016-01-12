namespace Library.UI.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Library.UI.Abstract;
    using Library.UI.Helpers;
    using Library.UI.Infrastructure;
    using Library.UI.Models.Books;
    using Newtonsoft.Json;

    public class BookService : Service, IBookService
    {
        public List<Book> GetAll(string ticket)
        {
            try
            {
                return GetObjectsAsList<Book>("GET", UrlResolver.Books_Url, ticket);
            }
            catch (Exception exc)
            {
                Logger.Write(exc.Message);
            }

            return new List<Book>();
        }

        public List<Book> GetByCategory(string category, string ticket)
        {
            try
            {
                return GetObjectsAsList<Book>("GET", UrlResolver.Books_By_Category_Name_Url(category), ticket);
            }
            catch (Exception exc)
            {
                Logger.Write(exc.Message);
            }

            return new List<Book>();
        }

        public Book GetById(int id, string ticket)
        {
            try
            {
                Dictionary<HttpStatusCode, Book> response = RequestToApi<Book>("GET", UrlResolver.Books_Id_Url(id), ticket);

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

        public bool Create(Book book, string ticket)
        {
            try
            {
                string postData = JsonConvert.SerializeObject(book);

                Dictionary<HttpStatusCode, Book> response = RequestToApi<Book>("POST", UrlResolver.Books_Url, postData: postData, ticket: ticket);

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

        public bool Delete(int bookId, string ticket)
        {
            try
            {
                Dictionary<HttpStatusCode, Book> response = RequestToApi<Book>("DELETE", UrlResolver.Books_Id_Url(bookId), ticket);

                return response.ContainsKey(HttpStatusCode.OK);
            }
            catch (Exception exc)
            {
                Logger.Write(exc.Message);
            }

            return false;
        }

        public bool Update(Book book, string ticket)
        {
            try
            {
                string postData = JsonConvert.SerializeObject(book);
                Dictionary<HttpStatusCode, Book> response = RequestToApi<Book>("PUT", UrlResolver.Books_Id_Url(book.Id), postData: postData, ticket: ticket);

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