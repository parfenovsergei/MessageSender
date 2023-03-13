using System.ComponentModel.DataAnnotations;

namespace MessageSenderAPI.Domain.Request
{
    public class ResetPasswordRequest
    {
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Min size of password is 6 symbols")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm passwrod is required")]
        [Compare("Password", ErrorMessage = "Passwords are mismatch")]
        public string ConfirmPassword { get; set; }
    }
}
