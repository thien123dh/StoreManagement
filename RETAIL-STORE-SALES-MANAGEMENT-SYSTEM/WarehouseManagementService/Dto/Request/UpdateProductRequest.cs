using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementData.Models;

namespace WarehouseManagementService.Dto.Request
{
    public class UpdateProductRequest
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string Manufactor { get; set; }

        public int Quantity { set; get; }

        public decimal SellingPrice { get; set; }

        public decimal? ImportPrice { get; set; }

        public string? ImageUrl { set; get; }

        public string? Notes { get; set; }

        public short? Status { get; set; }

        public int? CategoryId { get; set; }
    }

    public class CreateProductRequest
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public string Manufactor { get; set; }

        public int Quantity { set; get; }

        public decimal SellingPrice { get; set; }

        public decimal? ImportPrice { get; set; }

        public string? ImageUrl { set; get; }

        public string? Notes { get; set; }

        public short? Status { get; set; }

        public int? CategoryId { get; set; }
    }
}
