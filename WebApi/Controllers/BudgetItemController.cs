using Microsoft.AspNetCore.Mvc;
using Service.Dto.BudgetItemDto;
using Service.Dto.UserDto;
using Service.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetItemController : ControllerBase
    {

        private readonly IService<BudgetItemDtoo> _budgetItemService;

        public BudgetItemController(IService<BudgetItemDtoo> budgetItemService)
        {
            _budgetItemService = budgetItemService;
        }

        // GET: api/<BudgetItemController>
        // 1. שליפת כל הפריטים (לפי הרשאות משתמש)
        [HttpGet]
        public async Task<ActionResult<List<BudgetItemDtoo>>> GetAll([FromQuery] UserDtoo user)
        {
            var items = await _budgetItemService.GetAll(user);
            return Ok(items);
        }

        // GET api/<BudgetItemController>/5
        // 2. שליפת פריט לפי מזהה
        [HttpGet("{id}")]
        public async Task<ActionResult<BudgetItemDtoo>> GetById(int id)
        {
            var item = await _budgetItemService.GetById(id);
            if (item == null)
            {
                return NotFound($"BudgetItem with ID {id} not found.");
            }
            return Ok(item);
        }

        // POST api/<BudgetItemController>
        // 3. הוספת פריט תקציב חדש
        [HttpPost]
        public async Task<ActionResult<BudgetItemDtoo>> Post([FromBody] BudgetItemDtoo itemDto)
        {
            if (itemDto == null)
            {
                return BadRequest("Invalid data.");
            }

            var newItem = await _budgetItemService.AddItem(itemDto);

            // שימוש ב-CreatedAtAction כדי להחזיר קוד 201 ונתיב לשליפת האובייקט
            return CreatedAtAction(nameof(GetById), new { id = newItem.BudgetItemID }, newItem);
        }

        // PUT api/<BudgetItemController>/5
        // 4. עדכון פריט קיים
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] BudgetItemDtoo itemDto)
        {
            var existingItem = await _budgetItemService.GetById(id);
            if (existingItem == null)
            {
                return NotFound($"BudgetItem with ID {id} not found.");
            }

            await _budgetItemService.UpdateItem(id, itemDto);
            return NoContent(); // או Ok(itemDto) אם רוצים להחזיר את האובייקט המעודכן
        }
        // DELETE api/<BudgetItemController>/5
        // 5. מחיקת פריט
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existingItem = await _budgetItemService.GetById(id);
            if (existingItem == null)
            {
                return NotFound($"BudgetItem with ID {id} not found.");
            }

            await _budgetItemService.DeleteItem(id);
            return NoContent();
        }

        //using Microsoft.AspNetCore.Mvc;
        //using Service.Dto.BudgetItemDto;
        //using Service.Dto.UserDto;
        //using Service.Interfaces;
    }

}
