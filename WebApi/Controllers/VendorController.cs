//using Microsoft.AspNetCore.Mvc;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace WebApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class VendorController : ControllerBase
//    {
//        // GET: api/<VendorController>
//        [HttpGet]
//        public IEnumerable<string> Get()
//        {
//            return new string[] { "value1", "value2" };
//        }

//        // GET api/<VendorController>/5
//        [HttpGet("{id}")]
//        public string Get(int id)
//        {
//            return "value";
//        }

//        // POST api/<VendorController>
//        [HttpPost]
//        public void Post([FromBody] string value)
//        {
//        }

//        // PUT api/<VendorController>/5
//        [HttpPut("{id}")]
//        public void Put(int id, [FromBody] string value)
//        {
//        }

//        // DELETE api/<VendorController>/5
//        [HttpDelete("{id}")]
//        public void Delete(int id)
//        {
//        }
//    }
//}
using Microsoft.AspNetCore.Mvc;
using Service.Dto.UserDto;
using Service.Dto.VendorDto;
using Service.Interfaces;
using Service.Services;

[Route("api/[controller]")]
[ApiController]
public class VendorController : ControllerBase
{
    private readonly IVendorService _vendorService;

    public VendorController(IVendorService vendorService)
    {
        _vendorService = vendorService;
    }

    [HttpGet]
    public async Task<ActionResult<List<VendorDtoo>>> Get(int id)
    {
        return Ok(await _vendorService.GetAll(id));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VendorDtoo>> GetById(int id)
    {
        var vendor = await _vendorService.GetById(id);
        if (vendor == null) return NotFound();
        return Ok(vendor);
    }

    [HttpPost]
    public async Task<ActionResult<VendorDtoo>> Post([FromBody] VendorDtoo vendorDto)
    {
        var created = await _vendorService.AddItem(vendorDto);
        return CreatedAtAction(nameof(GetById), new { id = created.VendorID }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<VendorDtoo>> Put(int id, [FromBody] VendorDtoo vendorDto)
    {
        var updated = await _vendorService.UpdateItem(id, vendorDto);
        if (updated == null) return NotFound();
        return Ok(updated);
    }
}