using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WarehouseManagementService.StateMemory;

namespace WarehouseManagementController.Pages
{
    public class LogoutPageModel : PageModel
    {
        public IActionResult OnGet()
        {
            HttpContext.Session.Clear();
            StateMemory.Clear();
            return RedirectToPage("./LoginPage");
        }
    }
}
