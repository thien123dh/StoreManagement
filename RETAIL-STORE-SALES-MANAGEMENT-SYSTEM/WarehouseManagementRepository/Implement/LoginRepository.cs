using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementData.Base;
using WarehouseManagementData.Models;
using WarehouseManagementRepository.Interface;

namespace WarehouseManagementRepository.Implement
{
    public class LoginRepository : ILoginRepository
    {
        public User checkLogin(string userName, string password) => LoginDAO.Instance.checkLogin(userName, password);
    }
}
