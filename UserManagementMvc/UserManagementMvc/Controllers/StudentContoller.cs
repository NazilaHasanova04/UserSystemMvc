using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementMvc.Models;
using UserManagementMvc.Models.Group;
using UserManagementMvc.Models.Pagination;
using UserManagementMvc.Models.Student;
using UserManagementMvc.Servicess.Abstraction;

namespace UserManagementMvc.UI.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IGroupService _groupService;

        public StudentController(IStudentService studentService, IGroupService groupService)
        {
            _studentService = studentService;
            _groupService = groupService;
        }

        [Authorize(Roles = "User,Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllStudents(int page = 1)
        {
            var response = await _studentService.GetAllMvcAsync(new PaginationDto()
            {
                PageNumber = page,
                PageSize = 6,
            });

            if (!response.IsSuccess)
            {
                return View();
            }
            else
            {
                return View(response);
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CreateStudentDto student = new CreateStudentDto();

            return View(student);

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateStudentDto student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }
            var response = await _studentService.CreateMvcAsync(student);

            if (!response.IsSuccess)
            {
                ModelState.AddModelError("Email", response.Message);
                return View(student);
            }

            return RedirectToAction("GetAllStudents", "Student");
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _studentService.GetByIdAsync(id);
            if (!response.IsSuccess)
            {
                return RedirectToAction("GetAllStudents");
            }

            var studentModel = new UpdateStudentModel
            {
                Id = response.Data.Id,
                Name = response.Data.Name,
                Email = response.Data.Email,
                Phone = response.Data.Phone,
                Age = response.Data.Age
            };

            return View(studentModel);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateStudentModel studentModel)
        {
            UpdateStudentDto studentDto = new UpdateStudentDto
            {
                Name = studentModel.Name,
                Email = studentModel.Email,
                Age = studentModel.Age,
                Phone = studentModel.Phone
            };

            await _studentService.UpdateMvcAsync(studentModel.Id, studentDto);
            return RedirectToAction("GetAllStudents", "Student");
        }



        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _studentService.DeleteMvcAsync(id);
            return RedirectToAction("GetAllStudents", "Student");
        }

        public async Task<List<GroupDto>> GetGroups()
        {
            var response = await _groupService.GetAllAsync(new PaginationDto()
            {
                PageNumber = 1,
                PageSize = 10,
            });

            return response.Data.Items.ToList();
        }

    }
}
