using System.ComponentModel.DataAnnotations;

namespace Library.API.Common.Member
{
    public class RegisterModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string MemberName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The Password should be from 6 to 100 symbols", MinimumLength = 6)]
        public string Password { get; set; }
    }
}