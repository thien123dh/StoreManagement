using System;
using System.Collections.Generic;

namespace WarehouseManagementData.Models;

public partial class ReceiptDetail
{
    public int Id { get; set; }

    public string? ProductName { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public int? ReceiptId { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Receipt? Receipt { get; set; }
}
