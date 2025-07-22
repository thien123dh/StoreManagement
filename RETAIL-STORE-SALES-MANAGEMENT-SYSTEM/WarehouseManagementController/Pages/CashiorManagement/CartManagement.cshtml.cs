using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WarehouseManagementController.SessionHelper;
using WarehouseManagementService.Implement;

namespace WarehouseManagementController.Pages.CashiorManagement
{
    [IgnoreAntiforgeryToken]
    public class CartManagementModel : PageModel
    {
        public ShoppingCart Cart { get; set; }

        [BindProperty]
        public int ProductId { get; set; }

        [BindProperty]
        public int Quantity { get; set; }

        public IActionResult OnPostUpdateQuantity()
        {
            try
            {
                Console.WriteLine($"Gọi vào UpdateQuantity: ProductId={ProductId}, Quantity={Quantity}");

                var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
                if (cart == null)
                    return new JsonResult(new { success = false, error = "Cart không tồn tại" });

                var item = cart.Items.FirstOrDefault(x => x.ProductId == ProductId);
                if (item == null)
                    return new JsonResult(new { success = false, error = "Không tìm thấy sản phẩm" });

                item.Quantity = Quantity;
                HttpContext.Session.SetObjectAsJson("Cart", cart);

                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi server: " + ex.Message);
                return new JsonResult(new { success = false, error = ex.Message });
            }
        }


        public IActionResult OnGet()
        {

            var cartData = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");


            if (cartData == null)
            {

                Cart = new ShoppingCart();
            }
            else
            {

                Cart = cartData;
            }

            return Page();
        }

        public IActionResult OnPostCheckout()
        {
            // Lấy dữ liệu giỏ hàng từ Session
            var cartData = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");

            if (cartData == null || cartData.Items.Count == 0)
            {
                // Xử lý giỏ hàng rỗng
                return RedirectToPage("EmptyCart");
            }

            // Lưu giỏ hàng vào session (nếu cần thiết)
            HttpContext.Session.SetObjectAsJson("Cart", cartData);
            HttpContext.Session.SetObjectAsJson("TotalAmount", cartData.Price());
            HttpContext.Session.SetObjectAsJson("TotalPrice", cartData.Price());

            return RedirectToPage("./Checkout");
        }
    }
}
