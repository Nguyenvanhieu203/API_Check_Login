using BookManager.Data;
using System.ComponentModel.DataAnnotations;

namespace BookManager.Models
{
    public class SignInModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }

    public class Custommessage 
    {
        public string status { get; set; }
        public string token { get; set; }
        public string Id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
    }
}
