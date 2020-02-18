
using Microsoft.EntityFrameworkCore;
using TinyCrmConsole.Model;

namespace TinyCrm.Core.Data
{
    public class TinyCrmDbContext : DbContext
    {
        private readonly string connectionString_;

        public TinyCrmDbContext()
        {
            connectionString_ = "Server = DESKTOP-TJ30A6D\\BASI17; Database = tinycrm2 ; Integrated Security=SSPI  ; Persist Security Info=False;";
        }

        public TinyCrmDbContext(string connString)
        {
            connectionString_ = connString;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Model.Customer>()
                .ToTable("Customer", "core");

            modelBuilder
               .Entity<Model.Customer>()
               .HasIndex(c => c.VatNumber)
               .IsUnique();

            modelBuilder
               .Entity<Model.Customer>()
               .Property(c => c.VatNumber)
               .HasMaxLength(9)
               .IsFixedLength();

            modelBuilder
                .Entity<Model.Order>()
               .ToTable("Order", "core");


            modelBuilder
                 .Entity<Model.Product>()
                .ToTable("Product", "core");

            modelBuilder
                    .Entity<Model.OrderProduct>()
                    .ToTable("OrderProduct", "core");
            modelBuilder
                .Entity<Model.OrderProduct>()
                .HasKey(op => new { op.ProductId, op.OrderId });
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(connectionString_);
        }
        
    }
}
