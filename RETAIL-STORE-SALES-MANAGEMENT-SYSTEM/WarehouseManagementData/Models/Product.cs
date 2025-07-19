using System;
using System.Collections.Generic;

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

    public DateTime? ManufactureDateTime { get; set; }

    public string? Notes { get; set; }

    public short? Status { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public DateTime? UpdatedDateTime { get; set; }

    public int? CategoryId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<ImportRequestDetail> ImportRequestDetails { get; set; } = new List<ImportRequestDetail>();

    public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; } = new List<ReceiptDetail>();

    public virtual User? UpdatedByNavigation { get; set; }
}
