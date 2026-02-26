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
    public class AreaRepository : IRepository<Area>
    {
        private readonly IContext _context;
        public AreaRepository(IContext context)
        {
            this._context = context;
        }
        public async Task<Area> AddItem(Area item)
        {
             _context.Area.AddAsync(item);
            await _context.save();
            return item;
        }

        public async Task DeleteItem(int id)
        {
            var area = await GetById(id);
            _context.Area.Remove(area);
           await _context.save();
        }

        public async Task<List<Area>> GetAll()
        {
            return  _context.Area.ToList();
        }

        public async Task<List<Area>> GetAll(int id)
        {
            return await GetAll();
        }

        public async Task<Area> GetById(int id)
        {

            return await _context.Area.FirstOrDefaultAsync(x => x.AreaID == id);
        }

        public async Task UpdateItem(int id, Area item)
        {
            var area = await GetById(id);
            area.AreaName = item.AreaName;
            _context.Area.Update(area);

            await _context.save();
        }
  
    }
}
