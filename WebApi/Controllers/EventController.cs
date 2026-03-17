using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Service.Dto.EventDto;
using Service.Dto.EventTypeDto;
using Service.Dto.UserDto;
using Service.Services;

[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly IService<EventDtoo> _eventService;

    public EventController(IService<EventDtoo> eventService)
    {
        _eventService = eventService;

    }

    [HttpGet]
    public async Task<ActionResult<List<EventDtoo>>> Get(int id)
    {
        var events = await _eventService.GetAll(id);
        return Ok(events);
    }
   

    [HttpGet("{id}")]
    public async Task<ActionResult<EventDtoo>> GetById(int id)
    {
        var eventItem = await _eventService.GetById(id);
        if (eventItem == null)
            return NotFound($"Event with ID {id} not found.");

        return Ok(eventItem);
    }
    
    [HttpPost]
    public async Task<ActionResult<EventDtoo>> Post([FromBody] EventCreateDto eventDto)
    {
        if (eventDto == null) return BadRequest("Invalid event data.");
        try
        {
            var newEventDtoo = new EventDtoo
            {
                EventID = 0,
                EventName = eventDto.EventName,
                EventDate = eventDto.EventDate,
                UserID = eventDto.UserID,
                EventTypeID = eventDto.EventTypeID,
                TotalBudget = eventDto.TotalBudget,
                GuestCount = eventDto.GuestCount
            };

            var newEvent = await _eventService.AddItem(newEventDtoo);
            return CreatedAtAction(nameof(GetById), new { id = newEvent.EventID }, newEvent);
        }
        catch (DomainException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<EventDtoo>> Put(int id, [FromBody] EventDtoo eventDto)
    {
        try
        {
            var updatedEvent = await _eventService.UpdateItem(id, eventDto);
            if (updatedEvent == null)
                return NotFound($"Event with ID {id} not found for update.");
            return Ok(updatedEvent);
        }
        catch (DomainException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _eventService.DeleteItem(id);
        return NoContent(); // 204
    }
}
