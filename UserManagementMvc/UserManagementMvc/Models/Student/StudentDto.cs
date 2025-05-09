using UserManagementMvc.Models.General;

namespace UserManagementMvc.Models.Student
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public List<NameIdDto<int>> Groups { get; set; }
    }
}
