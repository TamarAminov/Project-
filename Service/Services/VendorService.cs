using AutoMapper;
using Repository.Entities;
using Repository.Interfaces;
using Service.Dto.EventDto;
using Service.Dto.TasksDto;
using Service.Dto.UserDto;
using Service.Dto.VendorDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Service.Services
{
    public class VendorService : IService<VendorDtoo>
    {
        private readonly IRepository<Vendor> repository;
        private readonly IMapper mapper;
        public VendorService(IRepository<Vendor> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<VendorDtoo> AddItem(VendorDtoo item)
        {
            var vendor = mapper.Map<VendorDtoo, Vendor>(item);
            vendor.Attributes = null;
           // vendor.VendorID = 0;
            var i =await repository.AddItem(vendor);
            return mapper.Map<Vendor, VendorDtoo>(i);
        }

        public async Task DeleteItem(int id)
        {
            await repository.DeleteItem(id);
        }

        public async Task<List<VendorDtoo>> GetAll(UserDtoo user)
        {
            if (user.Role == User.EnumRole.Manager)
            {
                var vendors = await repository.GetAll();
                return mapper.Map<List<Vendor>, List<VendorDtoo>>(vendors);
            }
            var item = await repository.GetAll(user.UserID);
            return mapper.Map<List<Vendor>, List<VendorDtoo>>(item);
        }

        public async Task<VendorDtoo> GetById(int id)
        {
            var vendor = await repository.GetById(id);
            if (vendor == null)
            {
                return null; // או לזרוק Exception לפי החלטה
            }
            return mapper.Map<Vendor, VendorDtoo>(vendor);
        }

        public async Task<VendorDtoo> UpdateItem(int id, VendorDtoo item)
        {
            // 1. בדיקה אם הפריט קיים ב-DB לפני העדכון
            var existingItem = await repository.GetById(id);
            if (existingItem == null) return null;

           
            var vendorToUpdate = mapper.Map<VendorDtoo, Vendor>(item);
            vendorToUpdate.VendorID = id;
            await repository.UpdateItem(id, vendorToUpdate);
            // 3. החזרת הפריט המעודכן ל-Controller
            return mapper.Map<VendorDtoo>(vendorToUpdate);
        }
    }
}
