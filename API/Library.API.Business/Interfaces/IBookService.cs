namespace Library.API.Business.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Library.API.Model;

    public interface IBookService
    {
        Book CreateBook(Book book);
        
        Book GetBookById(int bookId);
        
        List<Book> GetAllBooks();
        
        Book UpdateBook(Book book);
        
        List<Book> GetBooksByCategoryName(string categoryName);
        
        Boolean DeleteBook(int bookId);
    }
}