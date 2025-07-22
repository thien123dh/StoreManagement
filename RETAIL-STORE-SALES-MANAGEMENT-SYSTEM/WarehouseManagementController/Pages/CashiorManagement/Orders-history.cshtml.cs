using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WarehouseManagementData.Models;
using WarehouseManagementData.Paging;
using WarehouseManagementRepository;
using WarehouseManagementService.StateMemory;

namespace WarehouseManagementController.Pages.CashiorManagement
{
    public class Orders_historyModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;

        [BindProperty]
        public Paginate<Receipt> Receipts { get; set; } = default!;

        [BindProperty]
        public string Keyword { get; set; } = "";

        [BindProperty]
        public int PageIndex { get; set; } = 1;

        public int Size { get; set; } = 100;


        public Orders_historyModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private async Task<Paginate<Receipt>> SearchAsync()
        {
            var receipt = await _unitOfWork.ReceiptRepository.GetPagingListAsync<Receipt>(
                selector: p => p,
                predicate: p => p.CustomerName.ToLower().Contains(Keyword.ToLower()),
                orderBy: o => o.OrderByDescending(p => p.CreatedDateTime),
                include: i => i.Include(p => p.ReceiptDetails)
                .Include(p => p.Customer),
                page: PageIndex,
                size: Size
            );

            return receipt;
        }

        public async Task<IActionResult> OnPostRedirectToImportRequestAsync()
        {
            StateMemory.Clear();

            return RedirectToPage("./HomePage");
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var receipt = await SearchAsync();

            Receipts = receipt;

            return Page();
        }
    }
}

