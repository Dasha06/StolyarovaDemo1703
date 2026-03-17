using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StolyarovaDemo1703.Models;

public partial class StolyarovaContext : DbContext
{
    public StolyarovaContext()
    {
    }

    public StolyarovaContext(DbContextOptions<StolyarovaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductName> ProductNames { get; set; }

    public virtual DbSet<ProductOrder> ProductOrders { get; set; }

    public virtual DbSet<Provider> Providers { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=192.168.2.18;Port=5432;Database=stolyarova;Username=stolyarova;Password=91743");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("addresses_pkey");

            entity.ToTable("addresses", "demo1703");

            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.AddressName).HasColumnName("address_name");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("categories_pkey");

            entity.ToTable("categories", "demo1703");

            entity.Property(e => e.CategoryId)
                .ValueGeneratedNever()
                .HasColumnName("category_id");
            entity.Property(e => e.CategoryName).HasColumnName("category_name");
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.ManufacturerId).HasName("manufacturers_pkey");

            entity.ToTable("manufacturers", "demo1703");

            entity.Property(e => e.ManufacturerId)
                .HasDefaultValueSql("nextval('demo1703.manufacturers_manufaturer_id_seq'::regclass)")
                .HasColumnName("manufacturer_id");
            entity.Property(e => e.ManufacturerName).HasColumnName("manufacturer_name");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("orders_pkey");

            entity.ToTable("orders", "demo1703");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.OrderCode).HasColumnName("order_code");
            entity.Property(e => e.OrderCreateDate).HasColumnName("order_create_date");
            entity.Property(e => e.OrderDeliveryDate).HasColumnName("order_delivery_date");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Address).WithMany(p => p.Orders)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_addresses_fk");

            entity.HasOne(d => d.Status).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_status_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_user_id_fkey");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductArticul).HasName("products_pkey");

            entity.ToTable("products", "demo1703");

            entity.Property(e => e.ProductArticul).HasColumnName("product_articul");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.ManufacturerId).HasColumnName("manufacturer_id");
            entity.Property(e => e.ProdNameId).HasColumnName("prod_name_id");
            entity.Property(e => e.ProductActiveDiscount).HasColumnName("product_active_discount");
            entity.Property(e => e.ProductCount).HasColumnName("product_count");
            entity.Property(e => e.ProductDescription).HasColumnName("product_description");
            entity.Property(e => e.ProductImage).HasColumnName("product_image");
            entity.Property(e => e.ProductPrice).HasColumnName("product_price");
            entity.Property(e => e.ProviderId).HasColumnName("provider_id");
            entity.Property(e => e.UnitId).HasColumnName("unit_id");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_category_id_fkey");

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.Products)
                .HasForeignKey(d => d.ManufacturerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_manufacturer_id_fkey");

            entity.HasOne(d => d.ProdName).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProdNameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_prod_name_id_fkey");

            entity.HasOne(d => d.Provider).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProviderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_provider_id_fkey");

            entity.HasOne(d => d.Unit).WithMany(p => p.Products)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_unit_id_fkey");
        });

        modelBuilder.Entity<ProductName>(entity =>
        {
            entity.HasKey(e => e.ProdNameId).HasName("product_names_pkey");

            entity.ToTable("product_names", "demo1703");

            entity.Property(e => e.ProdNameId)
                .ValueGeneratedNever()
                .HasColumnName("prod_name_id");
            entity.Property(e => e.ProdName).HasColumnName("prod_name");
        });

        modelBuilder.Entity<ProductOrder>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ProductArticul }).HasName("product_order_pkey");

            entity.ToTable("product_orders", "demo1703");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ProductArticul).HasColumnName("product_articul");
            entity.Property(e => e.ProductOrderCount).HasColumnName("product_order_count");

            entity.HasOne(d => d.Order).WithMany(p => p.ProductOrders)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("product_orders_order_id_fkey");

            entity.HasOne(d => d.ProductArticulNavigation).WithMany(p => p.ProductOrders)
                .HasForeignKey(d => d.ProductArticul)
                .HasConstraintName("product_orders_product_articul_fkey");
        });

        modelBuilder.Entity<Provider>(entity =>
        {
            entity.HasKey(e => e.ProviderId).HasName("providers_pkey");

            entity.ToTable("providers", "demo1703");

            entity.Property(e => e.ProviderId)
                .ValueGeneratedNever()
                .HasColumnName("provider_id");
            entity.Property(e => e.ProviderName).HasColumnName("provider_name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("roles_pkey");

            entity.ToTable("roles", "demo1703");

            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("role_id");
            entity.Property(e => e.RoleName).HasColumnName("role_name");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("statuses_pkey");

            entity.ToTable("statuses", "demo1703");

            entity.Property(e => e.StatusId)
                .ValueGeneratedNever()
                .HasColumnName("status_id");
            entity.Property(e => e.StatusName).HasColumnName("status_name");
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(e => e.UnitId).HasName("units_pkey");

            entity.ToTable("units", "demo1703");

            entity.Property(e => e.UnitId)
                .ValueGeneratedNever()
                .HasColumnName("unit_id");
            entity.Property(e => e.UnitName).HasColumnName("unit_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users", "demo1703");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UserFio).HasColumnName("user_fio");
            entity.Property(e => e.UserLogin).HasColumnName("user_login");
            entity.Property(e => e.UserPassword).HasColumnName("user_password");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_role_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
