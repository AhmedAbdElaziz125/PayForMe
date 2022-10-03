using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PayForMe1._1.Models
{
    public partial class PayForMe_DBContext : DbContext
    {
        public PayForMe_DBContext()
        {
        }

        public PayForMe_DBContext(DbContextOptions<PayForMe_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Inventory> Inventories { get; set; } = null!;
        public virtual DbSet<LastService> LastServices { get; set; } = null!;
        public virtual DbSet<MainService> MainServices { get; set; } = null!;
        public virtual DbSet<MiddleService> MiddleServices { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Arabic_100_CS_AI");

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.ToTable("Inventory");

                entity.Property(e => e.InventoryDate).HasColumnType("datetime");

                entity.Property(e => e.InventoryStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LastService>(entity =>
            {
                entity.ToTable("LastService");

                entity.Property(e => e.LastServiceCost).HasColumnType("money");

                entity.Property(e => e.LastServiceName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastServiceTax).HasColumnType("money");

                entity.HasOne(d => d.MiddleService)
                    .WithMany(p => p.LastServices)
                    .HasForeignKey(d => d.MiddleServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LastService_MiddleService");
            });

            modelBuilder.Entity<MainService>(entity =>
            {
                entity.ToTable("MainService");

                entity.Property(e => e.MainServiceId).ValueGeneratedNever();

                entity.Property(e => e.MainServiceCost).HasColumnType("money");

                entity.Property(e => e.MainServiceName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MainServiceTax).HasColumnType("money");
            });

            modelBuilder.Entity<MiddleService>(entity =>
            {
                entity.ToTable("MiddleService");

                entity.Property(e => e.MiddleServiceCost).HasColumnType("money");

                entity.Property(e => e.MiddleServiceName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleServiceTax).HasColumnType("money");

                entity.HasOne(d => d.MainService)
                    .WithMany(p => p.MiddleServices)
                    .HasForeignKey(d => d.MainServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MiddleService_MainService");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.Cost).HasColumnType("money");

                entity.Property(e => e.IsDebt).HasColumnName("isDebt");

                entity.Property(e => e.Notes)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.Tax).HasColumnType("money");

                entity.Property(e => e.Total).HasColumnType("money");

                entity.HasOne(d => d.LastService)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.LastServiceId)
                    .HasConstraintName("FK_Order_LastService");

                entity.HasOne(d => d.MainService)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.MainServiceId)
                    .HasConstraintName("FK_Order_MainService");

                entity.HasOne(d => d.MiddleService)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.MiddleServiceId)
                    .HasConstraintName("FK_Order_MiddleService");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.Balance).HasColumnType("money");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Debits).HasColumnType("money");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LandLine).HasColumnName("landLine");

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NationalId).HasColumnName("National_Id");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Religion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserImage)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
