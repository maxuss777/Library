namespace Library.UI.Models.Books
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Book : BaseJsonObject
    {
        [Required]
        public Int32 Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The Book Name should be from 6 to 100 symbols", MinimumLength = 6)]
        public String Name { get; set; }

        [Required]
        [StringLength(13, ErrorMessage = "The ISBN length should be from 10 to 13", MinimumLength = 13)]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Wrong format ISBN, should be like: '978-1-4302-1998-9'")]
        public String ISBN { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The Author field should be from 6 to 100 symbols", MinimumLength = 6)]
        public String Author { get; set; }
    }
}