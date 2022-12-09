using System.ComponentModel.DataAnnotations;

namespace ReviewsPortal.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Enter your Email, please!")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Enter your password, please!")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Wrong password!")]
        public string ConfirmPassword { get; set; }
    }
}
