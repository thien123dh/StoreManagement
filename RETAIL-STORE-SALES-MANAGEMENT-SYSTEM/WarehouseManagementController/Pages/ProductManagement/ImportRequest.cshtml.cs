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
        [BindProperty(SupportsGet = true)]
        public string? SelectedId { set; get; } = "";

        [BindProperty(SupportsGet = true)]
        public List<SelectListItem> ProductSerialNumbers { set; get; } = default!;

        public ImportRequestModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task InitDataAsync()
        {
            ProductSerialNumbers = _unitOfWork.ProductRepository.Search(p => true).Select(p => new SelectListItem
            {
                Text = p.SerialNumber,
                Value = p.Id.ToString()
            }).ToList();

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

            var createProducts = importProducts.Where(p => p.Id <= 0);
            //var updateProducts = importProducts.Where(p => p.Id > 0);

            //foreach (var product in updateProducts)
            //{
            //    var dbProduct = _unitOfWork.ProductRepository.Get(p => p.Id == product.Id);

            //    product.Quantity = product.Quantity + dbProduct.Quantity;
            //}

            await _unitOfWork.ProductRepository.CreateManyAsync(createProducts).ConfigureAwait(false);
            //await _unitOfWork.ProductRepository.UpdateManyAsync(updateProducts).ConfigureAwait(false);

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
            if (!SelectedId.IsNullOrEmpty())
            {
                var product = _unitOfWork.ProductRepository.Get(p => p.Id == int.Parse(SelectedId));
                product.Quantity = 0;
                StateMemory.ImportRequestProducts.Add(product);
                return RedirectToPage("./ImportRequest");
            }
            StateMemory.ImportRequest = ImportRequest;

            return RedirectToPage("./CreateProduct");
        }
    }
}
