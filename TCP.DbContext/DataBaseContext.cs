using Microsoft.EntityFrameworkCore;
using TCP.Model.Entities;

namespace TCP.DataBaseContext
{
    public partial class DataBaseContext : DbContext
    {
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Invoice> Invoices { get; set; } = null!;
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; } = null!;      
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ListOption> ListOptions { get; set; } = null!;
        public DataBaseContext()
        {
        }
        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
