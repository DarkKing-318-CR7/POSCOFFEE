namespace CoffeeShopSystem.ViewModels
{
    public class CreateStockEntryDto
    {
        public int IngredientId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public int? Branch { get; set; }
    }
}
