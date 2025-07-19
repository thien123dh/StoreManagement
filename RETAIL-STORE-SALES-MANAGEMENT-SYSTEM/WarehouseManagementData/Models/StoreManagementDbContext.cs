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
        => optionsBuilder.UseSqlServer("Server=HELLOHAVAN\\SQLEXPRESS;User Id=sa;Password=1234567890;Database=StoreManagementDb;Trusted_Connection=True;TrustServerCertificate=True;");

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

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.CategoryCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("Category_Users_FK");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.CategoryUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("Category_Users_FK_1");
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

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.CustomerCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("Customer_Users_FK");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.CustomerUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("Customer_Users_FK_1");
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

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ImportRequestCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("ImportRequest_Users_FK");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.ImportRequestUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("ImportRequest_Users_FK_1");
        });

        modelBuilder.Entity<ImportRequestDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ImportRequestDetail_PK");

            entity.ToTable("ImportRequestDetail");

            entity.Property(e => e.ImportPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.ImportRequest).WithMany(p => p.ImportRequestDetails)
                .HasForeignKey(d => d.ImportRequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ImportRequestDetail_ImportRequest_FK");

            entity.HasOne(d => d.Product).WithMany(p => p.ImportRequestDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ImportRequestDetail_Product_FK");
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

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("Product_Category_FK");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ProductCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("Product_Users_FK");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.ProductUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("Product_Users_FK_1");
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

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Receipts)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("Receipt_Users_FK");

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

            entity.HasOne(d => d.Product).WithMany(p => p.ReceiptDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("ReceiptDetail_Product_FK");

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
