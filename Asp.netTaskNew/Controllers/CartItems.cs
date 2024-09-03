using Asp.netTaskNew.DTOs;
using Asp.netTaskNew.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asp.netTaskNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItems : ControllerBase
    {
        private readonly MyDbContext _db;

        public CartItems(MyDbContext db)
        {
            _db = db;
        }



        [HttpGet("{CID}")]
        public IActionResult GetCartItems(int CID)
        {
            var items = _db.CartItems.Where(i => i.CartId == CID).Select(h => new CartItemResponseDTO
            {
                CartItemId = h.CartItemId,
                CartId = h.CartId,
                Quantity = h.Quantity,
                Product = new ProductResponseDTO
                {
                    ProductName = h.Product.ProductName,
                    Price = h.Product.Price,

                }
            });

            return Ok(items);

        }

        [HttpPut("Edit/{id}")]
        public IActionResult Edit([FromBody] cartitemputrequestDTO quantity, int id)
        {
            var item = _db.CartItems.Find(id);
            if (item == null) { return BadRequest(); };
            item.Quantity = quantity.Quantity;
            _db.CartItems.Update(item);
            _db.SaveChanges();
            return Ok(item);
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var item = _db.CartItems.Find(id);
            _db.CartItems.Remove(item);
            _db.SaveChanges();
            return Ok(item);
        }
    }
}
