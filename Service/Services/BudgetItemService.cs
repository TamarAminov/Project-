using AutoMapper;
using Repository.Entities;
using Repository.Interfaces;
using Service.Dto.BudgetItemDto;
using Service.Dto.EventDto;
using Service.Dto.TasksDto;
using Service.Dto.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class BudgetItemService : IService<BudgetItemDtoo>
    {
        private readonly IRepository<BudgetItem> repository;
        private readonly IMapper mapper;
        public BudgetItemService(IRepository<BudgetItem> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<BudgetItemDtoo> AddItem(BudgetItemDtoo item)
        {
            var bi = mapper.Map<BudgetItem>(item);
            bi.BudgetItemID = 0;
            var i = await repository.AddItem(bi);
            return mapper.Map<BudgetItem, BudgetItemDtoo>(i);
        }

        public async Task DeleteItem(int id)
        {
           await repository.DeleteItem(id);
            //to check
        }

        public async Task<List<BudgetItemDtoo>> GetAll(int id)
        {
            //if (user.Role == User.EnumRole.Manager)
            //{
            //    var budgets = await repository.GetAll();
            //    return mapper.Map<List<BudgetItem>, List<BudgetItemDtoo>>(budgets);
            //}
            var bi = await repository.GetAll(id);
            return mapper.Map<List<BudgetItem>, List<BudgetItemDtoo>>(bi);
        }

        public async Task<BudgetItemDtoo> GetById(int id)
        {
            var budgetsItem = await repository.GetById(id);
            if (budgetsItem == null)
            {
                return null; // או לזרוק Exception לפי החלטה
            }
            return mapper.Map<BudgetItem, BudgetItemDtoo>(budgetsItem);
        }

        public async Task<BudgetItemDtoo> UpdateItem(int id, BudgetItemDtoo item)
        {
            // 1. בדיקה אם הפריט קיים ב-DB לפני העדכון
            var existingItem = await repository.GetById(id);
            if (existingItem == null) return null;
            // 2. מיפוי ועדכון
            var budgetsToUpdate = mapper.Map<BudgetItemDtoo, BudgetItem>(item);
            budgetsToUpdate.BudgetItemID = id;

           await repository.UpdateItem(budgetsToUpdate.BudgetItemID, budgetsToUpdate);



            // 3. החזרת הפריט המעודכן ל-Controller
            return mapper.Map<BudgetItem, BudgetItemDtoo>(budgetsToUpdate);




            //var budgetsToUpdate = mapper.Map<BudgetItemDtoo, BudgetItem>(item);
            //budgetsToUpdate.BudgetItemID = id;

            //await repository.UpdateItem(id, budgetsToUpdate);

        }
    }
}
