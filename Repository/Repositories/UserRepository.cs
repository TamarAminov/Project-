using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly IContext _context;
        public UserRepository(IContext context)
        {
            this._context = context;
        }
        public async  Task<User> AddItem(User item)
        {
             _context.Users.AddAsync(item);
            await _context.save();
            return item;
        }

        public async Task DeleteItem(int id)
        {
            var item = await GetById(id);
           _context.Users.Remove(item);
           await _context.save();
        }

        public async Task<List<User>> GetAll()
        {
           return _context.Users.ToList();
        }

        public async Task<List<User>> GetAll(int id)
        {
            return _context.Users.Where(x => x.UserID == id).ToList();
        }
        // פונקציה שבודקת אם הסיסמה נכונה (משתמשים בה בשינוי סיסמה או התחברות)
        public bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, hashedPassword);
        }
        public async Task<User> Login(string email,string pass)
        {
            // 1. מחפשים את המשתמש רק לפי האימייל (זה SQL יודע לעשות)
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserEmail == email);

            // 2. אם לא נמצא משתמש - מחזירים null
            if (user == null)
            {
                return null;
            }

            // 3. עכשיו, מחוץ לשאילתת ה-SQL, בודקים את הסיסמה בעזרת הספרייה
            // הערה: אם לא השתמשת עדיין ב-BCrypt, זה הזמן. 
            // אם הסיסמה שלך עדיין לא מוצפנת, פשוט תשני את זה לבדיקת string רגילה.
            bool isPasswordValid = VerifyPassword(pass, user.UserPasswordHash);

            if (isPasswordValid)
            {
                return user;
            }

            return null;
            //return await _context.Users.Where(x => x.UserEmail == email && VerifyPassword(pass, x.UserPasswordHash)).FirstOrDefaultAsync();
        }

        public async Task<User> GetById(int id)
        {
                return await _context.Users.FirstOrDefaultAsync(x => x.UserID == id);
        }

        public async Task UpdateItem(int id, User item)
        {
            var user =await GetById(id);
            if (user != null)
            {
                user.UserPhone=item.UserPhone;
                user.UserName=item.UserName;
                user.UserPasswordHash=item.UserPasswordHash;
                user.UserEmail=item.UserEmail;
                user.UserEvents=item.UserEvents;
                _context.Users.Update(user);
            }
            else
            {
                Console.WriteLine($"User with ID {id} was not found!");
                return;
            }
            await _context.save();
        }
        public async Task<User?> GetByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == email);
        }
    }
}
