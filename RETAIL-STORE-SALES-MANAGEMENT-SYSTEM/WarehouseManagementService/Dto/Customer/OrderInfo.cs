using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementService.Dto.Customer
{
    public class OrderInfo
    {
        public string? CustomerName { set; get; }

        public string? CustomerContactNumber { set; get; }

        public string? Notes { set; get; }

        [StringLength(100, ErrorMessage = "Địa chỉ không được bỏ trống")]
        public string? Adres { set; get; }

        public int? PaymentMethod { set; get; } = 1;
    }
}
