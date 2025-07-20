using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using WarehouseManagementData.Models;
using WarehouseManagementRepository;
using WarehouseManagementService.Dto.Request;

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
            var categories = _unitOfWork.CategoryRepository.GetAll();

            Categories = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
            }).ToList();

            return Page();
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
            product.Status = Product.Status;
            product.CategoryId = Product.CategoryId;
            product.Description = Product.Description;
            product.Name = Product.Name;
            product.ImageUrl = Product.ImageUrl;
            product.SellingPrice = Product.SellingPrice;
            product.UpdatedDateTime = DateTime.Now;
            product.CreatedDateTime = DateTime.Now;
            product.Category = categories.FirstOrDefault(c => c.Id == product.CategoryId);

            var importProductsJson = HttpContext.Session.GetString("ImportProducts");
            List<Product> products = new List<Product>();
            if (importProductsJson != null)
            {
                products = JsonSerializer.Deserialize<List<Product>>(importProductsJson) ?? default!;
            }

            products.Add(product);
            HttpContext.Session.SetString("ImportProducts", JsonSerializer.Serialize(products));
            return RedirectToPage("./ImportRequest");
        }
    }
}
