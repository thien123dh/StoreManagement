using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementService.Dto.User;

namespace WarehouseManagementService.Common
{
    public static class RoleConstant
    {
        public static short ADMIN = 1;
        public static short CASHIER = 2;
        public static short IMPORTER = 3;

        public static List<RoleDto> GetAllRoles()
        {
            return new List<RoleDto> {
                new RoleDto { Id = ADMIN, Name = "Quản trị viên" },
                new RoleDto { Id = CASHIER, Name = "Thu ngân" },
                new RoleDto { Id = IMPORTER, Name = "Quản lý kho" }
            };
        }
    }
}
