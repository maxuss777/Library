using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Library.API.Business.Abstract;
using Library.API.Common.Category;

namespace Library.API.Controllers
{
    [Authorize]
    public class CategoryController : ApiController
    {
        private readonly ICategoryServices _categoryServices;

        public CategoryController(ICategoryServices categoryServ)
        {
            _categoryServices = categoryServ;
        }
        
        public HttpResponseMessage GETCategories()
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

        public HttpResponseMessage GETCategory(int id)
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

        public HttpResponseMessage POSTCategory(CategoryObject categoryToBeCreated)
        {
            try
            {
                var createdCategory = _categoryServices.CreateCategory(categoryToBeCreated);
                return createdCategory != null
                    ? Request.CreateResponse(HttpStatusCode.OK, createdCategory)
                    : Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        "Sorry, some troubles with the category creation");
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc.Message);
            }
        }

        public HttpResponseMessage PUTCategory(int id, CategoryObject categoryToBeUptated)
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

        public HttpResponseMessage DELETECategory(int id)
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
    }
}