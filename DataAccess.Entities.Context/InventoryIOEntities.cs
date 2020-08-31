using DataAccess.Database.InventoryIO;
using DataAccess.Entities.Context.Interface;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccess.Entities.Context
{
    public class InventoryIOEntities : DbContext, IInventoryIOEntities
    {
        public InventoryIOEntities(DbContextOptions<InventoryIOEntities> options)
            : base(options)
        { }

        public virtual DbSet<CustomerPrice> CustomerPrices { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<MenuDetail> MenuDetails { get; set; }
        public virtual DbSet<MenuRoleDetail> MenuRoleDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<UserDetail> UserDetails { get; set; }
        public virtual DbSet<UserRoleDetail> UserRoleDetails { get; set; }
        public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public virtual DbSet<OrderType> OrderTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderType>()
                .HasKey(c => new { c.OrderTypeID });

            modelBuilder.Entity<PurchaseOrderDetail>()
                .HasKey(c => new { c.SupplierID, c.ProductID, c.PurchaseOrderID });

            modelBuilder.Entity<CustomerPrice>()
                .HasKey(c => new { c.CustomerID, c.ProductID });

            modelBuilder.Entity<MenuRoleDetail>()
                .HasKey(c => new { c.UserRoleID, c.MenuID });
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Customer>()
        //        .HasMany(e => e.CustomerPrices);
        //        //.WithRequired(e => e.Customer)
        //        //.WillCascadeOnDelete(false);

        //    modelBuilder.Entity<MenuDetail>()
        //        .HasMany(e => e.UserRoleDetails);
        //    //.WithMany(e => e.MenuDetails)
        //    //.Map(m => m.ToTable("MenuRoleDetails").MapLeftKey("MenuID").MapRightKey("UserRoleID"));

        //    modelBuilder.Entity<Product>()
        //        .HasMany(e => e.CustomerPrices);
        //        //.WithRequired(e => e.Product)
        //        //.WillCascadeOnDelete(false);

        //    modelBuilder.Entity<UserRoleDetail>()
        //        .Property(e => e.UserRoleName)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<UserRoleDetail>()
        //        .HasMany(e => e.UserDetails);
        //    //.WithRequired(e => e.UserRoleDetail)
        //    //.WillCascadeOnDelete(false);


        //    base.OnModelCreating(modelBuilder);
        //}


    }
}
