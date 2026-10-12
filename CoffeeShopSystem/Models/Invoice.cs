using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShopSystem.Models
{
    public partial class Invoice
    {
        [Key]
        [MaxLength(30)]
        public string InvoiceCode { get; set; }
        public int? CustomerId { get; set; }
        public DateTimeOffset? InvoiceDate { get; set; }
        public int? TableId { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        [Column(TypeName = "numeric(18,2)")]
        public decimal TotalAmount { get; set; }

        public bool PaymentStatus { get; set; }

        public List<InvoiceDetail>? InvoiceDetails { get; set; }
        public Customer? Customer { get; set; }
        public MyTable? MyTable { get; set; }
        public Staff? Staff { get; set; }
    }
}
