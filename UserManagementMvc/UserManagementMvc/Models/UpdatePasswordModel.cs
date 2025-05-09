using System.ComponentModel.DataAnnotations;

namespace UserManagementMvc.Models
{
    public class UpdatePasswordModel
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        [MinLength(6)]
        public string Password { get; set; }
        [MinLength(6)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
