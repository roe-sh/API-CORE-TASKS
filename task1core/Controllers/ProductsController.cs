using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using task1core.Models;
namespace task1core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly MyDbContext _db;

        public ProductsController(MyDbContext context)
        {
            _db = context;
        }

       
        [HttpGet("getAllProducts")]
        public IActionResult GetAllProducts()
        {
            var products = _db.Products.ToList();
            return Ok(products);
        }

        // GET: api/Products/{id}
        [HttpGet("GetProductById/{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _db.Products.Find(id);
            
            return Ok(product);
        }
    }
}
   