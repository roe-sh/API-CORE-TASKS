using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asp.netTaskNew.DTOs
{
   
    public class ProductResponseDTO 
    {
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
