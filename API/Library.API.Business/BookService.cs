namespace Library.API.Business
{
    using System.Collections.Generic;
    using Library.API.Business.Interfaces;
    using Library.API.DataAccess.Interfaces;
    using Library.API.Model;

    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public Book CreateBook(Book book)
        {
            if (book == null)
            {
                return null;
            }

            Book createdBook = _bookRepository.Create(book);

            return createdBook.Id == 0 ? null : createdBook;
        }

        public Book GetBookById(int bookId)
        {
            if (bookId <= 0)
            {
                return null;
            }

            Book book = _bookRepository.GetById(bookId);

            return book.Id == 0 ? null : book;
        }

        public List<Book> GetAllBooks()
        {
            return _bookRepository.GetAll();
        }

        public Book UpdateBook(Book book)
        {
            return book == null ? null : _bookRepository.Update(book);
        }

        public List<Book> GetBooksByCategoryName(string categoryName)
        {
            string clearCatName = categoryName.Trim();

            int categoryId = _bookRepository.IfCategoryExists(clearCatName);

            return categoryId == 0 ? null : _bookRepository.GetBooksByCategoryId(categoryId);
        }

        public bool DeleteBook(int bookId)
        {
            return bookId > 0 && _bookRepository.Delete(bookId);
        }
    }
}