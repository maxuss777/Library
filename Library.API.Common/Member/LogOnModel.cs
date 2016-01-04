using System.ComponentModel.DataAnnotations;

namespace Library.API.Common.Member
{
    public class LogOnModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}