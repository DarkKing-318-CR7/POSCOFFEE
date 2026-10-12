using System.ComponentModel.DataAnnotations;

namespace CoffeeShopSystem.Models
{
    public partial class MyTable
    {
        public int MyTableId { get; set; }
        public int TableNumber { get; set; }
        [MaxLength(100)]
        public string? Position { get; set; }
        public int BranchId { get; set; }
        public List<Invoice>? Invoices { get; set; }
        public Branch? Branch { get; set; }
    }
}
