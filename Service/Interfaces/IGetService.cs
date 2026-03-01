using Service.Dto.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IGetService<T>
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);

    }
}
