using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WarehouseManagementData.Models;
using WarehouseManagementData.Paging;
using WarehouseManagementService.Implement;
using WarehouseManagementService.Interface;

namespace WarehouseManagementController.Pages.UserManagement
{
    public class SearchUserModel : PageModel
    {
        private IUserService _userService;

        public SearchUserModel(IUserService userService)
        {
            _userService = userService;
        }

        public string Message { get; set; } = default!;

        public Paginate<User> User { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;
        [BindProperty(SupportsGet = true)]
        public int Size { get; set; } = 10;

        [BindProperty]
        public Category Users { get; set; } = default!;
        public async Task OnGetAsync()
        {
            var result = await _userService.GetPagination("", PageIndex, Size);
            result.Items = result.Items ?? new List<User>();
            User = result;
        }
    }
}
