namespace Library.UI.Models.Categories
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class Category : BaseJsonObject
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The Category Name should be from 6 to 100 symbols", MinimumLength = 6)]
        public string Name { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime CreationDate { get; set; }
    }
}