using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementData.Models;
using WarehouseManagementData.Paging;

namespace WarehouseManagementService.Interface
{
    public interface IUserService
    {
        public Task<Paginate<User>> GetPagination(string keyword, int pageIndex, int pageSize);
    }
}
