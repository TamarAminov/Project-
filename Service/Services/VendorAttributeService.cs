using AutoMapper;
using Repository.Entities;
using Repository.Interfaces;
using Service.Dto.EventDto;
using Service.Dto.TasksDto;
using Service.Dto.UserDto;
using Service.Dto.VendorAttributeDto;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Service.Services
{
    public class VendorAttributeService : IVendorAttributeService
    {
        private readonly IVendorAttributeRepository repository;
        private readonly IMapper mapper;
        public VendorAttributeService(IVendorAttributeRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<VendorAttributeDtoo> AddItem(VendorAttributeDtoo item)
        {
            // ✅ חדש
            if (string.IsNullOrWhiteSpace(item.VendorAttributeName))
                throw new DomainException("שם מאפיין חובה");
            if (string.IsNullOrWhiteSpace(item.Value))
                throw new DomainException("ערך מאפיין חובה");
            if (item.VendorId <= 0)
                throw new DomainException("ספק לא תקין");

            var entity = mapper.Map<VendorAttribute>(item);
            entity.VendorAttributeID = 0;

            var result = await repository.AddItem(entity);
            return mapper.Map<VendorAttributeDtoo>(result);
            }

        public async Task DeleteItem(int id)
        {
           await repository.DeleteItem(id);
        }

        public async Task<List<VendorAttributeDtoo>> GetAll(int id)
        {   
            var item= await repository.GetAll(id);
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

        public async Task<Dictionary<int, List<VendorAttributeDtoo>>> GetByVendorIdsAsync(List<int> vendorIds)
        {
            var attrs = await repository.GetByVendorIdsAsync(vendorIds);
            return attrs
                .GroupBy(a => a.VendorID)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(a => new VendorAttributeDtoo
                    {
                        VendorAttributeID = a.VendorAttributeID,
                        VendorAttributeName = a.VendorAttributeName,
                        Value = a.Value,
                        VendorId = a.VendorID
                    }).ToList()
                );
        }
        public async Task<VendorAttributeDtoo> UpdateItem(int id, VendorAttributeDtoo item)
        {

            // 1. בדיקה אם הפריט קיים ב-DB לפני העדכון
            var existingItem = await repository.GetById(id);
            // ✅ חדש
            if (existingItem == null)
                throw new DomainException("מאפיין לא נמצא");
            if (string.IsNullOrWhiteSpace(item.VendorAttributeName))
                throw new DomainException("שם מאפיין חובה");
            if (string.IsNullOrWhiteSpace(item.Value))
                throw new DomainException("ערך מאפיין חובה");
            if (item.VendorId <= 0)
                throw new DomainException("ספק לא תקין");

            var vendorAttributeToUpdate = mapper.Map<VendorAttributeDtoo, VendorAttribute>(item);
            vendorAttributeToUpdate.VendorAttributeID = id;

           await repository.UpdateItem(id, vendorAttributeToUpdate);
            // 3. החזרת הפריט המעודכן ל-Controller
            return mapper.Map<VendorAttributeDtoo>(vendorAttributeToUpdate);
        }
       
    }
}
