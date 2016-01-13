using Library.UI.Interfaces;

namespace Library.UI.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Library.UI.Helpers;
    using Library.UI.Infrastructure;
    using Library.UI.Models.Books;
    using Library.UI.Models.Categories;
    using Newtonsoft.Json;

    public class CategoryService : Service, ICategoryService
    {
        public List<Category> GetAll()
        {
            try
            {
                return GetObjectsAsList<Category>("GET", UrlResolver.GetApiCategoriesUrl);
            }
            catch (Exception exc)
            {
                Logger.Write(exc.Message);
            }

            return new List<Category>();
        }

        public bool Create(Category category)
        {
            try
            {
                string postData = JsonConvert.SerializeObject(category);
                Dictionary<HttpStatusCode, Book> response = RequestToApi<Book>("POST", UrlResolver.GetApiCategoriesUrl, postData);

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

        public bool Update(Category category)
        {
            try
            {
                string postData = JsonConvert.SerializeObject(category);

                Dictionary<HttpStatusCode, Book> response = RequestToApi<Book>("PUT", UrlResolver.GetCategoriesByIdUrl(category.Id), postData);

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

        public Category GetById(int id)
        {
            try
            {
                Dictionary<HttpStatusCode, Category> response = RequestToApi<Category>("GET", UrlResolver.GetCategoriesByIdUrl(id));

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

        public Category GetByName(string categoryName)
        {
            try
            {
                Dictionary<HttpStatusCode, Category> response = RequestToApi<Category>("GET", UrlResolver.GetCategoriesByNameUrl(categoryName));

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

        public bool Delete(int categoryId)
        {
            try
            {
                Dictionary<HttpStatusCode, Book> response = RequestToApi<Book>("DELETE", UrlResolver.GetCategoriesByIdUrl(categoryId));

                return response.ContainsKey(HttpStatusCode.OK);
            }
            catch (Exception exc)
            {
                Logger.Write(exc.Message);
            }

            return false;
        }

        public bool PutBookToCategory(int categoryId, int bookId)
        {
            try
            {
                Dictionary<HttpStatusCode, Book> response = RequestToApi<Book>("POST", UrlResolver.GetCategoriesAddBookUrl(categoryId, bookId));

                return response.ContainsKey(HttpStatusCode.OK);
            }
            catch (Exception exc)
            {
                Logger.Write(exc.Message);
            }

            return false;
        }

        public bool RemoveBookFromCategory(int categoryId, int bookId)
        {
            try
            {
                Dictionary<HttpStatusCode, Book> response = RequestToApi<Book>("DELETE", UrlResolver.GetCategoriesRemoveBookUrl(categoryId, bookId));

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