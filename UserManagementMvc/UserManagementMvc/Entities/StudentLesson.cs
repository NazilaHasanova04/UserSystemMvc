using UserManagementMvc.Entities.Base;


namespace UserManagementMvc.Entities
{
    public class StudentLesson : BaseEntity
    {
        public byte Mark { get; set; }

        public int StudentId { get; set; }
        public virtual Student Student { get; set; }


        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }
    }
}
