using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShopSystem.Models
{
    public partial class Drink
    {
        public int DrinkId { get; set; }
        [MaxLength(100)]
        public string DrinkName { get; set; }
        [Column(TypeName = "numeric(18,2)")]
        public decimal? Price { get; set; }
        public byte[]? DrinkImage { get; set; }
        public List<InvoiceDetail>? InvoiceDetails { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public List<BillOfMaterials>? BillOfMaterials { get; set; }
    }
}
