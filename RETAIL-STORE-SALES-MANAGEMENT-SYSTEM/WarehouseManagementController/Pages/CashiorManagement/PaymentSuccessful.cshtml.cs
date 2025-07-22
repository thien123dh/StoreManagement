using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WarehouseManagementController.SessionHelper;
using WarehouseManagementController.VnPayHelper;
using WarehouseManagementData.Models;
using WarehouseManagementRepository;
using WarehouseManagementService.Implement;

namespace WarehouseManagementController.Pages.CashiorManagement
{
    public class Payment_successfulModel : PageModel
    {
        public int UserID { get; set; }
        private readonly UnitOfWork _unitOfWork;

        public Payment_successfulModel(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> OnGet()
        {
            // Lấy dữ liệu từ Session
            var cartData = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            var totalAmount = HttpContext.Session.GetObjectFromJson<decimal>("TotalAmount");
            var totalPrice = HttpContext.Session.GetObjectFromJson<decimal>("TotalPrice");
            var userIdFromSession = HttpContext.Session.GetInt32("UserID");

            if (!userIdFromSession.HasValue || cartData == null)
            {
                return RedirectToPage("/Error"); 
            }

            UserID = userIdFromSession.Value;
            var cashier = _unitOfWork.UserRepository.GetById(UserID);

            if (cashier == null)
            {
                return RedirectToPage("/Error");
            }

            // Tạo Customer mới gắn Id từ user
            var newCustomer = new Customer
            {
                Fullname = "Văn Hà",
                ContactNumber = "0123456789",
                Email = "vanha10062000@gmail.com",
                CreatedBy = UserID,
                UpdatedBy = UserID,
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now
            };
            _unitOfWork.CustomerRepository.Create(newCustomer);

            // Tạo đơn hàng
            var newOrder = new Receipt
            {
                ReceiptSerialNumber = "OR" + UserID + DateTime.Now.ToString("yyyyMMddHHmmss"),
                CustomerId = newCustomer.Id,
                CustomerName = newCustomer.Fullname,
                Address = "Địa chỉ giao hàng",
                CreatedBy = UserID,
                CreatedDateTime = DateTime.Now,
                Promotion = 0,
                TotalPrice = totalAmount,
                Status = 1
            };
            _unitOfWork.ReceiptRepository.Create(newOrder);
            await _unitOfWork.ReceiptRepository.SaveAsync(); // Lưu đơn hàng để có ReceiptId

            // Tạo các ReceiptDetail
            foreach (var item in cartData.Items)
            {
                var orderDetail = new ReceiptDetail
                {
                    ReceiptId = newOrder.Id,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                };
                _unitOfWork.ReceiptDetailRepository.Create(orderDetail);
            }

            await _unitOfWork.ReceiptDetailRepository.SaveAsync(); // Lưu các chi tiết đơn

            // Xóa session
            HttpContext.Session.Remove("Cart");
            HttpContext.Session.Remove("cartQuantity");
            TempData.Remove("CartData");
            TempData.Remove("TotalAmount");

            return Page();
        }
    }
}
