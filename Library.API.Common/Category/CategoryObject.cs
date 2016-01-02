using System;
using System.Collections.Generic;
using Library.API.Common.Book;

namespace Library.API.Common.Category
{
    public class CategoryObject
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public DateTime CreationDate { get; set; }
        public IEnumerable<BookObject> Books { get; set; }
    }
}
