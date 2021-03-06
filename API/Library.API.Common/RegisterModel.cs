﻿namespace Library.API.Model
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "The Email should be from 6 to 100 symbols", MinimumLength = 6)]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The Meember Name should be from 6 to 100 symbols", MinimumLength = 6)]
        public string MemberName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The Password should be from 6 to 100 symbols", MinimumLength = 6)]
        public string Password { get; set; }
    }
}