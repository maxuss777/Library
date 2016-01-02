using System.Collections.Generic;
using Library.API.Business.Abstract;
using Library.API.Common.Book;
using Library.API.Common.Category;
using Library.API.DAL.Abstract;

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
            return book.Id == 0 ? null : book;
        }
        public IEnumerable<BookObject> GetAllBooks()
        {
            var books = _bookRepository.GetAll();
            return books.Equals(default(List<BookObject>)) 
                ? null 
                : books;
        }
        public BookObject UpdateBook(BookObject book)
        {
            if (book == null)
            {
                return null;
            }
            var updatedBook = _bookRepository.Update(book);
            return updatedBook;
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