namespace Asp.netTaskNew.DTOs
{
    namespace Task4.DTO
    {
        public class ProductRequestDTO
        {
            public string?ProductName { get; set; }

            public string? Description { get; set; }

            public int? Price { get; set; }

            public int? CategoryId { get; set; }

            public IFormFile? ProductImage { get; set; }
        }
    }
}
