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

    public virtual string StatusName => Status == 1 ? "Kích hoạt" : "Ban";

    public int Role { get; set; }

    public virtual String RoleName => Role == 1 ? "Admininistration" : Role == 2 ? "Thu ngân" : "Quản lý kho";

    public string BusinessId { get; set; } = null!;

}
