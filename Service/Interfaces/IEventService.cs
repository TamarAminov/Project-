using Repository.Entities;
using Service.Dto.EventDto;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IEventService: IService<EventDtoo>
    {
        public Task<EventDtoo> DevideBudgetsDefualt(int id);
    }
}
