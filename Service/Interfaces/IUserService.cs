using Repository.Interfaces;
using Service.Dto.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IUserService 
    {
  
        Task<List<UserDtoo>> GetAll(UserDtoo user);

        // מבצע רישום ומחזיר את המשתמש שנוצר
        Task<UserDtoo> Register(UserRegisterDto registerDto);

        // מעדכן פרטים ומחזיר את המשתמש המעודכן
        Task<UserDtoo> Update(int id,UserUpdateDto updateDto);

        // משנה סיסמה ומחזיר אמת או שקר
        Task<bool> ChangePassword(UserChangePasswordDto passwordDto);
        Task<UserDtoo> GetById(int id);
        Task<UserDtoo> Login(UserLoginDto user);
        //UserChangePasswordDto GetById(int id);

    }
}
