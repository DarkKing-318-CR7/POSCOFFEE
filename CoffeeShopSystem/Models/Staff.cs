using System.ComponentModel.DataAnnotations;

namespace CoffeeShopSystem.Models
{
    public class Staff
    {
        [MaxLength(100)]
        [Key]
        public string Email { get; set; }
        [MaxLength(20)]
        public string? Telephone { get; set; }
        public DateTimeOffset? StartWorkingDate { get; set; }
        [MaxLength(100)]
        public string? FullName { get; set; }
        [MaxLength(255)]
        public string Password { get; set; }
        public byte[]? Avatar { get; set; }
        [MaxLength(20)]
        public string? Role { get; set; }
        public int BranchId { get; set; }
        public Branch? Branch { get; set; }
        public List<Invoice>? Invoices { get; set; }
    }
}
