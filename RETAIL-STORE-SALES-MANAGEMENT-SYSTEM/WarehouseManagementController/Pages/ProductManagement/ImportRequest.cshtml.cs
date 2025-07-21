using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;
using WarehouseManagementData.Models;
using WarehouseManagementRepository;
using WarehouseManagementService.Dto.Request;
using WarehouseManagementService.StateMemory;

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
            ImportRequest = StateMemory.ImportRequest;
            Products = StateMemory.ImportRequestProducts;
        }

        private int? GetLoginUserId()
        {
            return HttpContext.Session.GetInt32("UserID");
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await InitDataAsync().ConfigureAwait(false);

            return Page();
        }

        private string GenerateImportRequestSerialNumber()
        {
            var sequenceNumber = _unitOfWork.ImportRequestRepository.Search(i => (i.CreatedDateTime != null) && i.CreatedDateTime.Value.Date == DateTime.Now.Date).Count();

            return $"IM{DateTime.Now.ToString("yyMMdd")}-{sequenceNumber.ToString("000000")}";
        }

        private List<Product> SetDataGetImportProducts()
        {
            var sequenceNumber = _unitOfWork.ProductRepository.Search(p => p.CreatedDateTime != null && p.CreatedDateTime.Value.Date == DateTime.Now.Date).Count();
            var importProducts = StateMemory.ImportRequestProducts;

            foreach (var product in importProducts)
            {
                product.SerialNumber = $"P{DateTime.Now.ToString("yyMMdd")}-{sequenceNumber++.ToString("00000")}";
                product.Category = null;
            }

            return importProducts;
        }

        /// <summary>
        /// Action submit import request
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostSubmitImportRequestAsync()
        {
            var importProducts = SetDataGetImportProducts();
            if (importProducts.IsNullOrEmpty())
            {
                    ViewData["ErrorMessage"] = $"Lỗi: Không có sản phẩm nào được thêm vào";
                    return Page();
            }

            var importRequest = new ImportRequest
            {
                CreatedBy = GetLoginUserId(),
                CreatedDateTime = DateTime.Now,
                Description = ImportRequest.Description,
                Status = 1,
                ImportedSerialNumber = GenerateImportRequestSerialNumber(),
            };

            _unitOfWork.ImportRequestRepository.Create(importRequest);

            await _unitOfWork.ProductRepository.CreateManyAsync(importProducts).ConfigureAwait(false);

            var importDetails = importProducts.Select(p =>
            {
                return new ImportRequestDetail
                {
                    ImportPrice = p.ImportPrice,
                    ImportRequestId = importRequest.Id,
                    ProductId = p.Id,
                    Quantity = p.Quantity,
                };
            }).ToList();    

            await _unitOfWork.ImportRequestDetailRepository.CreateManyAsync(importDetails).ConfigureAwait(false);

            return RedirectToPage("./SearchProduct");
        }

        public IActionResult OnPostRemoveImportProduct(int index)
        {
            StateMemory.ImportRequestProducts.RemoveAt(index);

            return RedirectToPage("./ImportRequest");
        }

        public async Task<IActionResult> OnPostCreateProductAsync()
        {
            StateMemory.ImportRequest = ImportRequest;

            return RedirectToPage("./CreateProduct");
        }
    }
}
