using System;
using System.Collections.Generic;
using Library.API.Common.Category;

namespace Library.API.Common.Book
{
    public class BookObject
    {
        public Int32 Id { get; set; }

        public String Name { get; set; }

        public Int64 ISBN { get; set; }

        public String Author { get; set; }

       public IEnumerable<CategoryObject> Categories { get; set; }
    }
}