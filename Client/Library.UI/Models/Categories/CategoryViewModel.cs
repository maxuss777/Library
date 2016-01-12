namespace Library.UI.Models.Categories
{
    using System.Collections.Generic;

    public class CategoryViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        
        public PagingInfo PagingInfo { get; set; }
    }
}