using AutoMapper;
using Repository.Entities;
using Repository.Interfaces;
using Service.Dto.CategoryDto;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CategoryService : IGetService<CategoryDtoo>
    {
        private readonly IRepository<Category> repository;
        private readonly IMapper mapper;

        public CategoryService(IRepository<Category> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<CategoryDtoo>> GetAll()
        {
            var categories = await repository.GetAll();
            return mapper.Map<List<CategoryDtoo>>(categories);
        }

        public async Task<CategoryDtoo> GetById(int id)
        {
            var categoryItem = await repository.GetById(id);
            if (categoryItem == null)
            {
                return null; // ניתן להחליף בזריקת Exception במידת הצורך
            }
            return mapper.Map<CategoryDtoo>(categoryItem);
        }
    }
}
