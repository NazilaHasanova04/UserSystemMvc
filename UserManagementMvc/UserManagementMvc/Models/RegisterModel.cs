using System.ComponentModel.DataAnnotations;

namespace UserManagementMvc.Models
{
    public class RegisterModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        public string Surname { get; set; }

        [Required]
        [MinLength(5)]
        public string UserName { get; set; }

        [Required]
        [MinLength(5)]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        [MinLength(7)]
        public string Password { get; set; }

        [Required]
        [MinLength(7)]
        [Compare("Password", ErrorMessage = Statics.StaticWords.PasswordNotMatched)]
        public string ConfirmPassword { get; set; }


    }
}
