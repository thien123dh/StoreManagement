using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseManagementData.Models;

public partial class Receipt
{
    public int Id { get; set; }

    public string ReceiptSerialNumber { get; set; } = null!;

    public decimal? TotalPrice { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public int? CreatedBy { get; set; }

    public string? Address { get; set; }

    public int? CustomerId { get; set; }

    public string? Notes { get; set; }

    public short? Status { get; set; }

    public decimal? Promotion { get; set; }

    public string? CustomerName { get; set; }

    [ForeignKey(nameof(CreatedBy))]
    public virtual User? CreatedByNavigation { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; } = new List<ReceiptDetail>();

    public virtual string ProductSerialNumbers => string.Join(",", ReceiptDetails.Select(r => r.Product?.SerialNumber).ToList());
}
