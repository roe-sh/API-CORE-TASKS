
namespace Asp.netTaskNew.DTOs
{
   
    public class CartItemResponseDTO 
    {
        public int CartItemId { get; set; }

        public int? CartId { get; set; }

        public int Quantity { get; set; }

        public virtual ProductResponseDTO Product { get; set; } 
    }
}
