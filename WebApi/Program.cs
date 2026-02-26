using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectEvent.Models;
using Repository.Entities;
using Repository.Interfaces;
using Repository.Repositories;
using Service.Dto.BudgetItemDto;
using Service.Dto.EventDto;
using Service.Dto.TasksDto;
using Service.Dto.VendorAttributeDto;
using Service.Dto.VendorDto;
using Service.Interfaces;
using Service.Profiles;
using Service.Services;


var builder = WebApplication.CreateBuilder(args);
// 1. תוודאי שה-using הזה הוא היחיד למעלה
builder.Services.AddAutoMapper(typeof(Program));

var config = new MapperConfiguration(cfg => {
    cfg.CreateMap<Repository.Entities.User, Service.Dto.UserDto.UserDtoo>();
});
IMapper maper = config.CreateMapper();
//services.AddSingleton(maper);

var mapperConfig = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});





builder.Services.AddAutoMapper(typeof(MappingProfile));
AutoMapper.IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
// הרישום הזה פותר את השגיאה שקיבלת עכשיו:
builder.Services.AddScoped<IContext, EventMaster>();
// הרישום הזה חסר לך וזה מה שגורם לקריסה:
//builder.Services.AddScoped(typeof(Repository.Interfaces.IRepository<User>), typeof(Repository.Repositories.UserRepository));
builder.Services.AddScoped(typeof(Repository.Interfaces.IUserRepository), typeof(Repository.Repositories.UserRepository));
// זה אומר למערכת: בכל פעם שמישהו מבקש IRepository של משהו, תביא לו את המחלקה Repository הכללית
builder.Services.AddScoped<IUserService, UserService>();
// --- ????? ?-Repositories (???? ?-Data) ---
builder.Services.AddScoped<IRepository<Vendor>, VendorRepository>();
builder.Services.AddScoped<IRepository<VendorAttribute>, VendorAttributeRepository>();
builder.Services.AddScoped<IRepository<Event>, EventRepository>();
builder.Services.AddScoped<IRepository<Tasks>, TaskRepository>();
builder.Services.AddScoped<IRepository<BudgetItem>, BudgetItemRepository>();

// --- ????? ?-Services (???? ???????) ---
// ???? ?? ??-Controller ???? IService<VendorDtoo>, ??? ?? ???? ??????? ???:
builder.Services.AddScoped<IService<VendorDtoo>, VendorService>();
builder.Services.AddScoped<IService<VendorAttributeDtoo>, VendorAttributeService>();
builder.Services.AddScoped<IService<EventDtoo>, EventService>();
builder.Services.AddScoped<IService<TasksDtoo>, TasksService>();
builder.Services.AddScoped<IService<BudgetItemDtoo>, BudgetItemService>();

// ?? ?? ?? IUserService ???? ???? ?-User:
builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddScoped<IRepository, UserRepository>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// חשוב מאוד: זה חייב לבוא לפני app.MapControllers()
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();
// הדרך הנכונה לרשום את ה-Profile
// הפתרון שעוקף את בעיית הגרסאות וה-typeof
// יצירת קונפיגורציה ידנית כדי לעקוף את בעיית הגרסאות


// הגדרת המיפויים
// הגדרה מפורשת שמוודאת שימוש ב-AutoMapper המקורי
