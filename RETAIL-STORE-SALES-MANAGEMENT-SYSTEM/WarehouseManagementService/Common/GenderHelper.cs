using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementService.Common
{
    public class GenderDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
    public static class GenderHelper
    {
        public static List<GenderDto> GenderList => new List<GenderDto> {
            new GenderDto { Id = 1, Name = "Nam"},
            new GenderDto { Id = 2, Name = "Nữ"}
        };
    }
}
