using UserManagementMvc.Models.File;


namespace UserManagementMvc.Models.Student
{
    public class StudentWithFileDto
    {
        public int Id { get; set; }
        public List<FileDetailsDto> FileInfo { get; set; }
    }
}
