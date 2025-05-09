using Microsoft.AspNetCore.Mvc;
using UserManagementMvc.Models;
using UserManagementMvc.Models.CustomResponse;

namespace UserManagementMvc.UI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected async Task<ActionResult<T>> HandleServiceActionAsync<T>(Func<Task<ApiResponse<T>>> serviceAction)
        {
            var result = await serviceAction();
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);

            // return StatusCode(response.StausCOde,response)
        }
        protected async Task<ActionResult<PaginationResponseDto<T>>> HandlePagedServiceActionAsync<T>(Func<Task<ApiResponse<PaginationResponseDto<T>>>> serviceAction)
        {
            var result = await serviceAction();
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
