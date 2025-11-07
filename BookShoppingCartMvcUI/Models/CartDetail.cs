using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace jerseyShoppingCartMvcUI.Models
{
    [Table("CartDetail")]
    public class CartDetail
    {
        public int Id { get; set; }
        [Required]
        public int ShoppingCartId { get; set; }
        [Required]
        public int jerseyId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double UnitPrice { get; set; }   
        public jersey jersey { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }
}
