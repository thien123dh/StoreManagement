using System;
using System.Collections.Generic;

namespace WarehouseManagementData.Models;

public partial class ImportRequest
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedDateTime { get; set; }

    public int? UpdatedBy { get; set; }

    public string ImportedSerialNumber { get; set; } = null!;

    public short? Status { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<ImportRequestDetail> ImportRequestDetails { get; set; } = new List<ImportRequestDetail>();

    public virtual User? UpdatedByNavigation { get; set; }
}
