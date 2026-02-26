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
    public class CategoryRepository : IRepository<Category>
    {
        private readonly IContext _context;
        public CategoryRepository(IContext context)
        {
            this._context = context;
        }
        public async Task< Category> AddItem(Category item)
        {
             _context.Category.AddAsync(item);
           await _context.save();
            return item;
        }

        public async Task DeleteItem(int id)
        {
            var category=await GetById(id);
            _context.Category.Remove(category);
            await _context.save();
        }

        public async  Task<List<Category>> GetAll()
        {
            return _context.Category.ToList();
        }

        public async Task<List<Category>> GetAll(int id)
        {
            return await GetAll();
        }

        public async Task<Category> GetById(int id)
        {
            return await _context.Category.FirstOrDefaultAsync(X => X.CategoryID == id);
        }

        public async Task UpdateItem(int id, Category item)
        {
            var category = await GetById(id);
            category.CategoryName = item.CategoryName;
            _context.Category.Update(category);

            await _context.save();
        }
    }
}
