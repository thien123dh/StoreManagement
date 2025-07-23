using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WarehouseManagementController.SessionHelper;
using WarehouseManagementController.VnPayHelper;
using WarehouseManagementData.Models;
using WarehouseManagementRepository;
using WarehouseManagementService.Dto.Customer;
using WarehouseManagementService.Implement;
using WarehouseManagementService.Interface;

namespace WarehouseManagementController.Pages.CashiorManagement
{
    public class Payment_successfulModel : PageModel
    {
        private readonly IOrderService _orderService;
        public int UserID { get; set; }

        public Payment_successfulModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        private void ClearSession()
        {
            // Xóa session
            HttpContext.Session.Remove("Cart");
            HttpContext.Session.Remove("OrderInfo");
            HttpContext.Session.Remove("TotalPrice");
            HttpContext.Session.Remove("TotalAmount");
            HttpContext.Session.Remove("cartQuantity");
            TempData.Remove("CartData");
            TempData.Remove("TotalAmount");
        }
        public async Task<IActionResult> OnGetAsync()
        {
            // Lấy dữ liệu từ Session
            var cartData = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            var totalAmount = HttpContext.Session.GetObjectFromJson<decimal>("TotalAmount");
            var totalPrice = HttpContext.Session.GetObjectFromJson<decimal>("TotalPrice");
            var loginUserId = HttpContext.Session.GetInt32("UserID");
            var orderInfo = HttpContext.Session.GetObjectFromJson<OrderInfo>("OrderInfo");

            if (cartData == null || loginUserId == null)
            {
                return RedirectToPage("/Error"); 
            }

            ClearSession();

            var result = _orderService.ProcessPaymentMethod(orderInfo, cartData, loginUserId);
            TempData["SuccessMessage"] = $"Thanh toán hóa đơn {result.ReceiptSerialNumber} thành công";
            return RedirectToPage("/ReceiptManagement/SearchReceipt");
        }
    }
}
