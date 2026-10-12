using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CoffeeShopSystem.Models
{
    public class StockEntry
    {
        public int StockEntryId { get; set; }

        [Column(TypeName = "numeric(18,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Quantity { get; set; }
        public int IngredientId { get; set; }
        public DateTimeOffset Date { get; set; } = DateTimeOffset.UtcNow;
        public int? BranchId { get; set; }
        [JsonIgnore]
        public Ingredient? Ingredient { get; set; }

    }
}
