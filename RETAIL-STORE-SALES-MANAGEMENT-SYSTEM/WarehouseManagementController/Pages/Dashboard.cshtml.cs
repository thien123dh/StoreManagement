using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WarehouseManagementData.Models;
using WarehouseManagementData.Paging;
using WarehouseManagementRepository;

namespace WarehouseManagementController.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;

        public DashboardModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [BindProperty(SupportsGet = true)]
        public int PageIndex { set; get; } = 1;

        [BindProperty]
        public Paginate<Receipt> Receipts { set; get; } = default!;


        [BindProperty]
        public Paginate<Customer> Customer { set; get; } = default!;

        private async Task SearchReceiptsAsync()
        {
            Receipts = await _unitOfWork.ReceiptRepository.GetPagingListAsync<Receipt>(
                selector: p => p,
                orderBy: o => o.OrderByDescending(p => p.CreatedDateTime),
                include: i => i.Include(p => p.ReceiptDetails).ThenInclude(d => d.Product)
                .Include(p => p.CreatedByNavigation),
                page: PageIndex,
                size: 20
            );
        }

        private async Task SearchCustomersAsync()
        {
            Customer = await _unitOfWork.CustomerRepository.GetPagingListAsync<Customer>(
                selector: p => p,
                orderBy: o => o.OrderByDescending(p => p.CreatedDateTime),
                page: PageIndex,
                size: 20
            );
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await SearchReceiptsAsync().ConfigureAwait(false);
            await SearchCustomersAsync().ConfigureAwait(false);
            return Page();
        }
    }
}
