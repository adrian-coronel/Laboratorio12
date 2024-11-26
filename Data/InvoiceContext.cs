using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class InvoiceContext : DbContext
    {

        // MIGRACIÓN
        /*
         - add-migration "v1 create database" : 
         - update-database
         */

        public DbSet<Product> Products { get; set; }
        public DbSet<Detail> Details { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=PC-ACORONEL\\SQLEXPRESS;Initial Catalog=InvoiceDB;user id=sa;password=miryam2003;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Invoices)
                .WithOne(i => i.Customer)
                .HasForeignKey(i => i.CustomerId) // ForeignKey está en Invoice
                .OnDelete(DeleteBehavior.Cascade); // Ejemplo de configuración adicional

            modelBuilder.Entity<Invoice>()
                .HasMany(i => i.Details)
                .WithOne(d => d.Invoice)
                .HasForeignKey(d => d.InvoiceId);



            modelBuilder.Entity<Customer>().HasQueryFilter(x => x.Enabled);
        }

    }
}
