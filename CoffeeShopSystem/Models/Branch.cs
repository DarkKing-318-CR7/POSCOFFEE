using System.ComponentModel.DataAnnotations;

namespace CoffeeShopSystem.Models
{
    public class Branch
    {
        public int BranchId { get; set; }

        [MaxLength(100)]
        public string BranchName { get; set; }
        [MaxLength(200)]
        public string BranchAddress { get; set; }

        public List<MyTable>? MyTables { get; set; }
        public List<Staff>? Staffs { get; set; }
    }
}
