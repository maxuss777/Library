using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            try
            {
                var response = GetObjectsAsList<Category>("GET", UrlResolver.Categories_Url, ticket: ticket);

                return response == null || !response.Any() ? null : response;
            }
            catch(Exception exc)
            {
                Logger.Write(exc.Message);
            }
            return null;
        }

        public bool Create(Category category, string ticket)
        {
            try
            {
                var postData = JsonConvert.SerializeObject(category);
                var response = RequestToApi<Book>("POST", UrlResolver.Categories_Url, postData: postData, ticket: ticket);
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
        public bool Update(Category category, string ticket)
        {
            try
            {
                var postData = JsonConvert.SerializeObject(category);
                var response = RequestToApi<Book>("PUT", UrlResolver.Categories_Id_Url(category.Id), postData: postData, ticket: ticket);
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
        public Category GetById(int id, string ticket)
        {
            try
            {
                var response = RequestToApi<Category>("GET", UrlResolver.Categories_Id_Url(id), ticket: ticket);
                Category requestOut;
                response.TryGetValue(HttpStatusCode.OK, out requestOut);
                return requestOut;
            }
            catch (Exception exc)
            {
                Logger.Write(exc.Message);
            }
            return null;
            
        }
        public Category GetByName(string categoryName, string ticket)
        {
            try
            {
                var response = RequestToApi<Category>("GET", UrlResolver.Categories_Name_Url(categoryName), ticket: ticket);
                Category requestOut;
                response.TryGetValue(HttpStatusCode.OK, out requestOut);
                return requestOut;
            }
            catch (Exception exc)
            {
                Logger.Write(exc.Message);
            }
            return null;
            
        }
        public bool Delete(int categoryId, string ticket)
        {
            try
            {
                var response = RequestToApi<Book>("DELETE", UrlResolver.Categories_Id_Url(categoryId), ticket: ticket);
                return response.ContainsKey(HttpStatusCode.OK);
            }
            catch (Exception exc)
            {
                Logger.Write(exc.Message);
            }
            return false;
        }
        public bool PutBookToCategory(int categoryId, int bookId, string ticket)
        {
            try
            {
                var response = RequestToApi<Book>("POST", UrlResolver.Categories_AddBook(categoryId, bookId), ticket: ticket);
                return response.ContainsKey(HttpStatusCode.OK);
            }
            catch (Exception exc)
            {
                Logger.Write(exc.Message);
            }
            return false;
        }
        public bool RemoveBookFromCategory(int categoryId, int bookId, string ticket)
        {
            try
            {
                var response = RequestToApi<Book>("DELETE", UrlResolver.Categories_RemoveBook(categoryId, bookId), ticket: ticket);
                return response.ContainsKey(HttpStatusCode.OK);
            }
            catch (Exception exc)
            {
                Logger.Write(exc.Message);
            }
            return false;
        }
    }
}
