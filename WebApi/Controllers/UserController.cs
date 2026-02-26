using Microsoft.AspNetCore.Mvc;
using Service.Services;
using Service.Dto.UserDto;
using Service.Interfaces;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService service;
        private readonly IMapper _mapper;
        public UserController(IUserService service,IMapper mapper )
        {
            this.service = service;
            this._mapper = mapper;
        }
        // GET: api/<LoginController>
        [HttpGet]
        public async Task<ActionResult<List<UserDtoo>>> Get([FromQuery] UserDtoo user)
        {
            // ה-await אומר: "תחכה שהמשימה תושלם בלי לתקוע את השרת"
            var users = await service.GetAll(user);
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDtoo>> GetById(int id)
        {
            var user = await service.GetById(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }
            return Ok(user);
        }

        // 3. הוספת משתמש חדש
        [HttpPost]
        public async Task<ActionResult<UserDtoo>> Post([FromBody] UserRegisterDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest("Invalid user data.");
            }
            
            var newUser = await service.Register(userDto);

            // מחזיר קוד 201 (Created) עם קישור לשליפת האובייקט שנוצר
            return CreatedAtAction(nameof(GetById), new { id = newUser.UserID }, newUser);
        }

        // 4. עדכון משתמש קיים
        [HttpPut("{id}")]
        public async Task<ActionResult<UserDtoo>> Put(int id, [FromBody] UserDtoo userDto)
        {
            var userUpdate = new UserUpdateDto
            {
                UserID = id,
                UserEmail = userDto.UserEmail,
                UserName = userDto.UserName,
                UserPhone = userDto.UserPhone,
            };
            var result = await service.Update(id, userUpdate);
            if (result== null) return NotFound();
            return Ok(result);// קוד 204 - הצלחתי לעדכן ואין לי מה להחזיר
        }


        [HttpPut("change-password/{id}")]
        public async Task<ActionResult<UserDtoo>> Put(int id, [FromBody] UserChangePasswordDto user)
        {
            var newUser = new UserChangePasswordDto
            {
                UserID = id,
                UserPassword = user.UserPassword,
                UserPasswordNew = user.UserPasswordNew
            };
            var result = await service.ChangePassword(newUser);
            if (result == null) return NotFound();
            return Ok(result);// קוד 204 - הצלחתי לעדכן ואין לי מה להחזיר
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDtoo>> Login([FromBody] UserLoginDto user)
        {

            var resultUser = await service.Login(user);
            if (resultUser == null)
                return NotFound("User not found or incorrect password.");
            return Ok(resultUser);
        }
      
    }
}

