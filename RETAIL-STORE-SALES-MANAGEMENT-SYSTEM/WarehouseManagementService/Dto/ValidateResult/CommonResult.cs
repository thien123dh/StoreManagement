using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementService.Dto.ValidateResult
{
    public class CommonResult
    {
        public bool IsSuccess { set; get; } = true;
        public string? ErrorMessage { set; get; }
        
    }

    public static class ResultHelper
    {
        public static CommonResult BuildResult(string? msg = null)
        {
            return new CommonResult
            {
                ErrorMessage = msg ?? "Thành công",
            };
        }

        public static CommonResult BuildError(string? msg = null)
        {
            return new CommonResult
            {
                IsSuccess = false,
                ErrorMessage = msg ?? "Lỗi",
            };
        }
    }
}
