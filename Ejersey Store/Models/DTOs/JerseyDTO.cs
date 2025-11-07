using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace jerseyShoppingCartMvcUI.Models.DTOs;
public class jerseyDTO
{
    public int Id { get; set; }

    [Required]
    [MaxLength(40)]
    public string? jerseyName { get; set; }

    [Required]
   
 
  
    public double Price { get; set; }
    public string? Image { get; set; }
    [Required]
    public int categoryId { get; set; }
    public IFormFile? ImageFile { get; set; }
    public IEnumerable<SelectListItem>? categoryList { get; set; }
}
