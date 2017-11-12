using System.ComponentModel.DataAnnotations;

namespace ez7zu6.Models.Account
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
