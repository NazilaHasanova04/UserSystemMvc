using UserManagementMvc.Entities.Base;


namespace UserManagementMvc.Entities;

public class Group : BaseEntity
{
    public string Name { get; set; }

    public int CourseId { get; set; }
    public virtual Course Course { get; set; }

    public int SubjectId { get; set; }
    public virtual Subject Subject { get; set; }

    public int? TeacherId { get; set; }
    public virtual Teacher Teacher { get; set; }

    public virtual ICollection<StudentGroup> StudentGroups { get; set; }
    public virtual ICollection<Lesson> Lessons { get; set; }
}
