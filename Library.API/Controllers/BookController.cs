using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Library.API.Business.Abstract;
using Library.API.Common.Book;

namespace Library.API.Controllers
{
    [Authorize]
    public class BookController : ApiController
    {
        private readonly IBookServices _bookServices;

        public BookController(IBookServices bookServ)
        {
            _bookServices = bookServ;
        }

        [HttpGet]
        [Route("api/books")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                var booksList = _bookServices.GetAllBooks();
                return booksList != null
                    ? Request.CreateResponse(HttpStatusCode.OK, booksList)
                    : Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Sorry, there is no any book yet");
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc.Message);
            }
        }

        [HttpGet]
        [Route("api/books/{id:int}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var book = _bookServices.GetBookById(id);
                return book != null
                    ? Request.CreateResponse(HttpStatusCode.OK, book)
                    : Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        string.Format("The book with id = {0} doesn't exist", id));
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc.Message);

            }
        }

        [HttpPost]
        [Route("api/books")]
        public HttpResponseMessage Create(BookInfo bookToBeCreated)
        {
            try
            {
                var createdBook = _bookServices.CreateBook(bookToBeCreated);
                return createdBook != null
                    ? Request.CreateResponse(HttpStatusCode.OK, createdBook)
                    : Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        "Sorry, some troubles with the book creation");
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc.Message);
            }
        }

        [HttpPut]
        [Route("api/books/{id:int}")]
        public HttpResponseMessage Update(int id, BookObject bookToBeUptated)
        {
            try
            {
                bookToBeUptated.Id = id;
                var updatedBook = _bookServices.UpdateBook(bookToBeUptated);
                return updatedBook != null
                    ? Request.CreateResponse(HttpStatusCode.OK, updatedBook)
                    : Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        "Sorry, the book doesn't exist");
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc.Message);
            }
        }

        [HttpDelete]
        [Route("api/books/{id:int}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var result = _bookServices.DeleteBook(id);

                return result
                    ? Request.CreateResponse(HttpStatusCode.OK)
                    : Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        "Sorry, the book doesn't exist");
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc.Message);
            }
        }
    }
}
