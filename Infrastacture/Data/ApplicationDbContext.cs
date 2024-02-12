using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastacture.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stock>()
                .HasKey(e => e.Symbol);

            modelBuilder.Entity<StockHistory>()
                .HasKey(e => new { e.Symbol, e.Date });
            modelBuilder.Entity<StockHistory>()
                .HasOne(child => child.Stock)
                .WithMany(parent => parent.StockHistory)
                .HasForeignKey(child => child.Symbol);

            modelBuilder.Entity<Order>()
                .HasKey(e => new { e.Symbol, e.OrderDate,e.UserId });
            modelBuilder.Entity<Order>()
                .HasOne(child => child.Stock)
                .WithMany(parent => parent.StockOrders)
                .HasForeignKey(child => child.Symbol);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockHistory> StockHistory { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
