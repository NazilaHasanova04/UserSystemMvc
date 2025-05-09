using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using UserManagementMvc.Entities;
using UserManagementMvc.Enums;
using UserManagementMvc.Models;
using UserManagementMvc.Models.CustomResponse;
using UserManagementMvc.Servicess.Abstraction;
using UserManagementMvc.Statics;

namespace UserManagementMvc.Servicess.Implementation
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMailService _mailService;
        private readonly SignInManager<User> _signInManager;
        private IConfiguration _configuration;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager,
            IConfiguration configuration, IMailService mailService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _mailService = mailService;
        }

        public async Task<ApiResponse<User>> RegisterAsync(RegisterModel model)
        {
            User user = new User
            {
                FirstName = model.Name,
                LastName = model.Surname,
                Email = model.Email,
                PhoneNumber = model.Phone,
                UserName = model.UserName,
                PasswordHash = model.Password
            };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return ApiResponse<User>.Failure(Statics.StaticWords.NotSucceded, System.Net.HttpStatusCode.BadRequest, errors);
            }

            await _userManager.AddToRoleAsync(user, Roles.User.ToString());

            string confirmToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            byte[] encodedToken = Encoding.UTF8.GetBytes(confirmToken);
            string token = WebEncoders.Base64UrlEncode(encodedToken);

            string confirmLink = EmailTemplate.BuildConfirmLink(_configuration, user.Id.ToString(), token);

            string body = EmailTemplate.GetConfirmEmailBody(confirmLink);

            await _mailService.SendMailAsync(model.Email, StaticWords.EmailConfirmation, body);

            return ApiResponse<User>.Success(System.Net.HttpStatusCode.OK);
        }

        public async Task<ApiResponse<User>> ConfirmRegisterAsync(string userId, string token)
        {
            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

            User user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return ApiResponse<User>.Failure(StaticWords.UsernameNotFound, System.Net.HttpStatusCode.NotFound);

            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);

            if (!result.Succeeded)
                return ApiResponse<User>.Failure(StaticWords.UsernameNotFound, System.Net.HttpStatusCode.NotFound,
                    result.Errors.Select(e => e.Description).ToList());

            return ApiResponse<User>.Success(System.Net.HttpStatusCode.OK);

        }

        public async Task<ApiResponse<User>> LoginAsync(LoginModel model)
        {
            await _signInManager.SignOutAsync();

            User? user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
                return ApiResponse<User>.Failure(StaticWords.UsernameNotFound, System.Net.HttpStatusCode.NotFound);

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);

            if (result.IsLockedOut)
                return ApiResponse<User>.Failure(StaticWords.UsernameLockedOut, System.Net.HttpStatusCode.NotFound);

            if (!result.Succeeded)
                return ApiResponse<User>.Failure(StaticWords.IncorrectLogin, System.Net.HttpStatusCode.NotFound);

            return ApiResponse<User>.Success(System.Net.HttpStatusCode.OK); ;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }


        public async Task<ApiResponse<User>> ForgotPasswordAsync(ForgotPasswordModel model)
        {
            User user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return ApiResponse<User>.Failure(StaticWords.IncorrectEmail, System.Net.HttpStatusCode.NotFound);

            string confirmToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            byte[] encodedToken = Encoding.UTF8.GetBytes(confirmToken);
            string token = WebEncoders.Base64UrlEncode(encodedToken);

            string confirmLink = EmailTemplate.BuildConfirmLink(_configuration, user.Id.ToString(), token);

            string body = EmailTemplate.GetConfirmEmailBodyForResetPassword(confirmLink);

            await _mailService.SendMailAsync(model.Email, StaticWords.ResetPassword, body);

            return ApiResponse<User>.Success(System.Net.HttpStatusCode.OK);
        }
        public async Task<ApiResponse<User>> ChangePasswordAsync(UpdatePasswordModel model)
        {
            byte[] bytes = WebEncoders.Base64UrlDecode(model.Token);
            var decodedToken = Encoding.UTF8.GetString(bytes);

            User user = await _userManager.FindByIdAsync(model.UserId);

            if (user == null)
                return ApiResponse<User>.Failure(StaticWords.UsernameNotFound, System.Net.HttpStatusCode.NotFound);

            var result = await _userManager.ResetPasswordAsync(user, decodedToken, model.Password);

            if (!result.Succeeded)
                return ApiResponse<User>.Failure(StaticWords.UsernameNotFound,
                    System.Net.HttpStatusCode.NotFound, result.Errors.Select(e => e.Description).ToList());

            return ApiResponse<User>.Success(System.Net.HttpStatusCode.OK);
        }

    }
}
