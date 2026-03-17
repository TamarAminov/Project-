using AutoMapper;
using Repository.Entities;
using Service.Dto.BudgetItemDto;
using Service.Dto.CategoryDto;
using Service.Dto.EventDto;
using Service.Dto.EventTypeDto;
using Service.Dto.TasksDto;
using Service.Dto.UserDto;
using Service.Dto.VendorAttributeDto;
using Service.Dto.VendorDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Profiles
{
   

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // כאן אנחנו מגדירים את ה"גשר" בין ה-DTOs
            CreateMap<UserDtoo, UserUpdateDto>();
            CreateMap<UserRegisterDto, UserDtoo>();
            CreateMap<UserDtoo, UserRegisterDto>();
            // זו השורה שהייתה חסרה וגרמה לשגיאה:
            CreateMap<UserRegisterDto, User>();

            // אם תצטרכי בעתיד להפוך חזרה מ-Entity ל-Dto:
            CreateMap<User, UserRegisterDto>();
            // אם יש לך עוד המרות (למשל מ-User למודל), הוסיפי אותן כאן
            CreateMap<User, UserDtoo>();

            // אם את צריכה להפוך גם מה-DTO חזרה לישות (למשל בעדכון/הוספה):
            CreateMap<UserDtoo, User>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<User, UserUpdateDto>();
            // מיפוי עבור Vendor (דו-כיווני)
            CreateMap<Vendor, VendorDtoo>().ReverseMap();
           // מיפוי עבור VendorAttribute
            CreateMap<VendorAttribute, VendorAttributeDtoo>().ReverseMap();
            // מיפוי עבור Event
            CreateMap<Event, EventDtoo>().ReverseMap();
            // מיפוי עבור Tasks
            CreateMap<Tasks, TasksDtoo>().ReverseMap();
            // מיפוי עבור BudgetItem
            CreateMap<BudgetItem, BudgetItemDtoo>().ReverseMap();
            // הוספת מיפוי עבור סוגי אירועים (EventType)
            CreateMap<EventType, EventTypeDtoo>().ReverseMap();
            CreateMap<EventDtoo, Event>()
            .ForMember(dest => dest.AllEventType, opt => opt.Ignore()); // אומר ל-Mapper: אל תנסה למפות את האובייקט הזה ב-Post
            CreateMap<EventTypeDtoo, EventType>();
            CreateMap<EventType, EventTypeDtoo>();
            CreateMap<Category, CategoryDtoo>();
            CreateMap<CategoryDtoo, Category>();
            CreateMap<TasksDtoo, Tasks>();
            CreateMap<Tasks, TasksDtoo>();



            CreateMap<Event, EventVendorDto>();

            CreateMap<VendorAttribute, VendorAttributeDtoo>();
            CreateMap<VendorAttributeDtoo,VendorAttribute> ();


        }
    }

}
