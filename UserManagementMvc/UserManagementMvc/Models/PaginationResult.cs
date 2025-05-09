using UserManagementMvc.Entities.Base;


namespace UserManagementMvc.Models
{
    public class PaginationResult<T> where T : class, IEntity
    {
        public int TotalCount { get; set; }
        public IEnumerable<T> Data { get; set; }
        public PaginationResult(int count, IEnumerable<T> data)
        {
            Data = data;
            TotalCount = count;
        }
    }
}
