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
    public class EventTypeRepository : IRepository<EventType>
    {
        private readonly IContext _context;
        public EventTypeRepository(IContext context)
        {
            this._context = context;
        }
        public async Task< EventType> AddItem(EventType item)
        {
             _context.EventType.AddAsync(item);
           await _context.save();
            return item;
        }

        public async Task DeleteItem(int id)
        {
            var item = await GetById(id);
            _context.EventType.Remove(item);
           await _context.save();
        }

        public async Task< List<EventType>> GetAll()
        {
            return _context.EventType.ToList();
        }

        public async Task<List<EventType>> GetAll(int id)
        {
            return await GetAll();
        }

        public async Task<EventType> GetById(int id)
        {

            return await _context.EventType.FirstOrDefaultAsync(x => x.EventTypeID == id);
        }

        public async Task UpdateItem(int id, EventType item)
        {
            var et =await GetById(id);
            et.EventTypeName = item.EventTypeName;
            _context.EventType.Update(et);

            await _context.save();
        }
    }
}
