using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task4.DTO_S;
using Task4.Models;

namespace Task4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public CategoriesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/Categories/5
    
        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPost("AddCategory")]
        public IActionResult addCategort([FromForm] categoriesDTO category)
        {
            if (category.CategoryImage != null)
            {
                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }

                var imagePath = Path.Combine(uploadsFolderPath, category.CategoryImage.FileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    category.CategoryImage.CopyToAsync(stream);
                }

            }

            var newCategory = new Category()
            {
                CategoryName = category.CategoryName,

                CategoryImage = category.CategoryImage.FileName,
            };

            _context.Categories.Add(newCategory);
            _context.SaveChanges();
            return Ok();
        }


        [HttpPut("EditCategory{id}")]
        public IActionResult editCategort(int id, [FromForm] categoriesDTO category)
        {

            if (category.CategoryImage != null)
            {
                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }

                var imagePath = Path.Combine(uploadsFolderPath, category.CategoryImage.FileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    category.CategoryImage.CopyToAsync(stream);
                }

            }

            var categoryUpdate = _context.Categories.Find(id);

            categoryUpdate.CategoryName = category.CategoryName;

            categoryUpdate.CategoryImage = category.CategoryImage.FileName;

            _context.Categories.Update(categoryUpdate);
            _context.SaveChanges();
            return Ok();
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }

    }
}
