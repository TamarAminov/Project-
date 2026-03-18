using AutoMapper;
using Microsoft.Extensions.Logging;
using Repository.Entities;
using Repository.Interfaces;
using Repository.Repositories;
using Service.Dto.BudgetItemDto;
using Service.Dto.EventDto;
using Service.Dto.UserDto;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Service.Services
{
    public class EventService : IEventService
    {
        private readonly IRepository<Event> repository;
        private readonly IRepository<CategoryBudgetRange> rangeRepository; // חדש
        private readonly IRepository<BudgetItem> budgetItemRepository;
        private readonly IRepository<Vendor> vendorRepository;

        private readonly IMapper mapper;
        public EventService(IRepository<Event> repository, IRepository<CategoryBudgetRange> rangeRepository,
        IRepository<BudgetItem> budgetItemRepository,IRepository<Vendor> vendorRepository, IMapper mapper)
        {
            this.repository = repository;
            this.rangeRepository = rangeRepository;
            this.budgetItemRepository = budgetItemRepository;
            this.vendorRepository=vendorRepository;
            this.mapper = mapper;
        }
        public async Task<EventDtoo> AddItem(EventDtoo item)
        {
            if (string.IsNullOrWhiteSpace(item.EventName))
                throw new DomainException("שם אירוע חובה");
            if (item.TotalBudget < 10000)
                throw new DomainException("תקציב חייב להיות לפחות 10000");
            if (item.GuestCount < 30)
                throw new DomainException("מספר אורחים חייב להיות לפחות 30"); 
            if (item.EventDate <= DateTime.Today)
                throw new DomainException("תאריך חייב להיות בעתיד");
            var EventEntity = mapper.Map<Event>(item);
            EventEntity.EventID = 0;
            var i = await repository.AddItem(EventEntity);
            // קריאה אוטומטית לפונקציית החלוקה לאחר הוספת האירוע
            await DevideBudgetsDefualt(i.EventID);
            // שליפה מחדש עם התקציבים הטריים מה-DB
            var finalEvent = await repository.GetById(i.EventID);
            //await repository.UpdateItem(finalEvent.EventID, finalEvent);

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

            var existingBudgets = eventEntity.BudgetItems?.ToList() ?? new List<BudgetItem>();
            foreach (var b in existingBudgets)
                await budgetItemRepository.DeleteItem(b.BudgetItemID);

            // 2. שליפת כל חוקי התקציב מה-DB
            var allRanges = await rangeRepository.GetAll();

            // 3. סינון הטווח הרלוונטי לתקציב האירוע
            var relevantRules = allRanges.Where(r =>
           
                eventEntity.TotalBudget >= r.MinBudget &&
                (r.MaxBudget == 0 || eventEntity.TotalBudget < r.MaxBudget)
            ).ToList();

            int totalAllocated = 0;

            for (int i = 0; i < relevantRules.Count; i++)
            {
                var rule = relevantRules[i];
                int amount = (i == relevantRules.Count - 1)
                    ? (int)eventEntity.TotalBudget - totalAllocated
                    : (int)(eventEntity.TotalBudget * (rule.Percentage /(double) 100));
                Console.WriteLine($"rule {i}: CategoryID={rule.CategoryID}, Percentage={rule.Percentage}, amount={amount}, totalAllocated={totalAllocated}");

                totalAllocated += amount;

                await budgetItemRepository.AddItem(new BudgetItem
                {
                    EventID = eventId,
                    CategoryID = rule.CategoryID,
                    PlannedAmount = amount,
                    ActualAmount = 0,
                    IsIgnore = false,
                    IsLocked = false,
                });
            }
            return mapper.Map<Event, EventDtoo>(eventEntity);
        }

        public async Task<List<EventDtoo>> GetAll(int id)
        {
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
            var existingItem = await repository.GetById(id);
            if (existingItem == null)
                throw new DomainException("אירוע לא נמצא");

            // ולידציה
            if (string.IsNullOrWhiteSpace(item.EventName))
                throw new DomainException("שם אירוע חובה");
            if (item.TotalBudget < 10000)
                throw new DomainException("תקציב חייב להיות לפחות 10000");
            if (item.GuestCount < 30)
                throw new DomainException("מספר אורחים חייב להיות לפחות 30");
            if (item.EventDate <= DateTime.Today)
                throw new DomainException("תאריך חייב להיות בעתיד");

            bool dateChanged = item.EventDate.Date != existingItem.EventDate.Date;
            bool budgetChanged = item.TotalBudget != existingItem.TotalBudget;

            // מחיקת ספקים — כשהתאריך השתנה או כשהתקציב השתנה
            if (dateChanged || budgetChanged)
            {
                var vendorsToDelete = existingItem.Vendors?.ToList() ?? new List<Vendor>();
                foreach (var vendor in vendorsToDelete)
                    await vendorRepository.DeleteItem(vendor.VendorID);
            }

            // עדכון האירוע ב-DB לפני חלוקה
            var eventToUpdate = mapper.Map<EventDtoo, Event>(item);
            eventToUpdate.EventID = id;
            await repository.UpdateItem(id, eventToUpdate);

            // חלוקת תקציב מחדש — רק כשהתקציב השתנה
            if (budgetChanged)
            {
                var budgetItemsToDelete = existingItem.BudgetItems.ToList()?? new List<BudgetItem>();
                foreach (var b in budgetItemsToDelete)
                    await budgetItemRepository.DeleteItem(b.BudgetItemID);

                await DevideBudgetsDefualt(id);
            }

            return mapper.Map<Event, EventDtoo>(eventToUpdate);
        }
    }
}