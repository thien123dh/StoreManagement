using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementData.Models;

namespace WarehouseManagementService.Interface
{
    public interface ILoginService
    {
        User checkLogin(string userName, string password);
    }
}
