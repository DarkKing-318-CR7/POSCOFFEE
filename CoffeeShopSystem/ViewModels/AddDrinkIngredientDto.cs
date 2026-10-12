namespace CoffeeShopSystem.ViewModels
{
    public class AddDrinkIngredientDto
    {
        public int DrinkId { get; set; }
        public int IngredientId { get; set; }
        public decimal Quantity { get; set; } // Lượng nguyên liệu cần cho 1 ly (gam, ml...)
    }
}
