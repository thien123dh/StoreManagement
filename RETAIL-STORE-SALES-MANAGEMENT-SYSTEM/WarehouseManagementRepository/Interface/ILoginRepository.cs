using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementData.Models;

namespace WarehouseManagementRepository.Interface
{
    public interface ILoginRepository
    {
        User checkLogin(string userName, string password);
    }
}
