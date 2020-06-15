using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnTime.Model.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnTime.Model
{
    public class AppDbContext : IdentityDbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ClientPersonal>(entity =>
            {
                entity.ToTable("ClientPersonal");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).UseIdentityColumn();
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.CompanyName).IsRequired();
                entity.Property(e => e.AccountNumber).IsRequired();
                entity.Property(e => e.CnicNumber).IsRequired();
                entity.Property(e => e.WebsiteUrl).IsRequired();
                entity.Property(e => e.ContactNumber).IsRequired();
                entity.Property(e => e.EmailAddress).IsRequired();
                entity.Property(e => e.Address).IsRequired();
                entity.HasOne(e => e.ApplicationUser).WithOne(e => e.ClientPersonal).HasForeignKey<ClientPersonal>(e => e.UserId);
                entity.HasMany(e => e.Orders).WithOne(e => e.ClientPersonal).HasForeignKey(e => e.ClientPersonalId);
            });
            builder.Entity<ProductType>(entity =>
            {
                entity.ToTable("ProductType");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).UseIdentityColumn();
                entity.Property(e => e.Name).IsRequired();
                entity.HasMany(e => e.ClientPersonals).WithOne(e => e.ProductType).HasForeignKey(e => e.ProductTypeId);
                entity.HasMany(e => e.Orders).WithOne(e => e.ProductType).HasForeignKey(e => e.ProductTypeId);
                entity.HasData(new ProductType
                {
                    Id = 1,
                    Name = "Cosmetics"
                },
                new ProductType
                {
                    Id = 2,
                    Name = "Appliances"
                });
            });
           
        }

        public DbSet<ClientPersonal> ClientPersonal { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<City> Cities { get; set; }
    }
}
