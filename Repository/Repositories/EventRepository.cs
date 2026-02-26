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
    public class EventRepository : IRepository<Event>
    {
        private readonly IContext _context;
        public EventRepository(IContext context)
        {
            this._context = context;
        }
        public async Task<Event> AddItem(Event item)
        {
            _context.Events.AddAsync(item);
            await _context.save();
            return item;
        }
        public async Task DeleteItem(int id)
        {
            var events=await GetById(id);
            _context.Events.Remove(events);
            await _context.save();
        }

        public async Task<List<Event>> GetAll()
        {
           return _context.Events.ToList();
        }
        public async Task<List<Event>> GetAll(int id) 
        { 
            return _context.Events.Where(x=>x.UserID==id).ToList();
        }
        public async Task<Event> GetById(int id)
        {
            return await _context.Events
            .Include(e => e.AllEventType) // טוען את נתוני סוג האירוע
            .FirstOrDefaultAsync(x => x.EventID == id); // מחפש את האירוע הספציפי
        }

        public async Task UpdateItem(int id, Event item)
        {
            var event1 = await GetById(id);
            event1.EventDate  = item.EventDate;
            event1.EventName = item.EventName;
            event1.EventTypeID = item.EventTypeID;
            event1.UserID = item.UserID;
            event1.AllUser = item.AllUser;
            event1.GuestCount = item.GuestCount;
            event1.TotalBudget = item.TotalBudget;
            event1.Vendors = item.Vendors;
            event1.BudgetItems= item.BudgetItems;
            event1.Tasks= item.Tasks;
            _context.Events.Update(event1);

            await _context.save();
        }
    
    }
}
