using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using WarehouseManagementData.Models;
using WarehouseManagementRepository;
using WarehouseManagementService.Dto.Request;

namespace WarehouseManagementController.Pages.ProductManagement
{
    public class ImportRequestModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;

        [BindProperty(SupportsGet = true)]
        public CreateImportRequest ImportRequest { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public List<Product> Products { set; get; } = default!;

        public ImportRequestModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task InitDataAsync()
        {
            var importRequestJson = HttpContext.Session.GetString("ImportRequest");
            var importProductsJson = HttpContext.Session.GetString("ImportProducts");

            if (importRequestJson != null)
            {
                ImportRequest = JsonSerializer.Deserialize<CreateImportRequest>(importRequestJson) ?? default!;
            }
            if (importProductsJson != null)
            {
                Products = JsonSerializer.Deserialize<List<Product>>(importProductsJson) ?? default!;
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await InitDataAsync().ConfigureAwait(false);

            return Page();
        }

        public async Task<IActionResult> OnPostCreateProductAsync()
        {
            await InitDataAsync();
            var importJson = JsonSerializer.Serialize(ImportRequest);
            var productsJson = JsonSerializer.Serialize(Products);

            HttpContext.Session.SetString("ImportProducts", productsJson);
            HttpContext.Session.SetString("ImportRequest", importJson);

            return RedirectToPage("./CreateProduct");
        }
    }
}
