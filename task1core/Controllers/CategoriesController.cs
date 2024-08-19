using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using task1core.Models;

namespace task1core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly MyDbContext _db;

        public CategoriesController(MyDbContext context)
        {
            _db = context;
        }

       
        [HttpGet("getAllCategories")]
        public IActionResult GetAllCategories()
        {
            var categories = _db.Categories.ToList();
            return Ok(categories);
        }

        [HttpGet("GetCategoryById/{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = _db.Categories.Find(id);
           
            return Ok(category);
        }
    }
}

