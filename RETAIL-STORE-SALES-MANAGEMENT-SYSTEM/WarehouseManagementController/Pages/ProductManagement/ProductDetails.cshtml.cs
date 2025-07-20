using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WarehouseManagementData.Models;
using WarehouseManagementRepository;

namespace WarehouseManagementController.Pages.ProductManagement
{
    public class ProductDetailsModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public Product Product { set; get; } = default!;

        public ProductDetailsModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.Search(p => p.Id == id)
                .Include(p => p.Category)
                .Include(p => p.CreatedByNavigation)
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            Product = product;

            return Page();
        }
    }
}
