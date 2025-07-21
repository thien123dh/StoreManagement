using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WarehouseManagementData.Models;
using WarehouseManagementRepository.Interface;
using WarehouseManagementService.Common;
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

        public IActionResult OnGet()
        {
            var role = HttpContext.Session.GetInt32("RoleId");
            if (role == null)
            {
                return Page();
            }

            return RedirectByRole(role ?? 0);
        }

        private IActionResult RedirectByRole(int role)
        {
            if (role == RoleConstant.ADMIN)
                return RedirectToPage("./UserManagement/SearchUser");

            return RedirectToPage("./ProductManagement/SearchProduct");
        }

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
                        HttpContext.Session.SetInt32("RoleId", check.Role);

                        return RedirectByRole(check.Role);
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
