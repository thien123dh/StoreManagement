using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WarehouseManagementData.Models;
using WarehouseManagementData.Paging;
using WarehouseManagementRepository;
using WarehouseManagementService.StateMemory;

namespace WarehouseManagementController.Pages.ProductManagement
{
    public class SearchProductModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;

        [BindProperty]
        public Paginate<Product> Products { get; set; } = default!;
        [BindProperty]
        public Paginate<ImportRequest> ImportRequests { get; set; } = default!;

        [BindProperty]
        public string Keyword { get; set; } = "";

        [BindProperty]
        public int PageIndex { get; set; } = 1;

        public int Size { get; set; } = 100;
        public SearchProductModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private async Task InitDataAsync()
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

            Products = products;

            var importRequests = await _unitOfWork.ImportRequestRepository.GetPagingListAsync<ImportRequest>(
                selector: p => p,
                orderBy: o => o.OrderByDescending(p => p.CreatedDateTime),
                include: i => i.Include(p => p.ImportRequestDetails)
                                    .ThenInclude(d => d.Product)
                                .Include(p => p.CreatedByNavigation),
                page: PageIndex,
                size: 20
            );

            ImportRequests = importRequests;
        }

        public async Task<IActionResult> OnPostRedirectToImportRequestAsync()
        {
            StateMemory.Clear();

            return RedirectToPage("ImportRequest");
        }
        public async Task<IActionResult> OnGetAsync()
        {
            await InitDataAsync().ConfigureAwait(false);

            return Page();
        }
    }
}
