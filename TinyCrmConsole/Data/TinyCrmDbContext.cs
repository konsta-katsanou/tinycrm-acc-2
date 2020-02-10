using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace TinyCrm.Core.Data
{
    public class TinyCrmDbContext : DbContext
    {
        private readonly string connectionString_;

        public TinyCrmDbContext()
        {
            connectionString_ = "Server = DESKTOP-TJ30A6D\\BASI17; Database = ecommerce; Integrated Security=SSPI  ; Persist Security Info=False;";
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
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(connectionString_);
        }
    }
}
