namespace CoffeeShopSystem.ViewModels
{
    public class IngredientCostDetailDto
    {
        public int IngredientId { get; set; }
        public string IngredientName { get; set; } = string.Empty;
        public string? IngredientUnit { get; set; }
        public decimal Quantity { get; set; } // Lượng dùng trong 1 ly
        public decimal AvePrice { get; set; } // Giá vốn bình quân của nguyên liệu
        public decimal TotalCost { get; set; } // Chi phí nguyên liệu này = Quantity * AvePrice
    }

    public class DrinkCostReportDto
    {
        public int DrinkId { get; set; }
        public string DrinkName { get; set; } = string.Empty;
        public decimal? SellingPrice { get; set; } // Giá bán niêm yết
        public decimal TotalCost { get; set; } // Tổng chi phí nguyên liệu để làm ra 1 ly
        public decimal? Profit { get; set; } // Lợi nhuận gộp trên 1 ly = SellingPrice - TotalCost
        public decimal? MarginPercent { get; set; } // Tỷ suất lợi nhuận gộp (%)
        public List<IngredientCostDetailDto> Ingredients { get; set; } = new();
    }
}
