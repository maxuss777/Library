namespace Library.UI.Models.Account
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class LoginModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Email should be from 6 to 100 symbols", MinimumLength = 6)]
        public String Email { get; set; }

        [Required]
        [StringLength(6, ErrorMessage = "Password should be 6 symbols and consists only of integers", MinimumLength = 6)]
        public String Password { get; set; }
    }
}