using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ProjectEvent.Models;
using Repository.Entities;
using Repository.Interfaces;
using Repository.Repositories;
using Service.Dto.BudgetItemDto;
using Service.Dto.CategoryDto;
using Service.Dto.EventDto;
using Service.Dto.EventTypeDto;
using Service.Dto.TasksDto;
using Service.Dto.VendorAttributeDto;
using Service.Dto.VendorDto;
using Service.Interfaces;
using Service.Profiles;
using Service.Services;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
// 1. úååãàé ùä-using äæä äåà äéçéã ìîòìä
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
// äøéùåí äæä ôåúø àú äùâéàä ù÷éáìú òëùéå:
builder.Services.AddScoped<IContext, EventMaster>();
// äøéùåí äæä çñø ìê åæä îä ùâåøí ì÷øéñä:
//builder.Services.AddScoped(typeof(Repository.Interfaces.IRepository<User>), typeof(Repository.Repositories.UserRepository));
builder.Services.AddScoped(typeof(Repository.Interfaces.IUserRepository), typeof(Repository.Repositories.UserRepository));
// æä àåîø ìîòøëú: áëì ôòí ùîéùäå îá÷ù IRepository ùì îùäå, úáéà ìå àú äîçì÷ä Repository äëììéú
builder.Services.AddScoped<IUserService, UserService>();
// --- ????? ?-Repositories (???? ?-Data) ---
builder.Services.AddScoped<IRepository<Vendor>, VendorRepository>();
builder.Services.AddScoped<IRepository<VendorAttribute>, VendorAttributeRepository>();
builder.Services.AddScoped<IRepository<Event>, EventRepository>();
builder.Services.AddScoped<IRepository<EventType>, EventTypeRepository>();
builder.Services.AddScoped<IRepository<Tasks>, TaskRepository>();
builder.Services.AddScoped<IRepository<BudgetItem>, BudgetItemRepository>();
builder.Services.AddScoped<IRepository<Category>, CategoryRepository>();


// --- ????? ?-Services (???? ???????) ---
// ???? ?? ??-Controller ???? IService<VendorDtoo>, ??? ?? ???? ??????? ???:
builder.Services.AddScoped<Service.Services.IService<VendorDtoo>, VendorService>();
builder.Services.AddScoped<Service.Services.IService<VendorAttributeDtoo>, VendorAttributeService>();
builder.Services.AddScoped<Service.Services.IService<EventDtoo>, EventService>();
builder.Services.AddScoped<Service.Services.IService<TasksDtoo>, TasksService>();
builder.Services.AddScoped<Service.Services.IService<BudgetItemDtoo>, BudgetItemService>();
builder.Services.AddScoped<IGetService<EventTypeDtoo>, EventTypeService>();
builder.Services.AddScoped<IGetService<CategoryDtoo>, CategoryService>();
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
    options.AddPolicy("AllowReact", policy =>
    {
        policy.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YOUR_SECRET_KEY_MIN_16_CHARS")),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });


// ── חשוב: הסדר חייב להיות כך ──

//var app = builder.Build();
//app.UseCors("AllowReact");  // ← לפני app.UseAuthorization
//app.UseAuthorization();
//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//// çùåá îàåã: æä çééá ìáåà ìôðé app.MapControllers()


//app.MapControllers();

//app.Run();
//// äãøê äðëåðä ìøùåí àú ä-Profile
//// äôúøåï ùòå÷ó àú áòééú äâøñàåú åä-typeof
//// éöéøú ÷åðôéâåøöéä éãðéú ëãé ìò÷åó àú áòééú äâøñàåú

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReact");        // ← 1. קודם CORS
// app.UseHttpsRedirection();     // ← 2. נטרלי את זה בפיתוח!
app.UseAuthorization();           // ← 3. אחר כך Authorization
app.MapControllers();             // ← 4. ולבסוף Controllers
app.UseAuthentication();
app.UseAuthorization();
app.Run();
// äâãøú äîéôåééí
// äâãøä îôåøùú ùîååãàú ùéîåù á-AutoMapper äî÷åøé
