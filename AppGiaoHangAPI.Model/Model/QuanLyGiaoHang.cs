using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using AppGiaoHangAPI.Model.Model;

namespace AppGiaoHangAPI.Model.Model
{
    public partial class QuanLyGiaoHang : DbContext
    {
        public QuanLyGiaoHang()
        {
        }

        public QuanLyGiaoHang(DbContextOptions<QuanLyGiaoHang> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<CustomerOrder> CustomerOrders { get; set; } = null!;
        public virtual DbSet<CustomerOrderDetail> CustomerOrderDetails { get; set; } = null!;
        public virtual DbSet<CustomerOrderInformation> CustomerOrderInformations { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:dbConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.HasIndex(e => e.UserName, "UQ__Account__C9F284565D3F133B")
                    .IsUnique();

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.AccountCreateTime).HasColumnType("datetime");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.Roles).IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FKAccount_EmployeeID");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.CustomerCode)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasComputedColumnSql("('CUS'+right((right(datepart(year,[DateCreate]),(2))+'00000')+CONVERT([varchar](5),[CustomerID]),(5)))", true);

                entity.Property(e => e.DateCreate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CustomerOrder>(entity =>
            {
                entity.ToTable("CustomerOrder");

                entity.Property(e => e.CustomerOrderId).HasColumnName("CustomerOrderID");

                entity.Property(e => e.CustomerOrderInformationId).HasColumnName("CustomerOrderInformationID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.TotalPrice).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.CustomerOrderInformation)
                    .WithMany(p => p.CustomerOrders)
                    .HasForeignKey(d => d.CustomerOrderInformationId)
                    .HasConstraintName("FKCustomerOrder_CustomerOrderInformationID");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.CustomerOrders)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FKCustomerOrder_EmployeeID");
            });

            modelBuilder.Entity<CustomerOrderDetail>(entity =>
            {
                entity.ToTable("CustomerOrderDetail");

                entity.Property(e => e.CustomerOrderDetailId).HasColumnName("CustomerOrderDetailID");

                entity.Property(e => e.CustomerOrderId).HasColumnName("CustomerOrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.CustomerOrder)
                    .WithMany(p => p.CustomerOrderDetails)
                    .HasForeignKey(d => d.CustomerOrderId)
                    .HasConstraintName("FKCustomerOrderDetail_CustomerOrderID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CustomerOrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FKCustomerOrderDetail_ProductID");
            });

            modelBuilder.Entity<CustomerOrderInformation>(entity =>
            {
                entity.ToTable("CustomerOrderInformation");

                entity.Property(e => e.CustomerOrderInformationId).HasColumnName("CustomerOrderInformationID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerOrderInformations)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FKCustomerOrderInformation_CustomerID");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.DateJoin).HasColumnType("datetime");

                entity.Property(e => e.EmployeeCode)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasComputedColumnSql("('EMP'+right((right(datepart(year,[DateJoin]),(2))+'00000')+CONVERT([varchar](5),[EmployeeID]),(5)))", true);

                entity.Property(e => e.IdentityNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
