using Microsoft.AspNetCore.Mvc;
using Service.Dto.EventDto;
using Service.Dto.EventTypeDto;
using Service.Dto.UserDto;
using Service.Interfaces;
using Service.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventTypeController : ControllerBase
    {
        private readonly IGetService<EventTypeDtoo> _eventTypeService;

        public EventTypeController(IGetService<EventTypeDtoo> eventService)
        {
            _eventTypeService = eventService;
        }

        [HttpGet]
        public async Task<ActionResult<List<EventTypeDtoo>>> Get()
        {
            var events = await _eventTypeService.GetAll();
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventTypeDtoo>> GetById(int id)
        {
            var eventItem = await _eventTypeService.GetById(id);
            if (eventItem == null)
                return NotFound($"Event with ID {id} not found.");

            return Ok(eventItem);
        }

    }
}
