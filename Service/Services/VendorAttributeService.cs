using AutoMapper;
using Repository.Entities;
using Repository.Interfaces;
using Service.Dto.EventDto;
using Service.Dto.TasksDto;
using Service.Dto.UserDto;
using Service.Dto.VendorAttributeDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Service.Services
{
    public class VendorAttributeService : IService<VendorAttributeDtoo>
    {
        private readonly IRepository<VendorAttribute> repository;
        private readonly IMapper mapper;
        public VendorAttributeService(IRepository<VendorAttribute> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<VendorAttributeDtoo> AddItem(VendorAttributeDtoo item)
        {
            var entity = mapper.Map<VendorAttribute>(item);

            // זה השלב הקריטי: מאפסים את ה-ID ל-0
            // כך SQL Server ידע לייצר מספר חדש בעצמו
            entity.VendorAttributeID = 0;

            var result = await repository.AddItem(entity);
            return mapper.Map<VendorAttributeDtoo>(result);
            //var i = await repository.AddItem(mapper.Map<VendorAttributeDtoo, VendorAttribute>(item));
            //return mapper.Map<VendorAttribute,VendorAttributeDtoo>(i);
        }

        public async Task DeleteItem(int id)
        {
           await repository.DeleteItem(id);
        }

        public async Task<List<VendorAttributeDtoo>> GetAll(UserDtoo user)
        {

            if (user.Role == User.EnumRole.Manager)
            {
                var vendorAttributes =await repository.GetAll();
                return mapper.Map<List<VendorAttribute>, List<VendorAttributeDtoo>>(vendorAttributes);
            }
            var item= await repository.GetAll(user.UserID);
            return mapper.Map<List<VendorAttribute>, List<VendorAttributeDtoo>>(item);
        }

        public async Task<VendorAttributeDtoo> GetById(int id)
        {
            var vendorAttribute =await repository.GetById(id);
            if (vendorAttribute == null)
            {
                return null; 
            }
            return mapper.Map<VendorAttribute, VendorAttributeDtoo>(vendorAttribute);
        }

        public async Task<VendorAttributeDtoo> UpdateItem(int id, VendorAttributeDtoo item)
        {
            // 1. בדיקה אם הפריט קיים ב-DB לפני העדכון
            var existingItem = await repository.GetById(id);
            if (existingItem == null) return null;

            
            var vendorAttributeToUpdate = mapper.Map<VendorAttributeDtoo, VendorAttribute>(item);
            vendorAttributeToUpdate.VendorAttributeID = id;

           await repository.UpdateItem(id, vendorAttributeToUpdate);
            // 3. החזרת הפריט המעודכן ל-Controller
            return mapper.Map<VendorAttributeDtoo>(vendorAttributeToUpdate);
        }
    }
}
