using Microsoft.AspNetCore.Mvc;
using UserManagementMvc.Models;
using UserManagementMvc.Servicess.Abstraction;
using UserManagementMvc.Statics;

namespace UserManagementMvc.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel user)
        {
            if (!ModelState.IsValid) return View(user);

            var result = await _userService.LoginAsync(user);

            if (!result.IsSuccess)
                return ReturnError(result.Message, user);

            return RedirectToAction("GetAllStudents", "Student");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var result = await _userService.ConfirmRegisterAsync(userId, token);

            if (result.IsSuccess == false)
                return ReturnErrors(result.Errors);

            return RedirectToAction("Login", "User");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel user)
        {
            if (!ModelState.IsValid)
                return View(user);

            var result = await _userService.RegisterAsync(user);

            if (!result.IsSuccess)
                return ReturnErrors(result.Errors);

            return RedirectWithMessage("User", "Login", StaticWords.RegisterConfirmation);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _userService.Logout();
            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            var response = await _userService.ForgotPasswordAsync(forgotPasswordModel);

            if (!response.IsSuccess)
                return ReturnError(response.Message, forgotPasswordModel);

            return RedirectWithMessage("User", "ForgotPassword", StaticWords.ResetPasswordConfirmation);
        }

        [HttpGet]
        public IActionResult UpdatePassword(string userId, string token)
        {
            return View(userId, token);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordModel updatePasswordModel)
        {
            var result = await _userService.ChangePasswordAsync(updatePasswordModel);

            if (!result.IsSuccess)
            {
                if (result.Errors.Count > 0)
                {
                    return ReturnErrors(result.Errors, updatePasswordModel);
                }
                else
                {
                    return ReturnError(result.Message, updatePasswordModel);
                }
            }

            return RedirectToAction("Login", "User");
        }

    }
}
