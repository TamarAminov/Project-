using Service.Dto.VendorAttributeDto;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IVendorAttributeService:IService<VendorAttributeDtoo>
    {
        Task<Dictionary<int, List<VendorAttributeDtoo>>> GetByVendorIdsAsync(List<int> vendorIds);
    }
}
