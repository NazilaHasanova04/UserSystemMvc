using Microsoft.AspNetCore.Mvc;

namespace UserManagementMvc.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult ReturnError(string message, object model = null)
        {
            ModelState.AddModelError(string.Empty, message);
            return View(model);
        }

        public IActionResult ReturnErrors(List<string> errors, object model = null)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
            return View(model);
        }

        public IActionResult RedirectWithMessage(string controller, string action, string message)
        {
            TempData["Message"] = message;
            return RedirectToAction(action, controller);
        }
    }
}
