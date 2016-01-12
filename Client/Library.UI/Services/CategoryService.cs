namespace Library.UI.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Library.UI.Abstract;
    using Library.UI.Helpers;
    using Library.UI.Infrastructure;
    using Library.UI.Models.Books;
    using Library.UI.Models.Categories;
    using Newtonsoft.Json;

    public class CategoryService : Service, ICategoryService
    {
        public List<Category> GetAll(string ticket)
        {
            try
            {
                return GetObjectsAsList<Category>("GET", UrlResolver.Categories_Url, ticket);
            }
            catch (Exception exc)
            {
                Logger.Write(exc.Message);
            }

            return new List<Category>();
        }

        public bool Create(Category category, string ticket)
        {
            try
            {
                string postData = JsonConvert.SerializeObject(category);
                Dictionary<HttpStatusCode, Book> response = RequestToApi<Book>("POST", UrlResolver.Categories_Url, postData: postData, ticket: ticket);

                Book book;
                response.TryGetValue(HttpStatusCode.Created, out book);

                return book != null;
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
                string postData = JsonConvert.SerializeObject(category);

                Dictionary<HttpStatusCode, Book> response = RequestToApi<Book>("PUT", UrlResolver.Categories_Id_Url(category.Id), postData: postData, ticket: ticket);

                Book book;
                response.TryGetValue(HttpStatusCode.OK, out book);

                return book != null;
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
                Dictionary<HttpStatusCode, Category> response = RequestToApi<Category>("GET", UrlResolver.Categories_Id_Url(id), ticket: ticket);

                Category category;
                response.TryGetValue(HttpStatusCode.OK, out category);

                return category;
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
                Dictionary<HttpStatusCode, Category> response = RequestToApi<Category>("GET", UrlResolver.Categories_Name_Url(categoryName), ticket: ticket);

                Category category;
                response.TryGetValue(HttpStatusCode.OK, out category);

                return category;
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
                Dictionary<HttpStatusCode, Book> response = RequestToApi<Book>("DELETE", UrlResolver.Categories_Id_Url(categoryId), ticket);

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
                Dictionary<HttpStatusCode, Book> response = RequestToApi<Book>("POST", UrlResolver.Categories_AddBook(categoryId, bookId), ticket);

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
                Dictionary<HttpStatusCode, Book> response = RequestToApi<Book>("DELETE", UrlResolver.Categories_RemoveBook(categoryId, bookId), ticket);

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