

using UserManagementMvc.Entities.Base;

namespace UserManagementMvc.Entities
{
    public class FileDetails : BaseEntity
    {
        public string Name { get; set; }
        public string FileType { get; set; }
        public string Path { get; set; }

        public virtual ICollection<StudentFile> StudentFiles { get; set; }
    }
}
