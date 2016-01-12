namespace Library.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Library.API.Business.Interfaces;
    using Library.API.Model;

    [Authorize]
    [RoutePrefix("api/books")]
    public class BookController : ApiController
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                List<Book> books = _bookService.GetAllBooks();

                return books.Count > 0
                    ? Request.CreateResponse(HttpStatusCode.OK, books)
                    : Request.CreateErrorResponse(HttpStatusCode.NotFound, "Sorry, there is no any book yet");
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc.Message);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public HttpResponseMessage GetById(int id)
        {
            try
            {
                Book book = _bookService.GetBookById(id);

                return book != null
                    ? Request.CreateResponse(HttpStatusCode.OK, book)
                    : Request.CreateErrorResponse(HttpStatusCode.NotFound, string.Format("The book with id = {0} doesn't exist", id));
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc.Message);
            }
        }

        [HttpGet]
        [Route("{categoryName}")]
        public HttpResponseMessage GetByCategory(string categoryName)
        {
            try
            {
                List<Book> book = _bookService.GetBooksByCategoryName(categoryName);

                return book.Count > 0
                    ? Request.CreateResponse(HttpStatusCode.OK, book)
                    : Request.CreateErrorResponse(HttpStatusCode.NotFound, string.Format("The books don't belong '{0}' category", categoryName));
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc.Message);
            }
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Create(Book book)
        {
            try
            {
                Book createdBook = _bookService.CreateBook(book);

                return createdBook != null
                    ? Request.CreateResponse(HttpStatusCode.Created, createdBook)
                    : Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Sorry, some troubles with the book creation");
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc.Message);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public HttpResponseMessage Update(int id, Book book)
        {
            try
            {
                book.Id = id;
                Book updatedBook = _bookService.UpdateBook(book);

                return updatedBook != null
                    ? Request.CreateResponse(HttpStatusCode.OK, updatedBook)
                    : Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Sorry, the book doesn't exist");
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
                bool isDeleted = _bookService.DeleteBook(id);

                return isDeleted
                    ? Request.CreateResponse(HttpStatusCode.OK)
                    : Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Sorry, the book doesn't exist");
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc.Message);
            }
        }
    }
}