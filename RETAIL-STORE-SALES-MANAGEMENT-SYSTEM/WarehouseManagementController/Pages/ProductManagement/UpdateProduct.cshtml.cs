using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WarehouseManagementData.Models;
using WarehouseManagementRepository;
using WarehouseManagementService.Dto.Request;
using WarehouseManagementService.Dto.ValidateResult;

namespace WarehouseManagementController.Pages.ProductManagement
{
    public class UpdateProductModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public Product DbProduct { set; get; } = default!;

        public List<SelectListItem> Categories = default!;

        [BindProperty(SupportsGet = true)]
        public UpdateProductRequest Product { set; get; } = default!;

        public UpdateProductModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Product?> GetProductByIdAsync()
        {
            var product = await _unitOfWork.ProductRepository.Search(p => p.Id == Id)
                .Include(p => p.Category)
                .Include(p => p.CreatedByNavigation)
                .FirstOrDefaultAsync();

            return product;
        }

        public async Task ProcessInitProduct()
        {
            var product = await GetProductByIdAsync();

            DbProduct = product;

            Product = new UpdateProductRequest
            {
                CategoryId = product.CategoryId,
                Description = product.Description,
                Name = product.Name,
                ImageUrl = product.ImageUrl,
                ImportPrice = product.ImportPrice,
                Manufactor = product.Manufactor,
                Notes = product.Notes,
                SellingPrice = product.SellingPrice ?? 0,
                Status = product.Status,
            };
        }

        public async Task<IActionResult> OnPostUpdateAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var product = _unitOfWork.ProductRepository.GetById(Id);

            product.Manufactor = Product.Manufactor;
            product.SellingPrice = Product.SellingPrice;
            product.Status = Product.Status;
            product.CategoryId = Product.CategoryId;
            product.Description = Product.Description;
            product.Name = Product.Name;
            product.ImageUrl = Product.ImageUrl;
            product.SellingPrice = Product.SellingPrice;
            product.UpdatedDateTime = DateTime.Now;

            _unitOfWork.ProductRepository.Update(product);

            return RedirectToPage("./SearchProduct");
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var categories = _unitOfWork.CategoryRepository.GetAll();

            Categories = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
            }).ToList();

            await ProcessInitProduct();

            if (DbProduct == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
