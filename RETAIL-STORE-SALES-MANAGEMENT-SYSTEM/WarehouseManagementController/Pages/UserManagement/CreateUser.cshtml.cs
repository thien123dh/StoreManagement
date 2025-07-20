using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseManagementData.Models;
using WarehouseManagementData.Paging;
using WarehouseManagementRepository;
using WarehouseManagementService.Common;
using WarehouseManagementService.Dto.User;
using WarehouseManagementService.Dto.ValidateResult;

namespace WarehouseManagementController.Pages.UserManagement
{
    public class CreateUserModel : PageModel
    {
        private readonly UnitOfWork unitOfWork;
        
        public CreateUserModel(UnitOfWork unitOfWork)
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

        public string ErrorMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public List<SelectListItem> RoleList => RoleConstant.GetAllRoles().Select(
                r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id.ToString(),
                }
            ).ToList();
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CreateUpdateUserDto User { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.

        private CommonResult ValidateUserDto(CreateUpdateUserDto userRequest)
        {
            var checkExistUser = !unitOfWork.UserRepository.Search(
                u => u.Username == userRequest.Username || u.Email == userRequest.Email || u.ContactNumber == userRequest.ContactNumber).IsNullOrEmpty();

            if (checkExistUser)
            {
                return ResultHelper.BuildError("Tài khoản/Email/Contact có thể bị trùng");
            }

            return ResultHelper.BuildResult();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = ValidateUserDto(User);

            if (!result.IsSuccess)
            {
                ViewData["ErrorMessage"] = $"Error: {result.ErrorMessage}";
                return Page();
            }

            var saveUser = new User
            {
                Email = User.Email,
                ContactNumber = User.ContactNumber,
                DateOfBirth = User.DateOfBirth,
                Fullname = User.Fullname,
                Gender = User.Gender,
                Role = User.Role,
                Password = User.Password,
                Status = 1,
                Username = User.Username
            };
            var sequenceNumber = unitOfWork.UserRepository.Search(u => u.Role == User.Role).Count();
            saveUser.BusinessId = BusinessIdHelper.GenerateBusinessId(User.Role, sequenceNumber);
            saveUser.Status = CommonStatusConstant.ACTIVE;

            unitOfWork.UserRepository.Create(saveUser);

            return RedirectToPage("./SearchUser");
        }
    }
}
