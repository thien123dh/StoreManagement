using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WarehouseManagementData.Models;

public partial class StoreManagementDbContext : DbContext
{
    public StoreManagementDbContext()
    {
    }

    public StoreManagementDbContext(DbContextOptions<StoreManagementDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<ImportRequest> ImportRequests { get; set; }

    public virtual DbSet<ImportRequestDetail> ImportRequestDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Receipt> Receipts { get; set; }

    public virtual DbSet<ReceiptDetail> ReceiptDetails { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);User Id=sa;Password=12345;Database=StoreManagementDb;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Category_PK");

            entity.ToTable("Category");

            entity.Property(e => e.CreatedDateTime).HasPrecision(0);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Status).HasDefaultValue((short)1);
            entity.Property(e => e.UpdatedDateTime).HasPrecision(0);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Customer_PK");

            entity.ToTable("Customer");

            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDateTime).HasPrecision(0);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Fullname).HasMaxLength(255);
            entity.Property(e => e.UpdatedDateTime).HasPrecision(0);

        });

        modelBuilder.Entity<ImportRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ImportForm_PK");

            entity.ToTable("ImportRequest");

            entity.Property(e => e.CreatedDateTime).HasPrecision(0);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.ImportedSerialNumber)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValue((short)1);
            entity.Property(e => e.UpdatedDateTime).HasPrecision(0);
        });

        modelBuilder.Entity<ImportRequestDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ImportRequestDetail_PK");

            entity.ToTable("ImportRequestDetail");

            entity.Property(e => e.ImportPrice).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Product_PK");

            entity.ToTable("Product");

            entity.Property(e => e.CreatedDateTime).HasPrecision(0);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.ImportPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Manufactor).HasMaxLength(500);
            entity.Property(e => e.ManufactureDateTime).HasPrecision(0);
            entity.Property(e => e.Name).HasMaxLength(500);
            entity.Property(e => e.Notes).HasMaxLength(100);
            entity.Property(e => e.Quantity).HasDefaultValue(0);
            entity.Property(e => e.SellingPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SerialNumber)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValue((short)1);
            entity.Property(e => e.UpdatedDateTime).HasPrecision(0);
        });

        modelBuilder.Entity<Receipt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Receipt_PK");

            entity.ToTable("Receipt");

            entity.Property(e => e.Address).HasMaxLength(1000);
            entity.Property(e => e.CreatedDateTime).HasPrecision(0);
            entity.Property(e => e.CustomerName).HasMaxLength(255);
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.Promotion).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ReceiptSerialNumber)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Customer).WithMany(p => p.Receipts)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("Receipt_Customer_FK");
        });

        modelBuilder.Entity<ReceiptDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ReceiptDetail_PK");

            entity.ToTable("ReceiptDetail");

            entity.Property(e => e.Price).HasColumnType("decimal(38, 0)");
            entity.Property(e => e.ProductName).HasMaxLength(500);

            entity.HasOne(d => d.Receipt).WithMany(p => p.ReceiptDetails)
                .HasForeignKey(d => d.ReceiptId)
                .HasConstraintName("ReceiptDetail_Receipt_FK");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Users_PK");

            entity.HasIndex(e => e.Username, "Users_UNIQUE").IsUnique();

            entity.Property(e => e.BusinessId)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DateOfBirth).HasPrecision(0);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Fullname).HasMaxLength(255);
            entity.Property(e => e.Gender).HasDefaultValue(0);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Role).HasDefaultValue(2);
            entity.Property(e => e.Status).HasDefaultValue(1);
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
