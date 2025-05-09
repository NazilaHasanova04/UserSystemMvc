using UserManagementMvc.Entities.Base;


namespace UserManagementMvc.Entities
{
    public class StudentFile : BaseEntity
    {
        public int FileId { get; set; }
        public virtual FileDetails FileDetails { get; set; }

        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
    }
}
