using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WarehouseManagementData.Models;
using WarehouseManagementData.Paging;
using WarehouseManagementRepository;

namespace WarehouseManagementController.Pages.ProductManagement
{
    public class SearchProductModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;

        [BindProperty]
        public Paginate<Product> Products { get; set; } = default!;

        [BindProperty]
        public string Keyword { get; set; } = "";

        [BindProperty]
        public int PageIndex { get; set; } = 1;

        public int Size { get; set; } = 10;
        public SearchProductModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private async Task<Paginate<Product>> SearchAsync()
        {
            var products = await _unitOfWork.ProductRepository.GetPagingListAsync<Product>(
                selector: p => p,
                predicate: p => p.Name.ToLower().Contains(Keyword.ToLower()),
                orderBy: o => o.OrderByDescending(p => p.CreatedDateTime),
                include: i => i.Include(p => p.CreatedByNavigation)
                .Include(p => p.Category),
                page: PageIndex,
                size: Size
            );

            return products;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var product = await SearchAsync();

            Products = product;

            return Page();
        }
    }
}
