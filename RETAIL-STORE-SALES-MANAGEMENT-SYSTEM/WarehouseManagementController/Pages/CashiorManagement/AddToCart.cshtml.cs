using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WarehouseManagementData.Models;
using WarehouseManagementController.SessionHelper;
using WarehouseManagementService.Implement;

namespace WarehouseManagementController.Pages.CashiorManagement
{
    public class AddToCartModel : PageModel
    {
        private readonly StoreManagementDbContext _context;
        public AddToCartModel(StoreManagementDbContext context)
        {
            _context = context;
        }
        public IActionResult OnGet(int id,int quantity, int pageIndex, string searchTerm)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            if (pageIndex == 0) pageIndex = 1;

            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();

            cart.AddToCart(new WarehouseManagementData.CartModel.CartItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                ImageUrl = product.ImageUrl,
                Price = product.SellingPrice,
                Quantity = quantity
            });
            TempData["SuccessMessage"] = $"{product.Name} is added to cart";
            HttpContext.Session.SetInt32("cartQuantity", cart.Items.Count);
            HttpContext.Session.SetObjectAsJson("Cart", cart);
            return RedirectToPage("./HomePage", new { PageIndex = pageIndex, SearchTerm = searchTerm });
        }
    }
}
