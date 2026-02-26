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
    public class VendorRepository : IRepository<Vendor>
    {
        private readonly IContext _context;
        public VendorRepository(IContext context)
        {
            this._context = context;
        }
        public async Task<Vendor> AddItem(Vendor item)
        {
            _context.Vendors.AddAsync(item);
            await _context.save();
            return item;
        }

        public async Task DeleteItem(int id)
        {
            var item = await GetById(id);
            _context.Vendors.Remove(item);
            await _context.save();
        }

        public async Task<List<Vendor>> GetAll()
        {
            return  _context.Vendors.ToList();
        }

        public async Task<List<Vendor>> GetAll(int id)
        {
            return _context.Vendors.Where(x => x.VendorID == id).ToList();
        }

        public async Task<Vendor> GetById(int id)
        {
            return await _context.Vendors.FirstOrDefaultAsync(X => X.VendorID == id);
        }

        public async Task UpdateItem(int id, Vendor item)
        {
            var vendor = await GetById(id);
            vendor.BasePrice = item.BasePrice;
            vendor.BusinessName = item.BusinessName;
            vendor.AreaID = item.AreaID;
            //vendor.CategoryID = item.CategoryID;
            vendor.Attributes = item.Attributes;
            vendor.Status = item.Status;
            vendor.Events= item.Events;
            _context.Vendors.Update(vendor);

            await _context.save();
        }
    }
}
