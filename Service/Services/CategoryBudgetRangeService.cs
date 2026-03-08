using AutoMapper;
using Repository.Entities;
using Repository.Interfaces;
using Service.Dto.CategoryBudgetRangeDto;
using Service.Dto.EventDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CategoryBudgetRangeService : IService<CategoryBudgetRangeDtoo>
    {

        private readonly IRepository<CategoryBudgetRange> repository;
        private readonly IMapper mapper;
        public CategoryBudgetRangeService(IRepository<CategoryBudgetRange> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<CategoryBudgetRangeDtoo> AddItem(CategoryBudgetRangeDtoo item)
        {
            var CategoryBudgetRangeEntity = mapper.Map<CategoryBudgetRange>(item);
            CategoryBudgetRangeEntity.CategoryBudgetRangeID= 0;
            var i = await repository.AddItem(CategoryBudgetRangeEntity);
            return mapper.Map<CategoryBudgetRange, CategoryBudgetRangeDtoo>(i);
        }

        public async Task DeleteItem(int id)
        {
            await repository.DeleteItem(id);
        }

        public async Task<List<CategoryBudgetRangeDtoo>> GetAll(int id)
        {
            var categoryBudgetRange = await repository.GetAll(id);
            return mapper.Map<List<CategoryBudgetRange>, List<CategoryBudgetRangeDtoo>>(categoryBudgetRange);
        }

        public async Task<CategoryBudgetRangeDtoo> GetById(int id)
        {
            var categoryBudgetRange = await repository.GetById(id);
            if (categoryBudgetRange == null)
            {
                return null; // או לזרוק Exception לפי החלטה
            }
            return mapper.Map<CategoryBudgetRange, CategoryBudgetRangeDtoo>(categoryBudgetRange);
        }

        public async Task<CategoryBudgetRangeDtoo> UpdateItem(int id, CategoryBudgetRangeDtoo item)
        {
            var categoryBudgetRange = await repository.GetById(id);
            if (categoryBudgetRange == null)
            {
                return null; // או לזרוק Exception לפי החלטה
            }
            var update = mapper.Map<CategoryBudgetRangeDtoo, CategoryBudgetRange>(item);
            update.CategoryBudgetRangeID = id;
            await repository.UpdateItem(id, update);
            return mapper.Map<CategoryBudgetRange, CategoryBudgetRangeDtoo>(categoryBudgetRange);

        }
    }
}
