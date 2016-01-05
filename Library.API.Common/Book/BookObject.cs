﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Library.API.Common.Category;

namespace Library.API.Common.Book
{
    public class BookObject
    {
        [Required]
        public Int32 Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The Book Name should be from 6 to 100 symbols", MinimumLength = 6)]
        public String Name { get; set; }

        [Required]
        [StringLength(13, ErrorMessage = "The ISBN length should be 13 symbols and consists only of integers", MinimumLength = 13)]
        public Int64 ISBN { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The Aothor field should be from 6 to 100 symbols", MinimumLength = 6)]
        public String Author { get; set; }

        public IEnumerable<CategoryInfo> Categories { get; set; }
    }
}