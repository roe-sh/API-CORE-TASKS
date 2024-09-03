using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Asp.netTaskNew.Models;
using Asp.netTaskNew.DTOs.Task4.DTO;

namespace Asp.netTaskNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly MyDbContext _db;
        public ProductsController(MyDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _db.Products.ToList();
            return Ok(products);
        }

        [HttpGet("{id:int:min(1)}")]
        public IActionResult GetById(int id)
        {
            var product = _db.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            _db.Products.Remove(product);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpGet("category/{categoryId:int}")]
        public IActionResult GetProductsByCategoryId(int categoryId)
        {
            var products = _db.Products.Where(p => p.CategoryId == categoryId).ToList();
            if (products == null || !products.Any())
            {
                return NotFound();
            }
            return Ok(products);
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromForm] ProductRequestDTO product)
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
                    await product.ProductImage.CopyToAsync(stream);
                }
            }

            var newProduct = new Product
            {
                ProductName = product.ProductName,
                Description = product.Description,
                CategoryId = product.CategoryId,
                ProductImage = product.ProductImage?.FileName
            };

            _db.Products.Add(newProduct);
            _db.SaveChanges();
            return Ok(newProduct);
        }
        
        [HttpPut("EditProduct/{id:int}")]
        public async Task<IActionResult> EditProduct(int id, [FromForm] ProductRequestDTO product)
        {
            var productUpdate = _db.Products.Find(id);
            if (productUpdate == null)
            {
                return NotFound();
            }

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
                    await product.ProductImage.CopyToAsync(stream);
                }
            }

            productUpdate.ProductName = product.ProductName;
            productUpdate.Description = product.Description;
            productUpdate.CategoryId = product.CategoryId;
            productUpdate.Price = Convert.ToDecimal(product.Price) ;
            productUpdate.ProductImage = product.ProductImage?.FileName;

            _db.Products.Update(productUpdate);
            _db.SaveChanges();
            return Ok(productUpdate);
        }
    }
}

