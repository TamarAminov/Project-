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
        
        //public async Task AddVendorsToEvent(int eventId, List<int> vendorIds)
        //{
        //    var eventEntity = await _context.Events
        //        .Include(e => e.Vendors)
        //        .FirstOrDefaultAsync(e => e.EventID == eventId);
        //    if (eventEntity == null) return;

        //    // שליפת הספקים החדשים
        //    var newVendors = await _context.Vendors
        //        .Where(v => vendorIds.Contains(v.VendorID))
        //        .ToListAsync();

        //    // הקטגוריות של הספקים החדשים
        //    var newCategoryIDs = newVendors.Select(v => v.CategoryID).ToHashSet();

        //    // מחיקת כל ספק שהקטגוריה שלו:
        //    // 1. נמצאת במערך החדש (החלפה)
        //    // 2. לא נמצאת במערך החדש (הוסר לחלוטין)
        //    var vendorsToRemove = eventEntity.Vendors.ToList();
        //    foreach (var v in vendorsToRemove)
        //        eventEntity.Vendors.Remove(v);

        //    // הוספת הספקים החדשים בלבד
        //    foreach (var vendor in newVendors)
        //        eventEntity.Vendors.Add(vendor);

        //    await _context.save();
        //}

        public async Task AddVendorsToEvent(int eventId, List<int> vendorIds)
        {
            var eventEntity = await _context.Events
                .Include(e => e.Vendors)
                .FirstOrDefaultAsync(e => e.EventID == eventId);
            if (eventEntity == null) return;

            var newVendors = await _context.Vendors
                .Where(v => vendorIds.Contains(v.VendorID))
                .ToListAsync();

            // ספקים שהוסרו = היו באירוע ולא נמצאים במערך החדש
            var removedVendorIds = eventEntity.Vendors
                .Where(v => !vendorIds.Contains(v.VendorID))
                .Select(v => v.VendorID)
                .ToList();

            // מחיקת משימות רק של ספקים שהוסרו
            var tasksToRemove = _context.Tasks
                .Where(t => t.EventID == eventId &&
                            t.VendorID != null &&
                            removedVendorIds.Contains(t.VendorID.Value));
            _context.Tasks.RemoveRange(tasksToRemove);

            // החלפת ספקים
            var vendorsToRemove = eventEntity.Vendors.ToList();
            foreach (var v in vendorsToRemove)
                eventEntity.Vendors.Remove(v);

            foreach (var vendor in newVendors)
                eventEntity.Vendors.Add(vendor);

            await _context.save();
        }
    }
}
