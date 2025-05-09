
using UserManagementMvc.Entities.Base;


namespace UserManagementMvc.Entities
{
    public class Student : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public virtual ICollection<StudentGroup> StudentGroups { get; set; } = new List<StudentGroup>();
        public virtual ICollection<StudentLesson> StudentLessons { get; set; }
        public virtual ICollection<StudentFile> StudentFiles { get; set; } = new List<StudentFile>();
    }
}
