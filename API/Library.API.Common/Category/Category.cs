using System;
using System.ComponentModel.DataAnnotations;

namespace Library.API.Common.Category
{
    public class Category
    {
        public Int32 Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The Category Name should be from 6 to 100 symbols", MinimumLength = 6)]
        public String Name { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
