namespace UserManagementMvc.Models.Group

{
    public class CreateGroupDto
    {
        public string Name { get; set; }
        public int CourseId { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
    }
}
