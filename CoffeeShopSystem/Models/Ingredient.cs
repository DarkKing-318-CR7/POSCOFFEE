using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CoffeeShopSystem.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }

        [MaxLength(100)]
        public string IngredientName { get; set; }
        [MaxLength(30)]
        public string? IngredientUnit { get; set; }
        [Column(TypeName = "numeric(18,2)")]
        public decimal AvePrice { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Inventory { get; set; }
        [JsonIgnore]
        public List<StockEntry>? StockEntries { get; set; }
        [JsonIgnore]
        public List<BillOfMaterials>? BillOfMaterials { get; set; }
    }
}
