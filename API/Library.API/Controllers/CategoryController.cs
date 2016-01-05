using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Library.API.Business.Abstract;
using Library.API.Common.Category;

namespace Library.API.Controllers
{
    //[Authorize]
    public class CategoryController : ApiController
    {
        private readonly ICategoryServices _categoryServices;

        public CategoryController(ICategoryServices categoryServ)
        {
            _categoryServices = categoryServ;
        }
        
        [HttpGet]
        [Route("api/categories")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                var categoryList = _categoryServices.GetAllCategories();
                return categoryList != null
                    ? Request.CreateResponse(HttpStatusCode.OK, categoryList)
                    : Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Sorry, there is no any category yet");
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc.Message);
            }
        }

        [HttpGet]
        [Route("api/categories/{id:int}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var category = _categoryServices.GetCategoryById(id);
                return category != null
                    ? Request.CreateResponse(HttpStatusCode.OK, category)
                    : Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        string.Format("The category with id = {0} doesn't exist", id));
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc.Message);

            }
        }

        [HttpPost]
        [Route("api/categories")]
        public HttpResponseMessage Create(CategoryInfo categoryToBeCreated)
        {
            try
            {
                var createdCategory = _categoryServices.CreateCategory(categoryToBeCreated);
                return createdCategory != null
                    ? Request.CreateResponse(HttpStatusCode.Created, createdCategory)
                    : Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        "Sorry, some troubles with the category creation");
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc.Message);
            }
        }

        [HttpPut]
        [Route("api/categories/{id:int}")]
        public HttpResponseMessage Update(int id, CategoryObject categoryToBeUptated)
        {
            try
            {
                categoryToBeUptated.Id = id;
                var updatedCategory = _categoryServices.UpdateCategory(categoryToBeUptated);
                return updatedCategory != null
                    ? Request.CreateResponse(HttpStatusCode.OK, updatedCategory)
                    : Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        "Sorry, the category doesn't exist");
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc.Message);
            }
        }

        [HttpDelete]
        [Route("api/categories/{id:int}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var result = _categoryServices.DeleteCategory(id);

                return result
                    ? Request.CreateResponse(HttpStatusCode.OK)
                    : Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        "Sorry, the category doesn't exist");
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc.Message);
            }
        }

        [HttpPost]
        [Route("api/categories/{categoryId:int}/addbook/{bookId:int}")]
        public HttpResponseMessage AddBook(int categoryId, int bookId)
        {
            try
            {
                var book = _categoryServices.PutBookToCategory(categoryId, bookId);
                return book == false
                    ? Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The book hasn't been put to the category")
                    : Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc.Message);
            }
        }

        [HttpDelete]
        [Route("api/categories/{categoryId:int}/removebook/{bookId:int}")]
        public HttpResponseMessage RemoveBook(int categoryId, int bookId)
        {
            try
            {
                var book = _categoryServices.RemoveBookFromCategory(categoryId, bookId);
                return book == false
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