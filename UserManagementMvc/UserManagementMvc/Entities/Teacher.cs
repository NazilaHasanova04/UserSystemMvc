using UserManagementMvc.Entities.Base;


namespace UserManagementMvc.Entities
{
    public class Teacher : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
    }
}
