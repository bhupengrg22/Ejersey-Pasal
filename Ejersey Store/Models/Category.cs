using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace jerseyShoppingCartMvcUI.Models
{
    [Table("category")]
    public class category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string categoryName { get; set; }
        public List<jersey> jerseys { get; set; }
    }
}
