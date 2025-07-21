using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementData.Models;
using WarehouseManagementService.Dto.Request;

namespace WarehouseManagementService.StateMemory
{
    public static class StateMemory
    {
        public static List<Product> ImportRequestProducts = new List<Product>();

        public static CreateImportRequest ImportRequest = new CreateImportRequest();
    }
}
