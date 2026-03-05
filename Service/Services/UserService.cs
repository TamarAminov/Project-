using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Repository.Entities;
using Repository.Interfaces;
using Service.Dto.UserDto;
using Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service.Services
{
    public class UserService : IUserService
    {
        //private readonly IRepository<User> repository;
        private readonly IUserRepository repository;

        private readonly IMapper mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

    
        // 1. שליפת כל המשתמשים
        public async Task<List<UserDtoo>> GetAll(UserDtoo user)
        {
            if (user.Role == User.EnumRole.Manager)
            {
                // מביאים את כל הישויות מהמסד
                var users =await repository.GetAll();
                // הופכים את רשימת הישויות לרשימת DTOs ומחזירים
                return mapper.Map<List<UserDtoo>>(users);
            }
            return null;
        }

        public async Task<UserDtoo> Login(UserLoginDto user)
        {
            //var pass = HashPassword(user.UserPassword);
            //Console.WriteLine(pass);
            var currentUser = await repository.Login(user.UserEmail, user.UserPassword); 
            return mapper.Map<UserDtoo>(currentUser);
        }

        // 2. רישום משתמש חדש
        public async Task<UserDtoo> Register(UserRegisterDto registerDto)
        {
            // הופכים את ה-DTO שקיבלנו לישות User
            var userEntity = mapper.Map<User>(registerDto);
            userEntity.Role = User.EnumRole.Client;
            userEntity.UserPasswordHash = HashPassword(registerDto.UserPassword);
            userEntity.UserEvents = null;
            // שומרים במסד הנתונים דרך ה-Repository
            var result =await repository.AddItem(userEntity);
            // מחזירים את התוצאה כ-DTO למסך
            return mapper.Map<UserDtoo>(result);
        }

        // 3. עדכון פרטים (שם, טלפון וכו')
        public async Task<UserDtoo> Update(int id, UserUpdateDto updateDto)
        {
            // מוצאים את המשתמש הקיים לפי ה-ID
            var existingUser =await repository.GetById(updateDto.UserID);
            // מעדכנים את הפרטים מה-DTO לתוך הישות הקיימת
            mapper.Map(updateDto, existingUser);

           // שומרים את השינויים במסד
            await repository.UpdateItem(existingUser.UserID, existingUser);

            // מחזירים את המשתמש המעודכן כ-DTO
            return mapper.Map<UserDtoo>(existingUser);
        }
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // פונקציה שבודקת אם הסיסמה נכונה (משתמשים בה בשינוי סיסמה או התחברות)
        public  bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, hashedPassword);
        }
        // 4. שינוי סיסמה
        public async Task<bool> ChangePassword(UserChangePasswordDto passwordDto)
        {
           var user =await repository.GetById(passwordDto.UserID);
                if (user == null) return false;

                // כאן הקסם: במקום להשוות עם ==, משתמשים בפונקציה שבודקת את ההצפנה
                // נניח שיש לך מחלקת עזר שנקראת PasswordHelper
                bool isPasswordCorrect = VerifyPassword(passwordDto.UserPassword, user.UserPasswordHash);

                if (isPasswordCorrect)
                {
                    // לפני ששומרים את הסיסמה החדשה - חייבים להצפין גם אותה!
                    user.UserPasswordHash = HashPassword(passwordDto.UserPasswordNew);
                   await repository.UpdateItem(user.UserID, user);
                    return true;
                }
                return false;
        }
        
        public async Task<UserDtoo> GetById(int id)
        {
            // 1. מבקשים מה-Repository למצוא את המשתמש לפי ה-ID שלו במסד הנתונים
            var userEntity =await repository.GetById(id);

            // הגנה: אם המשתמש לא קיים, נחזיר null כדי שלא תקרוס האפליקציה
            if (userEntity == null) return null;

            // 2. הופכים את הישות שמצאנו ל-DTO (האריזה היפה שכוללת את האירועים)
            return mapper.Map<UserDtoo>(userEntity);
        }


        //public UserChangePasswordDto GetById(int id)
        //{
        //    // 1. מבקשים מה-Repository למצוא את המשתמש לפי ה-ID שלו במסד הנתונים
        //    var userEntity = repository.GetById(id);

        //    // הגנה: אם המשתמש לא קיים, נחזיר null כדי שלא תקרוס האפליקציה
        //    if (userEntity == null) return null;

        //    // 2. הופכים את הישות שמצאנו ל-DTO (האריזה היפה שכוללת את האירועים)
        //    return mapper.Map<UserChangePasswordDto>(userEntity);
        //}
        public string GenerateToken(UserDtoo user)
        {
            var claims = new[] {
        new Claim("userID", user.UserID.ToString()),
        new Claim("email", user.UserEmail)
    };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("wsxcde345*&^SDFG478euwryiowew8e85683FQWTRTW-34908;29-09RR2"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(expires: DateTime.Now.AddDays(7), signingCredentials: creds, claims: claims);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }


}