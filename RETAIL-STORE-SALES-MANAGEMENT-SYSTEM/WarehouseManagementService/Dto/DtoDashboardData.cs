using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementService.Dto
{
    public class BestSellerProductDto
    {
        public string ProductName { get; set; }
        public int ProductId { set; get; }
        public int TotalQuantity { set; get; }
        public decimal TotalPrice { set; get; }
    }
    public class DtoDashboardData
    {
        public List<int> TotalImportedData { get; set; } = default!;

        public List<int> TotalSelledData { get; set; } = default!;

        public List<int> TotalSelledByMonth { get; set; } = default!;

        public decimal TotalRevenue { set; get; }

        public decimal TotalImportPrice { set; get; }

        public BestSellerProductDto? BestSellerProduct { set; get; } = default!;
    }
}
