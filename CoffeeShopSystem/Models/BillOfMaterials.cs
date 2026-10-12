using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CoffeeShopSystem.Models
{
    public class BillOfMaterials
    {
        public int Id { get; set; }
        public int DrinkId { get; set; }
        public int IngredientId { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Quantity { get; set; } // Lượng nguyên liệu cần cho 1 ly nước (gam, ml...)

        [JsonIgnore]
        public Drink? Drink { get; set; }

        [JsonIgnore]
        public Ingredient? Ingredient { get; set; }
    }
}
