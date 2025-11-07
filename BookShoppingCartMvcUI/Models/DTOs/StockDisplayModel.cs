namespace jerseyShoppingCartMvcUI.Models.DTOs
{
    public class StockDisplayModel
    {
        public int Id { get; set; }
        public int jerseyId { get; set; }
        public int Quantity { get; set; }
        public string? jerseyName { get; set; }
    }
}
