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
    public class TaskRepository : IRepository<Tasks>
    {
        private readonly IContext _context;
        public TaskRepository(IContext context)
        {
            this._context = context;
        }
        public async Task< Tasks> AddItem(Tasks item)
        {
             _context.Tasks.AddAsync(item);
           await _context.save();
            return item;
        }

        public async Task DeleteItem(int id)
        {
            var item = await GetById(id);
            _context.Tasks.Remove(item);
           await _context.save();
        }

        public async Task<List<Tasks>> GetAll()
        {
            return _context.Tasks.ToList();
        }

        public async Task<List<Tasks>> GetAll(int id)
        {
            return _context.Tasks.Where(x => x.AllEvent.AllUser.UserID == id).ToList();
        }

        public async Task<Tasks> GetById(int id)
        {
            return await _context.Tasks.Include(e => e.AllEvent).FirstOrDefaultAsync(x => x.TaskID == id);

        }

        public async Task UpdateItem(int id, Tasks item)
        {
            var tasks =await GetById(id);
            //tasks.Description = item.Description;
            //tasks.Vendor = item.Vendor;
            //tasks.DueDate = item.DueDate;
            //tasks.VendorID = item.VendorID;
            //tasks.EventID = item.EventID;
            //tasks.AllEvent= item.AllEvent;
            tasks.IsCompleted= item.IsCompleted;
            _context.Tasks.Update(tasks);

            await _context.save();
        }
    }
}
