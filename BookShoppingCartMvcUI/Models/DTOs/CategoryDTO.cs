using System.ComponentModel.DataAnnotations;

namespace jerseyShoppingCartMvcUI.Models.DTOs
{
    public class categoryDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string categoryName { get; set; }
    }
}
