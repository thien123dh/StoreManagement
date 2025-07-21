using System;
using System.Collections.Generic;

namespace WarehouseManagementData.Models;

public partial class ImportRequestDetail
{
    public int Id { get; set; }

    public int ImportRequestId { get; set; }

    public int ProductId { get; set; }

    public decimal? ImportPrice { get; set; }

    public int? Quantity { get; set; }
    public virtual Product Product { get; set; } = null!;
}
