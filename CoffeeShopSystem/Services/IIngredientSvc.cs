using CoffeeShopSystem.Models;
using CoffeeShopSystem.ViewModels;

namespace CoffeeShopSystem.Services
{
    public interface IIngredientSvc
    {
        public Task<StockEntry>AddStockEntry(CreateStockEntryDto stockEntryDto);
        public Task<Ingredient> AddIngredient(CreateIngredientDto ingredientDto);
        public Task<Ingredient> DeleteIngredient(int ingredientId);
        public Task<StockEntry> DeleteStockEntry(int stockEntryId);
        public Task<Ingredient> GetIngredientById(int ingredientId);
        

    }
}
