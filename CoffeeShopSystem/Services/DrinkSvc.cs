using CoffeeShopSystem.Models;
using CoffeeShopSystem.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShopSystem.Services
{
    public class DrinkSvc : IDrinkSvc
    {
        private readonly MyDbContext _context;

        public DrinkSvc(MyDbContext context)
        {
            _context = context;
        }

        public async Task<BillOfMaterials> AddOrUpdateRecipeIngredientAsync(AddDrinkIngredientDto dto)
        {
            if (dto.Quantity <= 0)
            {
                throw new ArgumentException("Số lượng nguyên liệu trong công thức phải lớn hơn 0.");
            }

            var drink = await _context.Drinks.FindAsync(dto.DrinkId);
            if (drink == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy đồ uống với ID = {dto.DrinkId}.");
            }

            var ingredient = await _context.Ingredients.FindAsync(dto.IngredientId);
            if (ingredient == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy nguyên liệu với ID = {dto.IngredientId}.");
            }

            // Kiểm tra xem nguyên liệu này đã có trong công thức của món chưa
            var existing = await _context.BillOfMaterials
                .FirstOrDefaultAsync(di => di.DrinkId == dto.DrinkId && di.IngredientId == dto.IngredientId);

            if (existing != null)
            {
                // Cập nhật lại số lượng
                existing.Quantity = dto.Quantity;
                await _context.SaveChangesAsync();
                return existing;
            }

            var billOfMaterial = new BillOfMaterials
            {
                DrinkId = dto.DrinkId,
                IngredientId = dto.IngredientId,
                Quantity = dto.Quantity
            };

            await _context.BillOfMaterials.AddAsync(billOfMaterial);
            await _context.SaveChangesAsync();

            return billOfMaterial;
        }

        public async Task<bool> RemoveRecipeIngredientAsync(int drinkId, int ingredientId)
        {
            var item = await _context.BillOfMaterials
                .FirstOrDefaultAsync(di => di.DrinkId == drinkId && di.IngredientId == ingredientId);

            if (item == null)
            {
                throw new KeyNotFoundException("Nguyên liệu không tồn tại trong công thức của món này.");
            }

            _context.BillOfMaterials.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<DrinkCostReportDto> CalculateDrinkCostAsync(int drinkId)
        {
            var drink = await _context.Drinks
                .Include(d => d.BillOfMaterials!)
                .ThenInclude(di => di.Ingredient)
                .FirstOrDefaultAsync(d => d.DrinkId == drinkId);

            if (drink == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy đồ uống với ID = {drinkId}.");
            }

            var report = new DrinkCostReportDto
            {
                DrinkId = drink.DrinkId,
                DrinkName = drink.DrinkName,
                SellingPrice = drink.Price
            };

            decimal totalCost = 0;

            if (drink.BillOfMaterials != null && drink.BillOfMaterials.Any())
            {
                foreach (var di in drink.BillOfMaterials)
                {
                    var avePrice = di.Ingredient?.AvePrice ?? 0;
                    var itemCost = di.Quantity * avePrice;
                    totalCost += itemCost;

                    report.Ingredients.Add(new IngredientCostDetailDto
                    {
                        IngredientId = di.IngredientId,
                        IngredientName = di.Ingredient?.IngredientName ?? "Không xác định",
                        IngredientUnit = di.Ingredient?.IngredientUnit,
                        Quantity = di.Quantity,
                        AvePrice = avePrice,
                        TotalCost = Math.Round(itemCost, 2)
                    });
                }
            }

            report.TotalCost = Math.Round(totalCost, 2);

            if (drink.Price.HasValue)
            {
                var profit = drink.Price.Value - totalCost;
                report.Profit = Math.Round(profit, 2);

                if (drink.Price.Value > 0)
                {
                    report.MarginPercent = Math.Round((profit / drink.Price.Value) * 100, 2);
                }
                else
                {
                    report.MarginPercent = 0;
                }
            }

            return report;
        }

        public async Task<List<DrinkCostReportDto>> GetAllDrinksCostReportAsync()
        {
            var drinks = await _context.Drinks
                .Include(d => d.BillOfMaterials!)
                .ThenInclude(di => di.Ingredient)
                .ToListAsync();

            var reports = new List<DrinkCostReportDto>();

            foreach (var drink in drinks)
            {
                var report = new DrinkCostReportDto
                {
                    DrinkId = drink.DrinkId,
                    DrinkName = drink.DrinkName,
                    SellingPrice = drink.Price
                };

                decimal totalCost = 0;

                if (drink.BillOfMaterials != null && drink.BillOfMaterials.Any())
                {
                    foreach (var di in drink.BillOfMaterials)
                    {
                        var avePrice = di.Ingredient?.AvePrice ?? 0;
                        var itemCost = di.Quantity * avePrice;
                        totalCost += itemCost;

                        report.Ingredients.Add(new IngredientCostDetailDto
                        {
                            IngredientId = di.IngredientId,
                            IngredientName = di.Ingredient?.IngredientName ?? "Không xác định",
                            IngredientUnit = di.Ingredient?.IngredientUnit,
                            Quantity = di.Quantity,
                            AvePrice = avePrice,
                            TotalCost = Math.Round(itemCost, 2)
                        });
                    }
                }

                report.TotalCost = Math.Round(totalCost, 2);

                if (drink.Price.HasValue)
                {
                    var profit = drink.Price.Value - totalCost;
                    report.Profit = Math.Round(profit, 2);

                    if (drink.Price.Value > 0)
                    {
                        report.MarginPercent = Math.Round((profit / drink.Price.Value) * 100, 2);
                    }
                    else
                    {
                        report.MarginPercent = 0;
                    }
                }

                reports.Add(report);
            }

            return reports;
        }
    }
}
