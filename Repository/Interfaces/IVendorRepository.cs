using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IVendorRepository:IRepository<Vendor>
    {
        public Task<int> GetMinPriceOfVendorByCategory(int categoryId);
        public Task<int> GetMaxPriceOfVendorByCategory(int categoryId);
    }
}
