﻿namespace Library.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Library.API.Business.Interfaces;
    using Library.API.Model;

    [Authorize]
    [RoutePrefix("api/categories")]
    public class CategoryController : ApiController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                List<Category> categoryList = _categoryService.GetAllCategories();

                return categoryList.Count > 0
                    ? Request.CreateResponse(HttpStatusCode.OK, categoryList)
                    : Request.CreateErrorResponse(HttpStatusCode.NotFound, "Sorry, there is no any category yet");
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc.Message);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public HttpResponseMessage GetOne(int id)
        {
            try
            {
                Category category = _categoryService.GetCategoryById(id);

                return category != null
                    ? Request.CreateResponse(HttpStatusCode.OK, category)
                    : Request.CreateErrorResponse(HttpStatusCode.NotFound, string.Format("The category with id = {0} doesn't exist", id));
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc.Message);
            }
        }

        [HttpGet]
        [Route("{categoryName}")]
        public HttpResponseMessage GetByName(string categoryName)
        {
            try
            {
                Category category = _categoryService.GetCategoryByName(categoryName);

                return category != null
                    ? Request.CreateResponse(HttpStatusCode.OK, category)
                    : Request.CreateErrorResponse(HttpStatusCode.NotFound, string.Format("The category with Name = {0} doesn't exist", categoryName));
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc.Message);
            }
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Create(Category categoryToBeCreated)
        {
            try
            {
                Category createdCategory = _categoryService.CreateCategory(categoryToBeCreated);

                return createdCategory != null
                    ? Request.CreateResponse(HttpStatusCode.Created, createdCategory)
                    : Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Sorry, some troubles with the category creation");
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc.Message);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public HttpResponseMessage Update(int id, Category category)
        {
            try
            {
                category.Id = id;

                Category updatedCategory = _categoryService.UpdateCategory(category);

                return updatedCategory != null
                    ? Request.CreateResponse(HttpStatusCode.OK, updatedCategory)
                    : Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Sorry, the category doesn't exist");
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc.Message);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                bool isDeleted = _categoryService.DeleteCategory(id);

                return isDeleted
                    ? Request.CreateResponse(HttpStatusCode.OK)
                    : Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Sorry, the category doesn't exist");
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc.Message);
            }
        }

        [HttpPost]
        [Route("{categoryId:int}/addbook/{bookId:int}")]
        public HttpResponseMessage AddBook(int categoryId, int bookId)
        {
            try
            {
                bool isAssigned = _categoryService.PutBookToCategory(categoryId, bookId);

                return isAssigned == false
                    ? Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The book hasn't been put to the category")
                    : Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc.Message);
            }
        }

        [HttpDelete]
        [Route("{categoryId:int}/removebook/{bookId:int}")]
        public HttpResponseMessage RemoveBook(int categoryId, int bookId)
        {
            try
            {
                bool isRemoved = _categoryService.RemoveBookFromCategory(categoryId, bookId);

                return isRemoved == false
                    ? Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The book hasn't been removed from the category")
                    : Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc.Message);
            }
        }
    }
}