using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WarehouseManagementData.Models;
using WarehouseManagementRepository;
using WarehouseManagementService.Dto.Request;
using WarehouseManagementService.StateMemory;

namespace WarehouseManagementController.Pages.ProductManagement
{
    public class CreateProductModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty(SupportsGet = true)]
        public int? Id { get; set; }
        public List<SelectListItem> Categories = default!;

        [BindProperty(SupportsGet = true)]
        public CreateProductRequest Product { set; get; } = default!;

        [BindProperty(SupportsGet = true)]
        public List<SelectListItem> ProductSerialNumbers { set; get; } = default!;

        [BindProperty]
        public IFormFile ImageFile { get; set; }

        private readonly IWebHostEnvironment _webHostEnvironment;
        public CreateProductModel(UnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment )
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public void setDataInit(Product? product)
        {
            Product.Manufactor = product.Manufactor;
            Product.SellingPrice = product.SellingPrice ?? 0;
            Product.ImportPrice = product.ImportPrice;
            Product.Notes = product.Notes;
            Product.Status = product.Status;
            Product.Quantity = 0;
            Product.CategoryId = product.CategoryId;
            Product.Description = product.Description;
            Product.Name = product.Name;
            Product.ImageUrl = product.ImageUrl;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync().ConfigureAwait(false);

            Categories = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
            }).ToList();

            if (Id != null)
            {
                var product = _unitOfWork.ProductRepository.Search(p => p.Id == Id)
                    .Include(p => p.Category)
                    .FirstOrDefault();

                setDataInit(product);
            }

            return Page();
        }

        private int? GetLoginUserId()
        {
            return HttpContext.Session.GetInt32("UserID");
        }


        public async Task<IActionResult> OnPostImportProductAsync()
        {
            // Nếu cả hai đều null => lỗi
            if (ImageFile == null && string.IsNullOrWhiteSpace(Product.ImageUrl))
            {
                ModelState.AddModelError("Product.ImageUrl", "Bạn cần cung cấp link ảnh hoặc chọn ảnh từ máy.");
                return Page();
            }

            // Ưu tiên link ảnh nếu có
            if (!string.IsNullOrWhiteSpace(Product.ImageUrl))
            {
                // Không làm gì, giữ nguyên link
            }
            else if (ImageFile != null)
            {
                // Nếu không có link thì mới xử lý upload
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "imageProduct");
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileStream);
                }

                Product.ImageUrl = "/imageProduct/" + uniqueFileName;
            }
            // Nếu không có file mà chỉ có link => giữ nguyên Product.ImageUrl đã nhập

            // Nếu là sửa thì lấy sản phẩm cũ, không thì tạo mới
            var product = Id != null
                ? _unitOfWork.ProductRepository.Get(p => p.Id == Id)
                : new Product();

            var categories = _unitOfWork.CategoryRepository.GetAll();

            // Cập nhật sản phẩm
            product.Name = Product.Name;
            product.Manufactor = Product.Manufactor;
            product.SellingPrice = Product.SellingPrice;
            product.ImportPrice = Product.ImportPrice;
            product.Notes = Product.Notes;
            product.Status = Product.Status;
            product.CategoryId = Product.CategoryId;
            product.Description = Product.Description;
            product.ImageUrl = Product.ImageUrl; // <--- dòng này đảm bảo luôn gán link ảnh
            product.Quantity = (product.Quantity > 0 ? product.Quantity : 0) + Product.Quantity;
            product.UpdatedDateTime = DateTime.Now;
            product.CreatedDateTime = DateTime.Now;
            product.ManufactureDateTime = DateTime.Now;
            product.CreatedBy = GetLoginUserId();
            product.UpdatedBy = GetLoginUserId();
            product.Category = categories.FirstOrDefault(c => c.Id == product.CategoryId);

            StateMemory.ImportRequestProducts.Add(product);

            return RedirectToPage("./ImportRequest");
        }

    }
}
