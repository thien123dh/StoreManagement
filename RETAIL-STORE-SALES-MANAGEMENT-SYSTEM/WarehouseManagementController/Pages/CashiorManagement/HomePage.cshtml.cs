using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WarehouseManagementController.SessionHelper;
using WarehouseManagementData.Models;
using WarehouseManagementData.Paging;
using WarehouseManagementRepository;
using WarehouseManagementService.Base;
using WarehouseManagementService.Implement;

namespace WarehouseManagementController.Pages.CashiorManagement
{
    public class HomePageModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;

        [BindProperty]
        public int CartSize { get; set; } = 0;
        [BindProperty]
        public Paginate<Product> Products { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public List<Category> Categories { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string Keyword { get; set; } = "";

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;
        [BindProperty(SupportsGet = true)]
        public int Size { get; set; } = 9;
        public HomePageModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private async Task<Paginate<Product>> SearchProdoductAsync()
        {
            var products = await _unitOfWork.ProductRepository.GetPagingListAsync<Product>(
                selector: p => p,
                predicate: p => p.Name.ToLower().Contains(Keyword.ToLower()) || p.SerialNumber.ToLower().Contains(Keyword.ToLower()),
                orderBy: o => o.OrderByDescending(p => p.CreatedDateTime),
                include: i => i.Include(p => p.CreatedByNavigation)
                               .Include(p => p.Category),
                page: PageIndex,
                size: Size
            );

            return products;
        }

        private async Task<List<Category>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();

            return categories;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var role = HttpContext.Session.GetInt32("RoleId");
            if (role == null)
            {
                return RedirectToPage("/LoginPage");
            }

            var product = await SearchProdoductAsync();
            Categories = await GetAllCategoriesAsync();
            var carts = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();

            CartSize = carts?.Items?.Count() ?? 0;

            Products = product;

            return Page();
        }
    }
}
