using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementService.Dto.User
{
    public class UserDetailDto
    {
        public int Id { get; set; }

        public string? BusinessId { set; get; }

        public string? Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Fullname { get; set; }

        public int? Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? ContactNumber { get; set; }

        public int Role { get; set; }

        public int? Status { set; get; }
    }
}
