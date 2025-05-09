namespace UserManagementMvc.Entities.Base
{
    public interface ICreateEntity : IEntity
    {
        int CreateUserId { get; set; }
        DateTime CreatedDate { get; set; }
    }
}
