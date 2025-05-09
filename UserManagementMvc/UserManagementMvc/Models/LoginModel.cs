using System.ComponentModel.DataAnnotations;

namespace UserManagementMvc.Models
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required(ErrorMessage = "You must click Remember Me to continue.")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "You must click Remember Me to continue.")]
        public bool RememberMe { get; set; }
    }
}
