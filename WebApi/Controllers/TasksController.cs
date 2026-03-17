    //using Microsoft.AspNetCore.Mvc;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace WebApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class TasksController : ControllerBase
//    {
//        // GET: api/<TasksController>
//        [HttpGet]
//        public IEnumerable<string> Get()
//        {
//            return new string[] { "value1", "value2" };
//        }

//        // GET api/<TasksController>/5
//        [HttpGet("{id}")]
//        public string Get(int id)
//        {
//            return "value";
//        }

//        // POST api/<TasksController>
//        [HttpPost]
//        public void Post([FromBody] string value)
//        {
//        }

//        // PUT api/<TasksController>/5
//        [HttpPut("{id}")]
//        public void Put(int id, [FromBody] string value)
//        {
//        }

//        // DELETE api/<TasksController>/5
//        [HttpDelete("{id}")]
//        public void Delete(int id)
//        {
//        }
//    }
//}
using Microsoft.AspNetCore.Mvc;
using Service.Dto.TasksDto;
using Service.Dto.UserDto;
using Service.Interfaces;
using Service.Services;
using Service.Dto.TasksDto;

[Route("api/[controller]")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly ITasksService _tasksService;

    public TasksController(ITasksService tasksService)
    {
        _tasksService = tasksService;
    }
    // POST /api/tasks/generate  ← קריאה לפרוצדורה ליצירת משימות לספק
    [HttpPost("generate", Name = "GenerateTasks")]
    public async Task<ActionResult> GenerateTasks([FromBody] GenerateTasksDto dto)
    {
        try
        {


            await _tasksService.GenerateTasksForVendor(dto.EventID, dto.VendorID);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                message = ex.Message,
                inner = ex.InnerException?.Message
            });
        }
    }

        [HttpGet]
    public async Task<ActionResult<List<TasksDtoo>>> Get(int eventId)
    {
        var tasks = await _tasksService.GetAll(eventId);
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TasksDtoo>> GetById(int id)
    {
        var task = await _tasksService.GetById(id);
        if (task == null) return NotFound();
        return Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<TasksDtoo>> Post([FromBody] TasksDtoo taskDto)
    {
        try
        {
            if (taskDto == null) return BadRequest();

        var newTask = await _tasksService.AddItem(taskDto);
        return CreatedAtAction(nameof(GetById), new { id = newTask.TaskID }, newTask);
    
        }
        catch (DomainException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TasksDtoo>> Put(int id, [FromBody] TasksDtoo taskDto)
    {
        try
        {
        var result = await _tasksService.UpdateItem(id, taskDto);
        if (result == null) return NotFound();

        return Ok(result);
        }
        catch (DomainException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _tasksService.DeleteItem(id);
        return NoContent();
    }

    // PATCH /api/tasks/{id}/toggle  ← סימון משימה
    [HttpPatch("{id}/toggle")]
    public async Task<ActionResult> ToggleTask(int id, [FromBody] ToggleTaskDto dto)
    {
        var result = await _tasksService.ToggleTask(id, dto.IsCompleted);
        if (result == null) return NotFound();
        return Ok(result);
    }


}
