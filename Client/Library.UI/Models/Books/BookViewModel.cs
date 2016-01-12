namespace Library.UI.Models.Books
{
    using System.Collections.Generic;

    public class BookViewModel
    {
        public List<Book> Books { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}