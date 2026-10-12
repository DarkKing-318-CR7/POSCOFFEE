using CoffeeShopSystem.Models;
using CoffeeShopSystem.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShopSystem.Services
{
    public class IngredientSvc : IIngredientSvc
    {
        private MyDbContext _context;
        public IngredientSvc(MyDbContext context)
        {
            _context = context;
        }

        public async Task<Ingredient> AddIngredient(CreateIngredientDto ingredientDto)
        {
            if(string.IsNullOrWhiteSpace(ingredientDto.Name))
            {
                throw new ArgumentException("Tên nguyên liệu không được để trống");
            }
            var ingredient = new Ingredient
            {
                IngredientName = ingredientDto.Name,
                IngredientUnit = ingredientDto.IngredientUnit,
                Inventory = 0,
                AvePrice = 0
            };
            await _context.Ingredients.AddAsync(ingredient);
            await _context.SaveChangesAsync();

            return ingredient;

        }

        public async Task<StockEntry> AddStockEntry(CreateStockEntryDto stockEntryDto)
        {
            if(stockEntryDto.Quantity <= 0)
            {
                throw new ArgumentException("Số lượng nhập phải lớn hơn 0");
            }
            if (stockEntryDto.Price <= 0)
            {
                throw new ArgumentException("Giá nhập phải lớn hơn 0");
            }
            //1. tim nguyên liệu theo can nhap kho
            var ingredient =await _context.Ingredients.FindAsync(stockEntryDto.IngredientId);
            if (ingredient == null)
            {
                throw new ArgumentException("Nguyên liệu không tồn tại");
            }
            //2 . tạo stock entry
            var stockEntry = new StockEntry
            {
                IngredientId = stockEntryDto.IngredientId,
                Quantity = stockEntryDto.Quantity,
                Price = stockEntryDto.Price,
                Date = DateTimeOffset.UtcNow,
                BranchId = stockEntryDto.Branch
            };
            //3. cập nhật tồn kho và giá trung bình
            decimal oldTotalValue= ingredient.Inventory * ingredient.AvePrice;
            decimal newEntryValue = stockEntryDto.Quantity * stockEntryDto.Price;
            decimal totalNewQuantity = ingredient.Inventory + stockEntryDto.Quantity;

            if (totalNewQuantity > 0)
            {
                ingredient.AvePrice=(oldTotalValue+newEntryValue)/ totalNewQuantity;
            }
            //4 cong don so luong ton kho
            ingredient.Inventory += stockEntryDto.Quantity;

            //5. lưu vào csdl
            await _context.StockEntries.AddAsync(stockEntry);
            await _context.SaveChangesAsync();

            return stockEntry;
        }

        public async Task<Ingredient> DeleteIngredient(int ingredientId)
        {

            var ingredient = await _context.Ingredients.FindAsync(ingredientId);
            if (ingredient == null)
            {
                throw new ArgumentException("Nguyên liệu không tồn tại");
            }
            bool hasStockEntry = await _context.StockEntries.AnyAsync(s => s.IngredientId == ingredientId);
            if (hasStockEntry)
            {
                throw new InvalidOperationException("Không thể xóa nguyên liệu đã có lịch sử nhập kho");
            }

            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();

            return ingredient;
        }

        public async Task<StockEntry> DeleteStockEntry(int stockEntryId)
        {
            var stockEntry = await _context.StockEntries.FindAsync(stockEntryId);
            if (stockEntry == null)
            {
                throw new ArgumentException("Nhập kho không tồn tại");
            }
            var ingredient = await _context.Ingredients.FindAsync(stockEntry.IngredientId);
            if (ingredient == null)
            {
                throw new ArgumentException("Nguyên liệu không tồn tại");
            }

            if (ingredient.Inventory < stockEntry.Quantity)
            {
                throw new InvalidOperationException($"Không thể xóa phiếu vì tồn kho hiện tại({ingredient.Inventory}) nhỏ hơn số lượng trên phiếu({stockEntry.Quantity}).");
            }

            // cập nhật tồn kho và giá trung bìnH
            decimal oldTotalValue = ingredient.Inventory * ingredient.AvePrice;
            decimal newTotalValue = oldTotalValue - (stockEntry.Quantity * stockEntry.Price);
            decimal newInventory = ingredient.Inventory - stockEntry.Quantity;

            if (newInventory > 0)
            {
                ingredient.AvePrice = newTotalValue / newInventory;
            }
            else
            {
                ingredient.AvePrice = 0;
            }

            ingredient.Inventory = newInventory;

            _context.StockEntries.Remove(stockEntry);
            await _context.SaveChangesAsync();

            return stockEntry;
        }
        public async Task<Ingredient> GetIngredientById(int ingredientId)
        {
            var ingredient = await _context.Ingredients.FindAsync(ingredientId);
            if (ingredient == null)
            {
                throw new ArgumentException("Nguyên liệu không tồn tại");
            }
            return ingredient;
        }

    }
}
