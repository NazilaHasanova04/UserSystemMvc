namespace UserManagementMvc.Entities.Base
{
    public abstract class BaseEntity : IDeleteEntity
    {
        public bool IsDeleted { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Id { get; set; }
        public int DeleteUserId { get; set; }
        public DateTime DeletedDate { get; set; }
        public int UpdateUserId { get; set; }
        public int CreateUserId { get; set; }
    }
}
