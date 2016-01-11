using System.Collections.Generic;
using System.Linq;
using Library.API.Common.BooksObjects;
using Library.API.DAL.Abstract;
using Library.API.Business.Abstract;

namespace Library.API.Business
{
    public class BookServices : IBookServices
    {
        private readonly IBookRepository _bookRepository;

        public BookServices(IBookRepository bookRepo)
        {
            _bookRepository = bookRepo;
        }

        public Book CreateBook(Book book)
        {
            if (book == null)
            {
                return null;
            }
            var createdBook = _bookRepository.Create(book);
            return createdBook.Id == 0 ? null : createdBook;
        }
        public Book GetBookById(int bookId)
        {
            if (bookId <= 0)
            {
                return null;
            }
            var book = _bookRepository.Get(bookId);
            return book.Id <= 0 ? null : book;
        }
        public IEnumerable<Book> GetAllBooks()
        {
            var books = _bookRepository.GetAll();
            return books.Any() ? books : null;
        }
        public Book UpdateBook(Book book)
        {
            return book == null ? null : _bookRepository.Update(book);
        }
        public IEnumerable<Book> GetBooksByCategoryName(string categoryName)
        {
            var clearCatName = categoryName.Trim();
            var categoryId = _bookRepository.IfCategoryExist(clearCatName);
            if(categoryId == 0) return null;
            var books = _bookRepository.GetBooksByCategoryId(categoryId);
            return !books.Any() ? null : books;
        }
        public bool DeleteBook(int bookId)
        {
            return bookId > 0 && _bookRepository.Delete(bookId);
        }
    }
}