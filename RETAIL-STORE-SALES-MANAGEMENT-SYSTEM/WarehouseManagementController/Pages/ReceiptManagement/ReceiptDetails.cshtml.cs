using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WarehouseManagementData.Models;
using WarehouseManagementData.Paging;
using WarehouseManagementRepository;

namespace WarehouseManagementController.Pages.ReceiptManagement
{
    public class ReceiptDetailsModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public Receipt Receipt { set; get; } = default!;

        public ReceiptDetailsModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var receipt = await _unitOfWork.ReceiptRepository.Search(p => p.Id == id)
                .Include(p => p.ReceiptDetails).ThenInclude(d => d.Product)
                .Include(p => p.CreatedByNavigation).Include(p => p.Customer)
                .FirstOrDefaultAsync();

            if (receipt == null)
            {
                return NotFound();
            }

            Receipt = receipt;

            return Page();
        }
    }
}
