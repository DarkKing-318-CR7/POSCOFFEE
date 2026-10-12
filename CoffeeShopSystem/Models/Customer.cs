using System.ComponentModel.DataAnnotations;

namespace CoffeeShopSystem.Models
{
    public partial class Customer
    {
        public int CustomerId { get; set; }
        [MaxLength(100)]
        public string CustomerName { get; set; }
        [MaxLength(20)]
        public string? TelephoneNumber { get; set; }
        public List<Invoice>? Invoices { get; set; }

    }
}
