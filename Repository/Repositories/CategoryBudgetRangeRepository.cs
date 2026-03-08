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
    public class CategoryBudgetRangeRepository : ICategoryBudgetRangeRepository
    {
        private readonly IContext _context;
        public CategoryBudgetRangeRepository(IContext context)
        {
            this._context = context;
        }
        public async Task<CategoryBudgetRange> AddItem(CategoryBudgetRange item)
        {
            _context.CategoryBudgetRanges.AddAsync(item);
            await _context.save();
            return item;
        }

        public  async Task DeleteItem(int id)
        {
            var categoryBudgetRange = await GetById(id);
            _context.CategoryBudgetRanges.Remove(categoryBudgetRange);
            await _context.save();
        }

        public async Task<List<CategoryBudgetRange>> GetAll()
        {
            return _context.CategoryBudgetRanges.ToList();

        }

        public async Task<List<CategoryBudgetRange>> GetAll(int id)
        {
            return await GetAll();
        }

        public async Task<CategoryBudgetRange> GetById(int id)
        {
            return await _context.CategoryBudgetRanges.FirstOrDefaultAsync(X => X.CategoryBudgetRangeID == id);
        }

        public async Task UpdateItem(int id, CategoryBudgetRange item)
        {
            var categoryBudgetRange= await GetById(id);
            categoryBudgetRange.MaxBudget = item.MaxBudget;
            categoryBudgetRange.MinBudget = item.MinBudget;
            categoryBudgetRange.Percentage = item.Percentage;
            _context.CategoryBudgetRanges.Update(categoryBudgetRange);

            await _context.save();
        }
        public async Task<CategoryBudgetRange> GetByBudget(int categoryId, double totalBudget)
        {
            return await _context.CategoryBudgetRanges
                .Where(r => r.CategoryID == categoryId
                    && r.MinBudget <= totalBudget
                    && (r.MaxBudget == 0 || r.MaxBudget >= totalBudget))
                .FirstOrDefaultAsync();
        }
    }
}
