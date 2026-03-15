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
    public class VendorRepository : IVendorRepository
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
            return _context.Vendors.Where(x => x.CategoryID == id).ToList();
        }

        public async Task<Vendor> GetById(int id)
        {
            return await _context.Vendors.FirstOrDefaultAsync(X => X.VendorID == id);
        }

        public async Task<int> GetMaxPriceOfVendorByCategory(int categoryId)
        {
            return await _context.Vendors
            .Where(b => b.CategoryID == categoryId)
            .MaxAsync(b => (int)b.BasePrice);
        }

        public async Task<int> GetMinPriceOfVendorByCategory(int categoryId)
        {
            return await _context.Vendors
            .Where(b => b.CategoryID == categoryId)
            .MinAsync(b => (int)b.BasePrice);
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
        //    public async Task AddVendorsToEvent(int eventId, List<int> vendorIds)
        //    {
        //        var eventEntity = await _context.Events
        //            .Include(e => e.Vendors)
        //            .FirstOrDefaultAsync(e => e.EventID == eventId);
        //        if (eventEntity == null) return;

        //        foreach (var vendorId in vendorIds)
        //        {
        //            var vendor = await GetById(vendorId);
        //            if (vendor != null && !eventEntity.Vendors.Any(v => v.VendorID == vendorId))
        //                eventEntity.Vendors.Add(vendor);
        //        }
        //        await _context.save(); // ✅ קיים כאן
        //    }
        public async Task AddVendorsToEvent(int eventId, List<int> vendorIds)
        {
            var eventEntity = await _context.Events
                .Include(e => e.Vendors)
                .FirstOrDefaultAsync(e => e.EventID == eventId);
            if (eventEntity == null) return;

            // שליפת הספקים החדשים כדי לדעת את הקטגוריות שלהם
            var newVendors = await _context.Vendors
                .Where(v => vendorIds.Contains(v.VendorID))
                .ToListAsync();

            // קבלת הקטגוריות של הספקים החדשים
            var categoriesToReplace = newVendors.Select(v => v.CategoryID).ToHashSet();

            // ✅ מחיקה רק של ספקים מאותן קטגוריות
            var vendorsToRemove = eventEntity.Vendors
                .Where(v => categoriesToReplace.Contains(v.CategoryID))
                .ToList();

            foreach (var v in vendorsToRemove)
                eventEntity.Vendors.Remove(v);

            // הוספת הספקים החדשים
            foreach (var vendor in newVendors)
                eventEntity.Vendors.Add(vendor);

            await _context.save();
        }
    }
}
