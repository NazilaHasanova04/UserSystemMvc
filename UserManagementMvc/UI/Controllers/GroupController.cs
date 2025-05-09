using Microsoft.AspNetCore.Mvc;
using UserManagementMvc.Models;
using UserManagementMvc.Models.Group;
using UserManagementMvc.Models.Pagination;
using UserManagementMvc.Servicess.Abstraction;

namespace UserManagementMvc.UI.Controllers;

public class GroupController : BaseController
{
    private readonly IGroupService _groupService;
    public GroupController(IGroupService groupService)
    {
        _groupService = groupService;
    }
    [HttpGet]
    public async Task<ActionResult<PaginationResponseDto<GroupDto>>> GetAll([FromQuery] PaginationDto paginationDto)
    {
        return await HandlePagedServiceActionAsync(() => _groupService.GetAllAsync(paginationDto));
    }

}
