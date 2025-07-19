using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WarehouseManagementData.Models;
using WarehouseManagementRepository.Interface;
using WarehouseManagementService.Interface;

namespace WarehouseManagementController.Pages
{
    public class LoginPageModel : PageModel
    {
        private readonly ILoginService _userService;
        public LoginPageModel(ILoginService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public User user { get; set; } = default!;
        public string ErrorMessage { get; private set; }

        public IActionResult OnPost()
        {

            if (!string.IsNullOrWhiteSpace(user.Username) && !string.IsNullOrWhiteSpace(user.Password))
            {
                try
                {
                    var check = _userService.checkLogin(user.Username, user.Password);
                    if (check != null)
                    {
                        HttpContext.Session.SetInt32("UserID", check.Id);
                        return RedirectToPage("Index");
                    }
                }
                catch
                {
                    ErrorMessage = "Incorect User Name or Password Please Try Again";
                    return Page();
                }
            }
            return Page();
        }
    }
}
