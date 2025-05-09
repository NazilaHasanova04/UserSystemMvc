using UserManagementMvc.Models.General;

namespace UserManagementMvc.Models.Group
{
    public class GroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public NameIdDto<int> Course { get; set; }
        public NameIdDto<int> Subject { get; set; }
        public NameIdDto<int> Teacher { get; set; }
    }
}
