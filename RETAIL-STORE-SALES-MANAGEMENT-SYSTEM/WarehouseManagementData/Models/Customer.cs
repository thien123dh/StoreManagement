using System;
using System.Collections.Generic;

namespace WarehouseManagementData.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string? Fullname { get; set; }

    public string? Address { get; set; }

    public string? ContactNumber { get; set; }

    public string? Email { get; set; }

    public short? Status { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public DateTime? UpdatedDateTime { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<Receipt> Receipts { get; set; } = new List<Receipt>();

    public virtual User? UpdatedByNavigation { get; set; }
}
