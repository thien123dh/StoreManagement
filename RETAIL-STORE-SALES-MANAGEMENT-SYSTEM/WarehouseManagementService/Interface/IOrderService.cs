using WarehouseManagementData.Models;
using WarehouseManagementService.Dto.Customer;
using WarehouseManagementService.Implement;

namespace WarehouseManagementService.Interface
{
    public interface IOrderService
    {
        public Receipt ProcessPaymentMethod(OrderInfo customerInfo, ShoppingCart carts, int? userId = null);
    }
}
