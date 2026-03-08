using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IEventRepository: IRepository<Event>
    {
        //public Task<Event> BudgetsDefault(int id);
    }
}
