using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class BudgetItemRepository : IRepository<BudgetItem>
    {
        private readonly IContext _context;
        public BudgetItemRepository(IContext context)
        {
            this._context = context;
        }
        public async Task<BudgetItem> AddItem(BudgetItem item)
        {
           _context.BudgetItems.AddAsync(item);
           await _context.save();
           return item;
        }

        public async Task DeleteItem(int id)
        {
            var bi=await GetById(id);
            _context.BudgetItems.Remove(bi);
            await _context.save();
        }

        public async Task<List<BudgetItem>> GetAll()
        {
            return _context.BudgetItems.ToList();
        }

        public async Task<List<BudgetItem>> GetAll(int id)
        {
            return _context.BudgetItems.Where(x => x.BudgetItemID == id).ToList();
        }

        public async Task<BudgetItem> GetById(int id)
        {
            return _context.BudgetItems.FirstOrDefault(X => X.BudgetItemID == id);
        }

        public async Task UpdateItem(int id, BudgetItem item)
        {
            var budgetItem = await GetById(id);
            budgetItem.EventID = item.EventID;
            budgetItem.AllEvent = item.AllEvent;
            budgetItem.CategoryID = item.CategoryID;
            budgetItem.ActualAmount = item.ActualAmount;
            budgetItem.VendorID = item.VendorID;
            budgetItem.IsIgnore = item.IsIgnore;
            budgetItem.IsLocked = item.IsLocked;
            budgetItem.Vendor = item.Vendor;
            budgetItem.PlannedAmount = item.PlannedAmount;
            _context.BudgetItems.Update(budgetItem);

            await _context.save();
        }
    }
}
