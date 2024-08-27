using Task4.Models;

namespace Task4.DTO_S
{
    public class productsDTO
    {
        public string? ProductName { get; set; }

        public string? Description { get; set; }

        public int? Price { get; set; }

        public int? CategoryId { get; set; }

        public IFormFile? ProductImage { get; set; }
    }
}
