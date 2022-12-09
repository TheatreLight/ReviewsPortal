using System.ComponentModel.DataAnnotations;

namespace ReviewsPortal.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Enter your Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
