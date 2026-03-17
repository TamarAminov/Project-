using Microsoft.AspNetCore.Mvc;
using Service.Dto.VendorAttributeDto;
using Service.Dto.UserDto;
using Service.Interfaces;
using Service.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorAttributeController : ControllerBase
    {
        //private readonly Service.Services.VendorAttributeService _attributeService;
        private readonly IVendorAttributeService _attributeService;

        public VendorAttributeController(IVendorAttributeService attributeService)
        {
            _attributeService = attributeService;
        }

        // 1. שליפת כל המאפיינים (סינון לפי משתמש/מנהל בתוך ה-Service)
        [HttpGet]
        public async Task<ActionResult<List<VendorAttributeDtoo>>> Get(int id)
        {
            var attributes = await _attributeService.GetAll(id);
            return Ok(attributes);
        }

        // 2. שליפת מאפיין ספציפי לפי ID
        [HttpGet("{id}")]
        public async Task<ActionResult<VendorAttributeDtoo>> GetById(int id)
        {
            var attribute = await _attributeService.GetById(id);
            if (attribute == null)
            {
                return NotFound($"Vendor Attribute with ID {id} not found.");
            }
            return Ok(attribute);
        }

        // 3. הוספת מאפיין חדש לספק
        [HttpPost]
        public async Task<ActionResult<VendorAttributeDtoo>> Post([FromBody] VendorAttributeDtoo attributeDto)
        {
            try
            {
            if (attributeDto == null)
                        {
                            return BadRequest("Invalid attribute data.");
                        }

                        var newAttribute = await _attributeService.AddItem(attributeDto);

                        // מחזיר 201 עם נתיב לשליפה
                        return CreatedAtAction(nameof(GetById), new { id = newAttribute.VendorAttributeID }, newAttribute);
      
            }
            catch (DomainException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // 4. עדכון מאפיין קיים
        [HttpPut("{id}")]
        public async Task<ActionResult<VendorAttributeDtoo>> Put(int id, [FromBody] VendorAttributeDtoo attributeDto)
        {
            try
            {
                    if (attributeDto == null) return BadRequest();

                    var result = await _attributeService.UpdateItem(id, attributeDto);

                    if (result == null)
                    {
                        return NotFound($"Vendor Attribute with ID {id} not found for update.");
                    }

                    return Ok(result); // מחזיר את האובייקט המעודכן כפי שביקשת

            }
            catch (DomainException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // 5. מחיקת מאפיין
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existing = await _attributeService.GetById(id);
            if (existing == null) return NotFound();

            await _attributeService.DeleteItem(id);
            return NoContent(); // 204 Success
        }
        [HttpPost("bulk")]
        public async Task<IActionResult> GetBulkAttributes([FromBody] List<int> vendorIds)
        {
            var result = await _attributeService.GetByVendorIdsAsync(vendorIds);
            return Ok(result);
        }
    }
}