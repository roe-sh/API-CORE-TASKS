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
    public class ProductsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ProductsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

       

        [HttpPost("AddProduct")]
        public IActionResult addProduct([FromForm] productsDTO product)
        {
            if (product.ProductImage != null)
            {
                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }

                var imagePath = Path.Combine(uploadsFolderPath, product.ProductImage.FileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    product.ProductImage.CopyToAsync(stream);
                }

            }

            var newProduct = new Product()
            {
                ProductName = product.ProductName,
                Description = product.Description,

                CategoryId = product.CategoryId,
                ProductImage = product.ProductImage.FileName
            };

            _context.Products.Add(newProduct);
            _context.SaveChanges();
            return Ok();
        }


        [HttpPut("EditProduct{id}")]
        public IActionResult editProduct(int id, [FromForm] productsDTO product)
        {

            if (product.ProductImage != null)
            {
                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }

                var imagePath = Path.Combine(uploadsFolderPath, product.ProductImage.FileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    product.ProductImage.CopyToAsync(stream);
                }

            }

            var productUpdate = _context.Products.Find(id);

            productUpdate.ProductName = product.ProductName;
            productUpdate.Description = product.Description;

            productUpdate.CategoryId = product.CategoryId;
            productUpdate.ProductImage = product.ProductImage.FileName;

            _context.Products.Update(productUpdate);
            _context.SaveChanges();
            return Ok();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
