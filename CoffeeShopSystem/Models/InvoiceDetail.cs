using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShopSystem.Models
{
    public partial class InvoiceDetail
    {
        public int Id { get; set; }
        [MaxLength(10)]
        public string InvoiceCode { get; set; }
        public int DrinkId { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal UnitPrice { get; set; }
        public Invoice? Invoice { get; set; }
        public Drink? Drink { get; set; }
    }
}
