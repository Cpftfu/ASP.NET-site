using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TeaShop.Models;

public partial class TeaShopDbContext : DbContext
{
    public TeaShopDbContext()
    {
    }

    public TeaShopDbContext(DbContextOptions<TeaShopDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<PlaceOfCultivation> PlaceOfCultivations { get; set; }

    public virtual DbSet<Tea> Teas { get; set; }

    public virtual DbSet<TeaOrder> TeaOrders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-MM4ENFA\\SQLEXPRESS;Initial Catalog=TeaShopDB;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategory).HasName("PK__Category__6DB3A68AC2328C07");

            entity.ToTable("Category");

            entity.Property(e => e.IdCategory).HasColumnName("ID_Category");
            entity.Property(e => e.NameOfCategory)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.IdCustomer).HasName("PK__Customer__2D8FDE5F7D0E0355");

            entity.Property(e => e.IdCustomer).HasColumnName("ID_Customer");
            entity.Property(e => e.EmailOfCust)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LoginOfCust)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MiddlenameOfCust)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NameOfCust)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PasswordOfCust)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.SurnameOfCust)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrder).HasName("PK__Orders__EC9FA955886ACA66");

            entity.Property(e => e.IdOrder).HasColumnName("ID_Order");
            entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__Customer__5070F446");
        });

        modelBuilder.Entity<PlaceOfCultivation>(entity =>
        {
            entity.HasKey(e => e.IdPlaceOfCultivation).HasName("PK__PlaceOfC__9C1266D8783788DF");

            entity.ToTable("PlaceOfCultivation");

            entity.Property(e => e.IdPlaceOfCultivation).HasColumnName("ID_PlaceOfCultivation");
            entity.Property(e => e.NameOfPlace)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Tea>(entity =>
        {
            entity.HasKey(e => e.IdTea).HasName("PK__Tea__27BE06C6A3EFD9BA");

            entity.ToTable("Tea");

            entity.Property(e => e.IdTea).HasColumnName("ID_Tea");
            entity.Property(e => e.CategoryId).HasColumnName("Category_ID");
            entity.Property(e => e.PlaceOfCultivationId).HasColumnName("PlaceOfCultivation_ID");
            entity.Property(e => e.PriceOfTea).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TeaName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Category).WithMany(p => p.Teas)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tea__Category_ID__59FA5E80");

            entity.HasOne(d => d.PlaceOfCultivation).WithMany(p => p.Teas)
                .HasForeignKey(d => d.PlaceOfCultivationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tea__PlaceOfCult__5AEE82B9");
        });

        modelBuilder.Entity<TeaOrder>(entity =>
        {
            entity.HasKey(e => e.IdTeaOrder).HasName("PK__TeaOrder__23D527BA8E6CF797");

            entity.Property(e => e.IdTeaOrder).HasColumnName("ID_TeaOrder");
            entity.Property(e => e.IdTea).HasColumnName("ID_Tea");
            entity.Property(e => e.OrderId).HasColumnName("Order_ID");

            entity.HasOne(d => d.IdTeaNavigation).WithMany(p => p.TeaOrders)
                .HasForeignKey(d => d.IdTea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TeaOrders__ID_Te__60A75C0F");

            entity.HasOne(d => d.Order).WithMany(p => p.TeaOrders)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TeaOrders__Order__5FB337D6");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
