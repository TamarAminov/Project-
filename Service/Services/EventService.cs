//using AutoMapper;
//using Microsoft.Extensions.Logging;
//using Repository.Entities;
//using Repository.Interfaces;
//using Repository.Repositories;
//using Service.Dto.BudgetItemDto;
//using Service.Dto.EventDto;
//using Service.Dto.UserDto;
//using Service.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Service.Services
//{
//    public class EventService : IEventService
//    {
//        private readonly IEventRepository repository;
//        private readonly IMapper mapper;
//        public EventService(IEventRepository repository, IMapper mapper)
//        {
//            this.repository = repository;
//            this.mapper = mapper;
//        }

//        public async Task<EventDtoo> AddItem(EventDtoo item)
//        {
//            var EventEntity = mapper.Map<Event>(item);
//            EventEntity.EventID = 0;
//            var i = await repository.AddItem(EventEntity);
//            //////////////
//            return  mapper.Map<Event, EventDtoo>(i);
//        }

//        public async Task DeleteItem(int id)
//        {
//              await  repository.DeleteItem(id);
//        }

//        public async Task<EventDtoo> DevideBudgetsDefualt(int id)
//        {
//            var result = await repository.BudgetsDefault(id);
//            return mapper.Map<EventDtoo>(result);
//        }

//        public async  Task<List<EventDtoo>> GetAll(int id)
//        {
//            //if (user.Role == User.EnumRole.Manager)
//            //{
//            //    var events1 = await repository.GetAll();
//            //    return mapper.Map<List<Event>, List<EventDtoo>>(events1);
//            //}
//            var events= await repository.GetAll(id);
//            return mapper.Map<List<Event>, List<EventDtoo>>(events);
//        }

//        public async Task<EventDtoo> GetById(int id)
//        {
//            var eventItem = await repository.GetById(id);
//            if (eventItem == null)
//            {
//                return null; // או לזרוק Exception לפי החלטה
//            }
//            return mapper.Map<Event, EventDtoo>(eventItem);
//        }

//        public async Task<EventDtoo> UpdateItem(int id, EventDtoo item)
//        {
//            // 1. בדיקה אם הפריט קיים ב-DB לפני העדכון
//            var existingItem = await repository.GetById(id);
//            if (existingItem == null) return null;
//            var eventToUpdate = mapper.Map<EventDtoo, Event>(item);
//            eventToUpdate.EventID = id;
//             await repository.UpdateItem(id, eventToUpdate);
//            // 3. החזרת הפריט המעודכן ל-Controller
//            return mapper.Map<Event, EventDtoo>(eventToUpdate);




//        }
//    }
//}
using AutoMapper;
using Microsoft.Extensions.Logging;
using Repository.Entities;
using Repository.Interfaces;
using Service.Dto.BudgetItemDto;
using Service.Dto.EventDto;
using Service.Dto.UserDto;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class EventService : IEventService
    {
        private readonly IRepository<Event> repository;
        private readonly IRepository<CategoryBudgetRange> rangeRepository; // חדש
        private readonly IRepository<BudgetItem> budgetItemRepository;
        private readonly IMapper mapper;
        public EventService(IRepository<Event> repository, IRepository<CategoryBudgetRange> rangeRepository,
        IRepository<BudgetItem> budgetItemRepository, IMapper mapper)
        {
            this.repository = repository;
            this.rangeRepository = rangeRepository;
            this.budgetItemRepository = budgetItemRepository;
            this.mapper = mapper;
        }
        public async Task<EventDtoo> AddItem(EventDtoo item)
        {
            var EventEntity = mapper.Map<Event>(item);
            EventEntity.EventID = 0;
            var i = await repository.AddItem(EventEntity);
            // קריאה אוטומטית לפונקציית החלוקה לאחר הוספת האירוע
            await DevideBudgetsDefualt(i.EventID);
            // שליפה מחדש עם התקציבים הטריים מה-DB
            var finalEvent = await repository.GetById(i.EventID);

            return mapper.Map<Event, EventDtoo>(finalEvent);
        }

        public async Task DeleteItem(int id)
        {
            await repository.DeleteItem(id);
        }

        public async Task<EventDtoo> DevideBudgetsDefualt(int eventId)
        {
            // 1. שליפת האירוע כדי לדעת מה התקציב הכולל
            var eventEntity = await repository.GetById(eventId);
            if (eventEntity == null) return null;

            // 2. שליפת כל חוקי התקציב מה-DB
            var allRanges = await rangeRepository.GetAll();

            // 3. סינון הטווח הרלוונטי לתקציב האירוע
            var relevantRules = allRanges.Where(r =>
                eventEntity.TotalBudget >= r.MinBudget &&
                (r.MaxBudget == 0 || eventEntity.TotalBudget <= r.MaxBudget)
            ).ToList();

            // 4. יצירת פריטי התקציב ושמירה ב-DB
            foreach (var rule in relevantRules)
            {
                var newItem = new BudgetItem
                {
                    EventID = eventId,
                    CategoryID = rule.CategoryID,
                    PlannedAmount = (int)(eventEntity.TotalBudget * (rule.Percentage / 100)),
                    ActualAmount = 0,
                    IsIgnore = false,
                    IsLocked = false,
                };
                await budgetItemRepository.AddItem(newItem);
            }
            return mapper.Map<Event, EventDtoo>(eventEntity);
        }

        public async Task<List<EventDtoo>> GetAll(int id)
        {
            //if (user.Role == User.EnumRole.Manager)
            //{
            //    var events1 = await repository.GetAll();
            //    return mapper.Map<List<Event>, List<EventDtoo>>(events1);
            //}
            var events = await repository.GetAll(id);
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