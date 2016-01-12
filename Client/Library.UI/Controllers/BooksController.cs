namespace Library.UI.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Library.UI.Abstract;
    using Library.UI.Filters;
    using Library.UI.Models;
    using Library.UI.Models.Books;

    [MyAuthorize]
    public class BooksController : Controller
    {
        private const int PageSize = 5;
        private readonly IBookService _booksService;

        public BooksController(IBookService bookService)
        {
            _booksService = bookService;
        }

        [HttpGet]
        public ActionResult GetAll(int page = 1)
        {
            string ticket = Request.Cookies["_auth"].Value;

            List<Book> books = _booksService.GetAll(ticket);

            if (books.Count == 0)
            {
                return PartialView("EmptyAllBooksList");
            }

            BookViewModel bookViewModel = new BookViewModel
            {
                Books = books
                    .OrderByDescending(b => b.Id)
                    .Skip((page - 1)*PageSize)
                    .Take(PageSize)
                    .ToList(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _booksService.GetAll(ticket).Count()
                }
            };

            return PartialView("FilledAllBooksList", bookViewModel);
        }

        [HttpGet]
        public ActionResult GetFiltered(string category, int page = 1)
        {
            BookViewModel bookViewModel = new BookViewModel();
            string ticket = Request.Cookies["_auth"].Value;
            
            List<Book> books = _booksService.GetByCategory(category, ticket);
            List<Book> allBooks = _booksService.GetAll(ticket);
            
            ViewBag.Books = _booksService.BooksAsListItems(allBooks);
            ViewBag.CurrentCategory = category;

            try
            {
                if (books.Count == 0)
                {
                    return PartialView("EmptyFilteredBookList");
                }

                bookViewModel.Books = books
                    .OrderByDescending(b => b.Id)
                    .Skip((page - 1)*PageSize)
                    .Take(PageSize)
                    .ToList();

                bookViewModel.PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null
                        ? allBooks.Count()
                        : _booksService.GetByCategory(category, ticket).Count()
                };


                return bookViewModel.Books.Count == 0
                    ? PartialView("EmptyFilteredBookList")
                    : PartialView("FilledFilteredBooksList", bookViewModel);
            }
            catch
            {
                return PartialView("EmptyFilteredBookList");
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return PartialView("CreateBook");
        }

        [HttpPost]
        public ActionResult Create(Book book)
        {
            if (!ModelState.IsValid || book == null)
            {
                return PartialView("CreateBook");
            }

            string ticket = Request.Cookies["_auth"].Value; 
            
            bool isCreated = _booksService.Create(book, ticket);
            if (isCreated)
            {
                TempData["succ_message"] = "The Book has been created successfully!";
            }
            else
            {
                TempData["fail_message"] = "The Book has been created unsuccessfully!";
            }

            return RedirectToAction("GetAll", "Books");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            string ticket = Request.Cookies["_auth"].Value;

            bool isDeleted = _booksService.Delete(id, ticket);
            
            if (id == 0 || !isDeleted)
            {
                TempData["fail_message"] = "The Book has been deleted unsuccessfully!";
            }
            else
            {
                TempData["succ_message"] = "The Book has been deleted successfully!";
            }

            return RedirectToAction("GetAll", "Books");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            string ticket = Request.Cookies["_auth"].Value;
            
            Book book = _booksService.GetById(id, ticket);
            if (book != null)
            {
                return PartialView("EditBook", book);
            }
            
            TempData["fail_message"] = "The Book has not been found!";
            
            return RedirectToAction("GetAll", "Books");
        }

        [HttpPost]
        public ActionResult Edit(Book book)
        {
            if (!ModelState.IsValid || book == null)
            {
                return PartialView("EditBook", book);
            }

            string ticket = Request.Cookies["_auth"].Value;

            bool isUpdated = _booksService.Update(book, ticket);
            if (isUpdated)
            {
                TempData["succ_message"] = "The Book has been edited successfully!";
            }
            else
            {
                TempData["fail_message"] = "The Book has been edited unsuccessfully!";
            }

            return RedirectToAction("GetAll", "Books");
        }

        public IEnumerable<Book> FilterByCategory(string category)
        {
            string ticket = Request.Cookies["_auth"].Value;
            
            return _booksService.GetByCategory(category, ticket);
        }
    }
}