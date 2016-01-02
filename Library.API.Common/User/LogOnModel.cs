using System.ComponentModel.DataAnnotations;

namespace Library.API.Common.User
{
    public class LogOnModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}