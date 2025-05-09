namespace UserManagementMvc.Entities.Base
{
    public interface IUpdateEntity : ICreateEntity
    {
        int UpdateUserId { get; set; }
        DateTime UpdatedDate { get; set; }
    }
}
