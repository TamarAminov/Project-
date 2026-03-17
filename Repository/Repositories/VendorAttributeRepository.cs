using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class VendorAttributeRepository : IVendorAttributeRepository
    {
        private readonly IContext _context;
        public VendorAttributeRepository(IContext context)
        {
            this._context = context;
        }
        public async Task<VendorAttribute> AddItem(VendorAttribute item)
        {
            _context.VendorAttributes.AddAsync(item);
           await _context.save();
            return item;
        }

        public async Task DeleteItem(int id)
        {
            var item = await GetById(id);
            _context.VendorAttributes.Remove(item);
           await _context.save();
        }

        public async Task<List<VendorAttribute>> GetAll()
        {
            return _context.VendorAttributes.ToList();
        }

        public async Task<List<VendorAttribute>> GetAll(int id)
        {
            return _context.VendorAttributes.Where(x => x.VendorID == id).ToList();
        }

        public async Task<VendorAttribute> GetById(int id)
        {
            return  await _context.VendorAttributes.FirstOrDefaultAsync(x => x.VendorAttributeID == id);

        }

        public async Task<List<VendorAttribute>> GetByVendorIdsAsync(List<int> vendorIds)
        {
            return await _context.VendorAttributes
                .Where(a => vendorIds.Contains(a.VendorID))
                .ToListAsync();
        }

        public async Task UpdateItem(int id, VendorAttribute item)
        {
            var vendorAttribute =await GetById(id);
            vendorAttribute.Value=item.Value;
            //vendorAttribute.VendorId = item.VendorId;
            vendorAttribute.VendorAttributeName = item.VendorAttributeName;
            _context.VendorAttributes.Update(vendorAttribute);

            await _context.save();
        }
    }
}
