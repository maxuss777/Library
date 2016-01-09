using System.Collections.Generic;

namespace Library.UI.Models
{
    public class CategoryViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}