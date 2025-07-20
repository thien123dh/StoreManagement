using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseManagementData.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Manufactor { get; set; }

    public string SerialNumber { get; set; } = null!;

    public decimal? SellingPrice { get; set; }

    public int? Quantity { get; set; }

    public decimal? ImportPrice { get; set; }

    public string? ImageUrl { set; get; }

    public DateTime? ManufactureDateTime { get; set; }

    public string? Notes { get; set; }

    public short? Status { get; set; }

    public virtual string StatusName => Status == 1 ? "Còn sử dụng" : "Không còn sử dụng";

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public DateTime? UpdatedDateTime { get; set; }

    public int? CategoryId { get; set; }

    public virtual Category? Category { get; set; }

    [ForeignKey(nameof(Product.CreatedBy))]
    public virtual User? CreatedByNavigation { get; set; }
}
