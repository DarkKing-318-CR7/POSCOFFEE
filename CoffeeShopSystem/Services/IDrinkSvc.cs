using CoffeeShopSystem.Models;
using CoffeeShopSystem.ViewModels;

namespace CoffeeShopSystem.Services
{
    public interface IDrinkSvc
    {
        Task<BillOfMaterials> AddOrUpdateRecipeIngredientAsync(AddDrinkIngredientDto dto);
        Task<bool> RemoveRecipeIngredientAsync(int drinkId, int ingredientId);
        Task<DrinkCostReportDto> CalculateDrinkCostAsync(int drinkId);
        Task<List<DrinkCostReportDto>> GetAllDrinksCostReportAsync();
    }
}
