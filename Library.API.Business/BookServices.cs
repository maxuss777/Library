using System.Collections.Generic;
using Library.API.Common.Book;
using Library.API.Common.Category;
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
        public BookObject CreateBook(BookObject book)
        {
            if (book == null)
            {
                return null;
            }
            var createdBook = _bookRepository.Create(book);
            return createdBook.Id == 0 ? null : createdBook;
        }
        public BookObject GetBookById(int bookId)
        {
            if (bookId <= 0)
            {
                return null;
            }
            var book = _bookRepository.Get(bookId);
            if (book.Id <= 0)
            {
                return null;
            }
            book.Categories = _bookRepository.GetBooksCategories(bookId);
            return book;
        }
        public IEnumerable<BookObject> GetAllBooks()
        {
            var books = _bookRepository.GetAll();
            if (books.Equals(default(List<BookObject>)))
            {
                return null;
            }
            foreach (BookObject b in books)
            {
                b.Categories = _bookRepository.GetBooksCategories(b.Id);
            }
            return books;
        }
        public BookObject UpdateBook(BookObject book)
        {
            return book == null ? null : _bookRepository.Update(book);
        }
        public IEnumerable<CategoryObject> GetBooksCategories(int bookId)
        {
            if (bookId <= 0)
            {
                return null;
            }
            var booksCategpries = _bookRepository.GetBooksCategories(bookId);
            return booksCategpries.Equals(default(List<CategoryObject>)) 
                ? null 
                : booksCategpries;
        }
        public bool DeleteBook(int bookId)
        {
            return bookId > 0 && _bookRepository.Delete(bookId);
        }
        public bool PutBookToCategory(int bookId)
        {
            return bookId > 0 && _bookRepository.PutBookToCategory(bookId);
        }
    }
}