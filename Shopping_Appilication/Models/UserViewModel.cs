using System.ComponentModel.DataAnnotations;

namespace Shopping_Appilication.Models
{
    public class UserViewModel
    {
        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        public bool AgreeToTerms { get; set; }
    }
}
