using System.ComponentModel.DataAnnotations;
namespace UserManagementMvc.Models.Student
{
    public class CreateStudentDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(25, MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(25, MinimumLength = 10)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Age must be greater than 6")]
        [Range(6, 100)]
        public int Age { get; set; }
        public List<int> GroupIds { get; set; }
        [Required(ErrorMessage = "Image is required")]
        public IFormFile Image { get; set; }
    }
}
