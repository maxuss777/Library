using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls.Expressions;
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
            try
            {
                var response = GetObjectsAsList<Book>("GET", UrlResolver.Books_Url, ticket: ticket);
                return response == null || !response.Any() ? null : response;
            }
            catch (Exception exc)
            {
                Logger.Write(exc.Message);
            }
            return null;
        }
        public IEnumerable<Book> GetByCategory(string category, string ticket)
        {
            try
            {
                var response = GetObjectsAsList<Book>("GET", UrlResolver.Books_By_Category_Name_Url(category), ticket: ticket);
                return response == null || !response.Any() ? null : response;
            }
            catch (Exception exc)
            {
                Logger.Write(exc.Message);
            }
            return null;
        }
        public Book GetById(int id, string ticket)
        {
            try
            {
                var response = RequestToApi<Book>("GET", UrlResolver.Books_Id_Url(id), ticket: ticket);
                Book requestOut;
                response.TryGetValue(HttpStatusCode.OK, out requestOut);
                return requestOut;
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
                var postData = JsonConvert.SerializeObject(book);
                var response = RequestToApi<Book>("POST", UrlResolver.Books_Url, postData: postData, ticket: ticket);
                Book requestOut;
                response.TryGetValue(HttpStatusCode.Created, out requestOut);
                return requestOut != null;

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
                var response = RequestToApi<Book>("DELETE", UrlResolver.Books_Id_Url(bookId), ticket: ticket);
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
                var postData = JsonConvert.SerializeObject(book);
                var response = RequestToApi<Book>("PUT", UrlResolver.Books_Id_Url(book.Id), postData: postData, ticket: ticket);
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
        public List<SelectListItem> BooksAsListItems(IEnumerable<Book> books)
        {
            if (books == null || !books.Any())
            {
                return null;
            }
            try
            {
                var listItemCollection = new List<SelectListItem>();
                listItemCollection.AddRange(books.Select(book => new SelectListItem
                {
                    Text = book.Name,
                    Value = book.Id.ToString(),
                    Selected = false
                }));

                return listItemCollection;
            }
            catch (Exception exc)
            {
                Logger.Write(exc.Message);
            }
            return null;
        }
    }
}