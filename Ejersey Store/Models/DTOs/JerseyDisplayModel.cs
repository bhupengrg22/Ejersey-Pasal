namespace jerseyShoppingCartMvcUI.Models.DTOs
{
    public class jerseyDisplayModel
    {
        public IEnumerable<jersey> jerseys { get; set; }
        public IEnumerable<category> categorys { get; set; }
        public string STerm { get; set; } = "";
        public int categoryId { get; set; } = 0;
    }
}
