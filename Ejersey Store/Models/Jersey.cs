using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jerseyShoppingCartMvcUI.Models
{
    [Table("jersey")]
    public class jersey
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string? jerseyName { get; set; }

        [Required]
        [MaxLength(40)]
       
    
        public double Price { get; set; }
        public string? Image { get; set; }
        [Required]
        public int categoryId { get; set; }
        public category category { get; set; }
        public List<OrderDetail> OrderDetail { get; set; }
        public List<CartDetail> CartDetail { get; set; }
        public Stock Stock { get; set; }

        [NotMapped]
        public string categoryName { get; set; }
        [NotMapped]
        public int Quantity { get; set; }


    }
}
