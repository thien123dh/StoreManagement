using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementService.Common
{
    public static class BusinessIdHelper
    {
        public static string GenerateBusinessId(int role, int sequenceNumber)
        {
            var roleCode = (role == RoleConstant.ADMIN) ? "AD" : (role == RoleConstant.CASHIER ? "CA" : "IM");
            return $"{roleCode}-{sequenceNumber.ToString("000000")}";
        }
    }
}
