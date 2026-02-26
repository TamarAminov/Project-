using AutoMapper;
using Microsoft.Extensions.Logging;
using Repository.Entities;
using Repository.Interfaces;
using Service.Dto.BudgetItemDto;
using Service.Dto.EventDto;
using Service.Dto.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class EventService : IService<EventDtoo>
    {
        private readonly IRepository<Event> repository;
        private readonly IMapper mapper;
        public EventService(IRepository<Event> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<EventDtoo> AddItem(EventDtoo item)
        {
            var EventEntity = mapper.Map<Event>(item);
            EventEntity.EventID = 0;
            var i = await repository.AddItem(EventEntity);
            return  mapper.Map<Event, EventDtoo>(i);
        }

        public async Task DeleteItem(int id)
        {
              await  repository.DeleteItem(id);
        }

        public async  Task<List<EventDtoo>> GetAll(UserDtoo user)
        {
            if (user.Role == User.EnumRole.Manager)
            {
                var events1 = await repository.GetAll();
                return mapper.Map<List<Event>, List<EventDtoo>>(events1);
            }
            var events= await repository.GetAll(user.UserID);
            return mapper.Map<List<Event>, List<EventDtoo>>(events);
        }

        public async Task<EventDtoo> GetById(int id)
        {
            var eventItem = await repository.GetById(id);
            if (eventItem == null)
            {
                return null; // או לזרוק Exception לפי החלטה
            }
            return mapper.Map<Event, EventDtoo>(eventItem);
        }

        public async Task<EventDtoo> UpdateItem(int id, EventDtoo item)
        {
            // 1. בדיקה אם הפריט קיים ב-DB לפני העדכון
            var existingItem = await repository.GetById(id);
            if (existingItem == null) return null;
            var eventToUpdate = mapper.Map<EventDtoo, Event>(item);
            eventToUpdate.EventID = id;
             await repository.UpdateItem(id, eventToUpdate);
            // 3. החזרת הפריט המעודכן ל-Controller
            return mapper.Map<Event, EventDtoo>(eventToUpdate);



            
        }
    }
}
