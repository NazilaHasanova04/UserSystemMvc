using UserManagementMvc.Entities.Base;


namespace UserManagementMvc.Entities;

public class Lesson : BaseEntity
{
    public DateTime Date { get; set; }
    public TimeSpan Duration { get; set; }

    public int GroupId { get; set; }
    public virtual Group Group { get; set; }

    public virtual ICollection<StudentLesson> StudentLessons { get; set; }
}
