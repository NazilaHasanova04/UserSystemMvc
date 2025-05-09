using Microsoft.AspNetCore.Mvc;
using UserManagementMvc.Models;
using UserManagementMvc.Models.Pagination;
using UserManagementMvc.Models.Student;
using UserManagementMvc.Servicess.Abstraction;

namespace UserManagementMvc.UI.Controllers;
public class StudentController : BaseController
{
    private readonly IStudentService _studentService;
    private readonly IFileService _fileService;
    public StudentController(IStudentService studentService, IFileService fileService)
    {
        _studentService = studentService;
        _fileService = fileService;
    }

    [HttpGet]
    public async Task<ActionResult<PaginationResponseDto<StudentDto>>> GetAll([FromQuery] PaginationDto paginationDto)
    {
        return await HandlePagedServiceActionAsync(() => _studentService.GetAllAsync(paginationDto));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StudentDto>> GetById(int id)
    {
        return await HandleServiceActionAsync(() => _studentService.GetByIdAsync(id));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StudentWithFileDto>> GetByIdWithFile(int id)
    {
        return await HandleServiceActionAsync(() => _studentService.GetStudentFileDetails(id));
    }

    [HttpPost]
    public async Task<ActionResult<bool>> Create(CreateStudentDto studentDto)
    {
        return await HandleServiceActionAsync(() => _studentService.CreateAsync(studentDto));
    }

    [HttpGet]
    public async Task<IActionResult> DownloadFile(int id)
    {
        var response = await _fileService.Download(id);

        if (!response.IsSuccess)
        {
            return NotFound(response.Message);
        }

        var (fileBytes, contentType, fileName) = response.Data;
        Convert.ToBase64String(fileBytes);

        return File(fileBytes, contentType, fileName);
    }



    [HttpPut]
    public async Task<ActionResult<bool>> Update(int id, UpdateStudentDto studentDto)
    {
        return await HandleServiceActionAsync(() => _studentService.UpdateAsync(id, studentDto));
    }

    [HttpPut]
    public async Task<ActionResult<bool>> UpdateGroup(int id, UpdateStudentGroupDto group)
    {
        return await HandleServiceActionAsync(() => _studentService.UpdateGroupsAsync(id, group));
    }


    [HttpDelete]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        return await HandleServiceActionAsync(() => _studentService.DeleteAsync(id));
    }


    [HttpGet]
    public async Task<ActionResult<PaginationResponseDto<StudentDto>>> GetStudentsByGroupId(int id, [FromQuery] PaginationDto paginationDto)
    {
        return await HandlePagedServiceActionAsync(() => _studentService.GetStudentsByGroupId(id, paginationDto));
    }


}