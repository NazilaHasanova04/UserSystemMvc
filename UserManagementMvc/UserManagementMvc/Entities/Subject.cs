using UserManagementMvc.Entities.Base;


namespace UserManagementMvc.Entities;
public class Subject : BaseEntity
{
    public string Name { get; set; }

    public virtual ICollection<Group> Groups { get; set; }
}
