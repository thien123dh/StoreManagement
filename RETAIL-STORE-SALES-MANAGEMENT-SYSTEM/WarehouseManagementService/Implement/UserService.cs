using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementRepository;
using WarehouseManagementService.Interface;

namespace WarehouseManagementService.Implement
{
    public class UserService : IUserService
    {
        private readonly UnitOfWork _unitOfWork;

        public UserService(UnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork; 
        }


    }
}
