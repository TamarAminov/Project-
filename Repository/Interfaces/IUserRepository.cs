using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUserRepository
    {

        Task<User> Login(string email, string password);
        Task<User> AddItem(User item); 
        Task DeleteItem(int id);
        Task< List<User>> GetAll();
        Task<List<User>> GetAll(int id);
        Task<User> GetById(int id);
        Task UpdateItem(int id, User item);
        Task<User?> GetByEmail(string email);
    }
}
