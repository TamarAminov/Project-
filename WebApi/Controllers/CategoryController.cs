using Microsoft.AspNetCore.Mvc;
using Service.Dto.CategoryDto;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IGetService<CategoryDtoo> _categoryService;

        public CategoryController(IGetService<CategoryDtoo> categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDtoo>>> Get()
        {
            var categories = await _categoryService.GetAll();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDtoo>> GetById(int id)
        {
            var categoryItem = await _categoryService.GetById(id);
            if (categoryItem == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }

            return Ok(categoryItem);
        }
    }
}
