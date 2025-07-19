using System;
using System.Collections.Generic;

namespace WarehouseManagementData.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Fullname { get; set; }

    public int? Gender { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? ContactNumber { get; set; }

    public int? Status { get; set; }

    public int Role { get; set; }

    public string BusinessId { get; set; } = null!;

    public virtual ICollection<Category> CategoryCreatedByNavigations { get; set; } = new List<Category>();

    public virtual ICollection<Category> CategoryUpdatedByNavigations { get; set; } = new List<Category>();

    public virtual ICollection<Customer> CustomerCreatedByNavigations { get; set; } = new List<Customer>();

    public virtual ICollection<Customer> CustomerUpdatedByNavigations { get; set; } = new List<Customer>();

    public virtual ICollection<ImportRequest> ImportRequestCreatedByNavigations { get; set; } = new List<ImportRequest>();

    public virtual ICollection<ImportRequest> ImportRequestUpdatedByNavigations { get; set; } = new List<ImportRequest>();

    public virtual ICollection<Product> ProductCreatedByNavigations { get; set; } = new List<Product>();

    public virtual ICollection<Product> ProductUpdatedByNavigations { get; set; } = new List<Product>();

    public virtual ICollection<Receipt> Receipts { get; set; } = new List<Receipt>();
}
