using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using WarehouseManagementData.Models;
using WarehouseManagementRepository;
using WarehouseManagementService.Common;
using WarehouseManagementService.Dto.User;
using WarehouseManagementService.Dto.ValidateResult;

namespace WarehouseManagementController.Pages.UserManagement
{
    public class UpdateUserModel : PageModel
    {
        private readonly UnitOfWork unitOfWork;

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        [BindProperty]
        public UserDetailDto User { get; set; } = default!;
        public UpdateUserModel(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [BindProperty(SupportsGet = true)]
        public List<SelectListItem> GenderList => GenderHelper.GenderList.Select(
            g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.Name
            }
        ).ToList();

        [BindProperty(SupportsGet = true)]
        public List<SelectListItem> RoleList => RoleConstant.GetAllRoles().Select(
                r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id.ToString(),
                }
            ).ToList();
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var user = unitOfWork.UserRepository.Get(u => u.Id == id);

            if (user == null)
                return NotFound();

            User = new UserDetailDto
            {
                Id = id,
                BusinessId = user.BusinessId,
                ContactNumber = user.ContactNumber,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                Fullname = user.Fullname,
                Gender = user.Gender,
                Role = user.Role,
                Username = user.Username,
                Status = user.Status
            };

            return Page();
        }
        private CommonResult ValidateUser(User userRequest)
        {
            var checkExistUser = !unitOfWork.UserRepository.Search(u => u.Id != Id && (u.Email == userRequest.Email || u.ContactNumber == userRequest.ContactNumber)).IsNullOrEmpty();

            if (checkExistUser)
            {
                return ResultHelper.BuildError("Email/Số điện thoại có thể bị trùng");
            }

            return ResultHelper.BuildResult();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var dbUser = unitOfWork.UserRepository.Get(u => u.Id == this.Id);

            dbUser.DateOfBirth = User.DateOfBirth;
            dbUser.Status = User.Status;
            dbUser.Gender = User.Gender;
            dbUser.ContactNumber = User.ContactNumber;
            dbUser.Email = User.Email;

            var result = ValidateUser(dbUser);

            if (!result.IsSuccess)
            {
                ViewData["ErrorMessage"] = $"Error: {result.ErrorMessage}";
                return Page();
            }

            unitOfWork.UserRepository.Update(dbUser);

            return RedirectToPage("./SearchUser");
        }
    }
}
