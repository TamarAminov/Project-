using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Interfaces;
using Repository.Repositories;
using Service.Dto.EventDto;
using Service.Dto.TasksDto;
using Service.Dto.UserDto;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Service.Services
{
    public class TasksService : ITasksService
    {
        private readonly ITaskRepository repository;
        private readonly IRepository<Event> eventRepository;
        private readonly IMapper mapper;
        public TasksService(ITaskRepository repository, IMapper mapper, IRepository<Event> eventRepository)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.eventRepository = eventRepository;
        }

        public async Task< TasksDtoo> AddItem(TasksDtoo item)
        {
            // ✅ חדש
            if (string.IsNullOrWhiteSpace(item.Description))
                throw new DomainException("תיאור משימה חובה");
            if (item.DueDate <= DateOnly.FromDateTime(DateTime.Today))
                throw new DomainException("תאריך יעד חייב להיות בעתיד");
            if (item.VendorID <= 0)
                throw new DomainException("ספק לא תקין");
            if (item.EventID <= 0)
                throw new DomainException("אירוע לא תקין");
            var tasks=mapper.Map<Tasks>(item);
            tasks.TaskID = 0;
            var i = await repository.AddItem(tasks);
            return mapper.Map<Tasks, TasksDtoo>(i);
        }

        public async Task DeleteItem(int id)
        {
           await repository.DeleteItem(id);
        }

        public async Task<List<TasksDtoo>> GetAll(int id)
        {

            //if (user.Role == User.EnumRole.Manager)
            //{
            //    var events =await repository.GetAll();
            //    return mapper.Map<List<Tasks>, List<TasksDtoo>>(events);
            //}
            var item= await repository.GetAll(id);
            return mapper.Map<List<Tasks>, List<TasksDtoo>>(item);
        }

        public async Task<TasksDtoo> GetById(int id)
        {
            var tasksItem =await repository.GetById(id);
            if (tasksItem == null)
            {
                return null; // או לזרוק Exception לפי החלטה
            }
            return mapper.Map<Tasks, TasksDtoo>(tasksItem);
        }


        public async Task<TasksDtoo> UpdateItem(int id, TasksDtoo item)
        {
            
            // 1. בדיקה אם הפריט קיים ב-DB לפני העדכון
            var existingItem = await repository.GetById(id);
            // ✅ חדש
            if (existingItem == null)
                throw new DomainException("משימה לא נמצאה");
            if (string.IsNullOrWhiteSpace(item.Description))
                throw new DomainException("תיאור משימה חובה");
            if (item.DueDate <= DateOnly.FromDateTime(DateTime.Today))
                throw new DomainException("תאריך יעד חייב להיות בעתיד");
            if (item.VendorID <= 0)
                throw new DomainException("ספק לא תקין");
            if (item.EventID <= 0)
                throw new DomainException("אירוע לא תקין");

            var tasksToUpdate = mapper.Map<TasksDtoo, Tasks>(item);
            tasksToUpdate.TaskID = id;

           await repository.UpdateItem(id, tasksToUpdate);
            // 3. החזרת הפריט המעודכן ל-Controller
            return mapper.Map<TasksDtoo>(tasksToUpdate);
        }
        // TasksService.cs — מימוש
        //public async Task<TasksDtoo?> ToggleTask(int taskId, bool isCompleted)
        //{
        //    var task = await repository.GetById(taskId);
        //    if (task == null) return null;

        //    task.IsCompleted = isCompleted;
        //    await repository.SaveChanges();
        //    return mapper.Map<TasksDtoo>(task);
        //}
        public async Task<TasksDtoo?> ToggleTask(int taskId, bool isCompleted)
        {
            var task = await repository.GetById(taskId);
            if (task == null) return null;

            task.IsCompleted = isCompleted;
            await repository.UpdateItem(taskId, task); // ← כבר קיים, כבר עושה save
            return mapper.Map<TasksDtoo>(task);
        }
        public async Task GenerateTasksForVendor(int eventId, int vendorId)
        {
            var ev = await eventRepository.GetById(eventId);
            if (ev == null) throw new DomainException("אירוע לא נמצא");

            await repository.GenerateTasksForVendor(eventId, vendorId, ev.EventDate);
        }
    }
}
