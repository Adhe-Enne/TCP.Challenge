using Microsoft.EntityFrameworkCore;
using TCP.Model.Entities;

namespace TCP.DataBaseContext;

public partial class DataBaseContext : DbContext
{
    public DataBaseContext()
    {
    }

    public DataBaseContext(DbContextOptions<DataBaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }

    public virtual DbSet<ListOption> ListOptions { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //=> optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress;Database=TCP.FacturadorDB;Trusted_Connection=True;MultipleActiveResultSets=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("Client");

            entity.Property(e => e.Adress)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CompanyName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CUIT)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CUIT");
            entity.Property(e => e.DateAdded).HasColumnType("datetime");
            entity.Property(e => e.DateUpdated).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DateAdded).HasColumnType("datetime");
            entity.Property(e => e.DateUpdated).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
        });


        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.ToTable("Invoice");

            entity.Property(e => e.DateAdded).HasColumnType("datetime");
            entity.Property(e => e.DateUpdated).HasColumnType("datetime");
            entity.Property(e => e.DueDate).HasColumnType("datetime");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Client).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoice_Client");

            entity.HasOne(d => d.Customer).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoice_Customer");
        });

        modelBuilder.Entity<InvoiceDetail>(entity =>
        {
            entity.ToTable("Invoice_Detail");

            entity.Property(e => e.DateAdded).HasColumnType("datetime");
            entity.Property(e => e.DateUpdated).HasColumnType("datetime");
            entity.Property(e => e.LineAmount).HasColumnType("decimal(30, 0)");
            entity.Property(e => e.Qty).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Invoice).WithMany(p => p.Detail)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoice");

            entity.HasOne(d => d.Product).WithMany(p => p.InvoiceDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product");
        });

        modelBuilder.Entity<ListOption>(entity =>
        {
            entity.ToTable("ListOption");

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DateAdded).HasColumnType("datetime");
            entity.Property(e => e.DateUpdated).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OptionType)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DateAdded).HasColumnType("datetime");
            entity.Property(e => e.DateUpdated).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
