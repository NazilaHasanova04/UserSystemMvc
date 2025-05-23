﻿
namespace UserManagementMvc.Models
{
    public class PaginationResponseDto<T>
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
