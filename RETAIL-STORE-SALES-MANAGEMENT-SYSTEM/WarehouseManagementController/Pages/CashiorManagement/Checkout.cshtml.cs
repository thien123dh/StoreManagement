using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WarehouseManagementController.SessionHelper;
using WarehouseManagementData.Models;
using WarehouseManagementService.Implement;
using WarehouseManagementController.VnPayHelper;

namespace WarehouseManagementController.Pages.CashiorManagement
{
    public class CheckoutModel : PageModel
    {
        private readonly StoreManagementDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly Utils _utils;

        public CheckoutModel(StoreManagementDbContext context, IConfiguration configuration, Utils utils)
        {
            _context = context;
            _configuration = configuration;
            _utils = utils;
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
            TempData["CartData"] = cartData;
            TempData["TotalPrice"] = totalPrice;
            TempData["TotalAmount"] = totalAmount;

            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return Page();
        }
        [BindProperty]
        public Receipt Order { get; set; } = default!;

        [BindProperty]
        public string Cart { get; set; } = string.Empty;

        public IActionResult OnPost()
        {
            var cartData = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            var totalAmount = HttpContext.Session.GetObjectFromJson<decimal>("TotalAmount");
            var totalPrice = HttpContext.Session.GetObjectFromJson<decimal>("TotalPrice");
            var userIdFromSession = HttpContext.Session.GetInt32("CashierId");

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
