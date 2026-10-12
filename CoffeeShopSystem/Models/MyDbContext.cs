using Microsoft.EntityFrameworkCore;

namespace CoffeeShopSystem.Models
{
    public class MyDbContext:DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<Customer>Customers{ get; set; }
        public DbSet<Drink>Drinks{ get; set; }
        public DbSet<Invoice>Invoices{ get; set; }
        public DbSet<InvoiceDetail>InvoiceDetails{ get; set; }
        public DbSet<MyTable>MyTables{ get; set; }
        public DbSet<Staff>Staff{ get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<StockEntry> StockEntries { get; set; }
        public DbSet<BillOfMaterials> BillOfMaterials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>()
                .HasOne(sc => sc.Customer)
                .WithMany(s => s.Invoices)
                .HasForeignKey(sc => sc.CustomerId);
            modelBuilder.Entity<Invoice>()
                .HasOne(sc => sc.MyTable)
                .WithMany(s => s.Invoices)
                .HasForeignKey(sc => sc.TableId);
            modelBuilder.Entity<Invoice>()
                .HasOne(sc => sc.Staff)
                .WithMany(s => s.Invoices)
                .HasForeignKey(sc => sc.Email);

            modelBuilder.Entity<InvoiceDetail>()
                .HasOne(sc => sc.Invoice)
                .WithMany(s => s.InvoiceDetails)
                .HasForeignKey(sc => sc.InvoiceCode);
            modelBuilder.Entity<InvoiceDetail>()
                .HasOne(sc => sc.Drink)
                .WithMany(s => s.InvoiceDetails)
                .HasForeignKey(sc => sc.DrinkId);
            modelBuilder.Entity<MyTable>()
                .HasOne(sc => sc.Branch)
                .WithMany(s => s.MyTables)
                .HasForeignKey(sc => sc.BranchId);

            modelBuilder.Entity<StockEntry>()
                .HasOne(sc=>sc.Ingredient)
                .WithMany(s => s.StockEntries)
                .HasForeignKey(sc => sc.IngredientId);

            modelBuilder.Entity<Staff>()
                .HasOne(sc => sc.Branch)
                .WithMany(s => s.Staffs)
                .HasForeignKey(sc => sc.BranchId);

            modelBuilder.Entity<BillOfMaterials>()
                .HasOne(di => di.Drink)
                .WithMany(d => d.BillOfMaterials)
                .HasForeignKey(di => di.DrinkId);

            modelBuilder.Entity<BillOfMaterials>()
                .HasOne(di => di.Ingredient)
                .WithMany(i => i.BillOfMaterials)
                .HasForeignKey(di => di.IngredientId);
        }

    }
}
