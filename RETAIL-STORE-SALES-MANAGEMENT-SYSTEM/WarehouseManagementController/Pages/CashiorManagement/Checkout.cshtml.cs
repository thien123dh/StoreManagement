using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WarehouseManagementController.SessionHelper;
using WarehouseManagementData.Models;
using WarehouseManagementService.Implement;
using WarehouseManagementController.VnPayHelper;
using WarehouseManagementService.Dto.Customer;
using WarehouseManagementService.Interface;

namespace WarehouseManagementController.Pages.CashiorManagement
{
    public class CheckoutModel : PageModel
    {
        private readonly StoreManagementDbContext _context;
        private readonly IOrderService _orderService;
        private readonly IConfiguration _configuration;
        private readonly Utils _utils;

        public CheckoutModel(StoreManagementDbContext context, 
            IConfiguration configuration, 
            Utils utils, 
            IOrderService orderService)
        {
            _context = context;
            _configuration = configuration;
            _utils = utils;
            _orderService = orderService;
        }

        public int UserID { get; set; }
        public IActionResult OnGet()
        {
            var cartData = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            var totalPrice = HttpContext.Session.GetObjectFromJson<decimal>("TotalPrice");
            var totalAmount = HttpContext.Session.GetObjectFromJson<decimal>("TotalAmount");
            var userIdFromSession = HttpContext.Session.GetInt32("UserID");
            if (userIdFromSession.HasValue)
            {
                UserID = userIdFromSession.Value;
            }
            TempData["CartData"] = cartData ?? new ShoppingCart();
            TempData["TotalPrice"] = totalPrice;
            TempData["TotalAmount"] = totalAmount;

            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return Page();
        }
        [BindProperty]
        public Receipt Order { get; set; } = default!;

        [BindProperty]
        public OrderInfo OrderInfo { get; set; } = new OrderInfo();

        [BindProperty]
        public string Cart { get; set; } = string.Empty;

        private IActionResult ProcessCashPayment()
        {
            var customerInfo = OrderInfo;
            var shopingCart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");

            var result = _orderService.ProcessPaymentMethod(customerInfo, shopingCart, GetLoginUserId());
            TempData["SuccessMessage"] = $"Thanh toán hóa đơn {result.ReceiptSerialNumber} thành công";
            ClearSession();
            return RedirectToPage("/ReceiptManagement/SearchReceipt");
        }

        private void ClearSession()
        {
            HttpContext.Session.Remove("Cart");
            HttpContext.Session.Remove("TotalAmount");
            HttpContext.Session.Remove("TotalPrice");
        }

        private int? GetLoginUserId()
        {
            return HttpContext.Session.GetInt32("UserID");
        }

        public IActionResult OnPost()
        {
            var cartData = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            var totalAmount = HttpContext.Session.GetObjectFromJson<decimal>("TotalAmount");
            var totalPrice = HttpContext.Session.GetObjectFromJson<decimal>("TotalPrice");
            var userIdFromSession = GetLoginUserId();

            if (userIdFromSession.HasValue)
            {
                UserID = userIdFromSession.Value;
            }

            if (totalAmount <= 0 || totalAmount > 1000000000)
            {
                ModelState.AddModelError(string.Empty, "Số tiền không hợp lệ.");
                return Page();
            }

            if (totalPrice <= 0 || totalPrice > 1000000000)
            {
                ModelState.AddModelError(string.Empty, "Số tiền không hợp lệ.");
                return Page();
            }

            if (OrderInfo.PaymentMethod == 1) //Cashpayment
            {
                return ProcessCashPayment();
            }
            //VNPAY
            HttpContext.Session.SetObjectAsJson("OrderInfo", OrderInfo);
            string tmnCode = _configuration["VNPAY:TmnCode"];
            string hashSecret = _configuration["VNPAY:HashSecret"];
            string vnpUrl = _configuration["VNPAY:VnpUrl"];
            string returnUrl = _configuration["VNPAY:ReturnUrl"];
            string txnRef = DateTime.Now.Ticks.ToString();

            var Prices = (long)totalAmount * 100;

            VnPayLibrary vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", tmnCode);
            vnpay.AddRequestData("vnp_Amount", Prices.ToString());
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", _utils.GetIpAddress());
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang");
            vnpay.AddRequestData("vnp_OrderType", "other");
            vnpay.AddRequestData("vnp_ReturnUrl", returnUrl);
            vnpay.AddRequestData("vnp_TxnRef", txnRef);

            string paymentUrl = vnpay.CreateRequestUrl(vnpUrl, hashSecret);
            return Redirect(paymentUrl);
        }
    }
}
