using WarehouseManagementData.Models;
using WarehouseManagementRepository;
using WarehouseManagementService.Dto.Customer;
using WarehouseManagementService.Interface;

namespace WarehouseManagementService.Implement
{
    public class OrderService : IOrderService
    {
        private readonly UnitOfWork _unitOfWork;

        public OrderService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private string GenerateReceiptSerialNumber()
        {
            var sequenceNumber = _unitOfWork.ReceiptRepository.Search(r => r.CreatedDateTime != null && r.CreatedDateTime.Value.Date == DateTime.Now.Date)?.Count() ?? 0;

            return $"RE{DateTime.Now.ToString("yyMMdd")}-{sequenceNumber.ToString("00000")}";
        }
        public Receipt ProcessPaymentMethod(OrderInfo orderInfo, ShoppingCart carts, int? loginUserId)
        {
            var totalPrice = (int)(carts?.Items?.Sum(c => c.Quantity * c.Price) ?? 0);

            var customer = _unitOfWork.CustomerRepository.Get(c => c.ContactNumber == orderInfo.CustomerContactNumber);

            if (customer == null && orderInfo.CustomerContactNumber != null)
            {
                customer = new Customer
                {
                    Address = "",
                    ContactNumber = orderInfo.CustomerContactNumber,
                    Fullname = orderInfo.CustomerName,
                    Email = "",
                    CreatedBy = loginUserId,
                    CreatedDateTime = DateTime.Now,
                    UpdatedBy = loginUserId,
                    UpdatedDateTime = DateTime.Now,
                    Status = 1
                };

                _unitOfWork.CustomerRepository.Create(customer);
            }

            var receipt = new Receipt
            {
                CreatedBy = loginUserId,
                CustomerName = orderInfo.CustomerName ?? "Khách vãng lai",
                CreatedDateTime = DateTime.Now,
                TotalPrice = totalPrice,
                Promotion = 0,
                Status = 1,
                Notes = orderInfo.Notes,
                Address = "",
                ReceiptSerialNumber = GenerateReceiptSerialNumber(),
                CustomerId = customer?.Id
            };

            _unitOfWork.ReceiptRepository.Create(receipt);

            var receiptDetails = carts?.Items?.Select(
                i => new ReceiptDetail
                {
                    Price = i.Price,
                    Quantity = i.Quantity,
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    ReceiptId = receipt.Id
                }
            );

            if (receiptDetails != null)
            {
                _unitOfWork.ReceiptDetailRepository.CreateMany(receiptDetails);
            }

            var productIds = carts?.Items?.Select(c => c.ProductId)?.ToHashSet() ?? new HashSet<int>();
            var products = _unitOfWork.ProductRepository.Search(p => productIds.Contains(p.Id));

            foreach (var product in products)
            {
                var cartItem = carts?.Items?.FirstOrDefault(c => c.ProductId == product.Id);

                product.Quantity -= cartItem?.Quantity ?? 0;

                if (product.Quantity < 0)
                    product.Quantity = 0;

                product.UpdatedBy = loginUserId;
            }

            _unitOfWork.ProductRepository.UpdateMany(products);

            return receipt;
        }
    }
}
