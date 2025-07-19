using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementData.Models;
using WarehouseManagementRepository.Interface;
using WarehouseManagementService.Interface;

namespace WarehouseManagementService.Implement
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        public LoginService(ILoginRepository userRepository)
        {
            _loginRepository = userRepository;
        }
        public User checkLogin(string userName, string password) => _loginRepository.checkLogin(userName, password);
    }
}
