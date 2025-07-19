using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementData.Models;
using WarehouseManagementService.Base;

namespace WarehouseManagementService.Interface
{
    public interface ICategoryServices
    {
        Task<IBusinessResult> GetAll(int page, int size);
        Task<IBusinessResult> GetByIdAsync(string id);
        Task<IBusinessResult> UpdateAsync(Category category);
        Task<IBusinessResult> Save(Category category);
        Task<IBusinessResult> DeleteAsync(int id);
        Task<IBusinessResult> Search(string searchTerm, int page, int size);
        Task<IBusinessResult> GetAllCategories();
        Task<IBusinessResult> GetById(int id);
    }
}
