using System;
using System.ComponentModel.DataAnnotations;

namespace Library.API.Common.Book
{
    public class BookInfo
    {
        public Int32 Id { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public Int64 ISBN { get; set; }

        [Required]
        public String Author { get; set; }
    }
}
