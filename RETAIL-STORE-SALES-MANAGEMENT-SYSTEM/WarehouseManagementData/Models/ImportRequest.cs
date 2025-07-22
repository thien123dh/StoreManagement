using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace WarehouseManagementData.Models;

public partial class ImportRequest
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public int? CreatedBy { get; set; }

    [ForeignKey(nameof(CreatedBy))]
    public virtual User CreatedByNavigation { set; get; } 

    public DateTime? UpdatedDateTime { get; set; }

    public int? UpdatedBy { get; set; }

    public string ImportedSerialNumber { get; set; } = null!;

    public short? Status { get; set; }
    public virtual ICollection<ImportRequestDetail> ImportRequestDetails { get; set; } = new List<ImportRequestDetail>();

    public virtual string ProductSerialNumbers => ImportRequestDetails.IsNullOrEmpty() ? "" : String.Join(",", ImportRequestDetails.Select(i => i.Product.SerialNumber));
}
