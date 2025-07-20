using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementData.Models;
using WarehouseManagementData.Paging;
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

        public async Task<Paginate<User>> GetPagination(string keyword, int pageIndex, int pageSize)
        {
            var pagination = await _unitOfWork.UserRepository.GetPagingListAsync(
                selector: u => u,
                predicate: u => true,
                page: pageIndex,
                size: pageSize
            );

            return pagination;
        }
    }
}
