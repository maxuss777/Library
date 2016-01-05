using System.Collections.Generic;

namespace Library.UI.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreationDate { get; set; }
        public List<BookInfo> Books { get; set; } 
    }
}