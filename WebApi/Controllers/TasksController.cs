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
using Service.Services;

[Route("api/[controller]")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly IService<TasksDtoo> _tasksService;

    public TasksController(IService<TasksDtoo> tasksService)
    {
        _tasksService = tasksService;
    }

    [HttpGet]
    public async Task<ActionResult<List<TasksDtoo>>> Get(int id)
    {
        var tasks = await _tasksService.GetAll(id);
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
}