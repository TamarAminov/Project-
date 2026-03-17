using Service.Dto.VendorAttributeDto;
using Service.Dto.VendorDto;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IVendorService: IService<VendorDtoo>
    {
        public Task<int> GetMinPriceOfVendorByCategory(int categoryId);
        public Task<int> GetMaxPriceOfVendorByCategory(int categoryId);
        public Task AddVendorsToEvent(int eventId, List<int> vendorIds);
        //public Task<Dictionary<int, List<VendorAttributeDtoo>>> GetByVendorIdsAsync(List<int> vendorIds);

    }
}
