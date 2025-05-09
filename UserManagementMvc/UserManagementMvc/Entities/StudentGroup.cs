using UserManagementMvc.Entities.Base;


namespace UserManagementMvc.Entities;
public class StudentGroup : BaseEntity
{
    public int StudentId { get; set; }
    public virtual Student Student { get; set; }

    public int GroupId { get; set; }
    public virtual Group Group { get; set; }
}
