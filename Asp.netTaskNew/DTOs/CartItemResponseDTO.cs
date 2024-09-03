using Asp.netTaskNew.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asp.netTaskNew.DTOs
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemResponseDTO : ControllerBase
    {
        public int CartItemId { get; set; }

        public int? CartId { get; set; }

        public int Quantity { get; set; }

        public virtual ProductResponseDTO Product { get; set; } 
    }
}
