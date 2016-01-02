using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Library.API.Business.Abstract;
using Library.API.Common.Book;

namespace Library.API.Controllers
{
    public class BookController : ApiController
    {
        private readonly IBookServices _bookServices;

        public BookController(IBookServices bookServ)
        {
            _bookServices = bookServ;
        }

        public HttpResponseMessage GETBooks()
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

        public HttpResponseMessage GETBook(int id)
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

        public HttpResponseMessage POSTBook(BookObject bookToBeCreated)
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

        public HttpResponseMessage PUTBook(int id, BookObject bookToBeUptated)
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

        public HttpResponseMessage DELETEBook(int id)
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
