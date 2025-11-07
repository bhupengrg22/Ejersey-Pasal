using System.ComponentModel.DataAnnotations.Schema;

namespace jerseyShoppingCartMvcUI.Models
{
    [Table("Stock")]
    public class Stock
    {
        public int Id { get; set; }
        public int jerseyId { get; set; }
        public int Quantity { get; set; }

        public jersey? jersey { get; set; }
    }
}
