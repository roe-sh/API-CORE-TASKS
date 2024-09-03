using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asp.netTaskNew.DTOs
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductResponseDTO : ControllerBase
    {
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
