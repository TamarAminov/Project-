using AutoMapper;
using Repository.Entities;
using Repository.Interfaces;
using Service.Dto.EventDto;
using Service.Dto.EventTypeDto;
using Service.Dto.UserDto;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class EventTypeService : IGetService<EventTypeDtoo>
    {
        private readonly IRepository<EventType> repository;
        private readonly IMapper mapper;
        public EventTypeService(IRepository<EventType> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<List<EventTypeDtoo>> GetAll()
        {
            var events = await repository.GetAll();
            return mapper.Map<List<EventTypeDtoo>>(events);
        }

        public async Task<EventTypeDtoo> GetById(int id)
        {
            var eventItem = await repository.GetById(id);
            if (eventItem == null)
            {
                return null; // או לזרוק Exception לפי החלטה
            }
            return mapper.Map<EventTypeDtoo>(eventItem);

        }
    }
}
