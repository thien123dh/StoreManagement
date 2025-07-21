using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using WarehouseManagementData.Models;
using WarehouseManagementRepository;
using WarehouseManagementService.Dto.Request;
using WarehouseManagementService.StateMemory;

namespace WarehouseManagementController.Pages.ProductManagement
{
    public class CreateProductModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;

        public List<SelectListItem> Categories = default!;

        [BindProperty(SupportsGet = true)]
        public CreateProductRequest Product { set; get; } = default!;

        public CreateProductModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync().ConfigureAwait(false);

            Categories = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
            }).ToList();

            return Page();
        }

        private int? GetLoginUserId()
        {
            return HttpContext.Session.GetInt32("UserID");
        }


        public async Task<IActionResult> OnPostImportProductAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var product = new Product();
            var categories = _unitOfWork.CategoryRepository.GetAll();

            product.Manufactor = Product.Manufactor;
            product.SellingPrice = Product.SellingPrice;
            product.ImportPrice = Product.ImportPrice;
            product.Notes = Product.Notes;
            product.Status = Product.Status;
            product.Quantity = Product.Quantity;
            product.CategoryId = Product.CategoryId;
            product.Description = Product.Description;
            product.Name = Product.Name;
            product.ImageUrl = Product.ImageUrl;
            product.SellingPrice = Product.SellingPrice;
            product.UpdatedDateTime = DateTime.Now;
            product.CreatedDateTime = DateTime.Now;
            product.CreatedBy = GetLoginUserId();
            product.UpdatedBy = GetLoginUserId();
            product.Category = categories.FirstOrDefault(c => c.Id == product.CategoryId);

            StateMemory.ImportRequestProducts.Add(product);
            return RedirectToPage("./ImportRequest");
        }
    }
}
