namespace UserManagementMvc.Entities.Base
{
    public interface IDeleteEntity : IUpdateEntity
    {
        int DeleteUserId { get; set; }
        bool IsDeleted { get; set; }
        DateTime DeletedDate { get; set; }
    }
}
