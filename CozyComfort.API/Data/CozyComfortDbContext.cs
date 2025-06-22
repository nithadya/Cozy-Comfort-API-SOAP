using Microsoft.EntityFrameworkCore;
using CozyComfort.API.Models;

namespace CozyComfort.API.Data
{
    public class CozyComfortDbContext : DbContext
    {
        public CozyComfortDbContext(DbContextOptions<CozyComfortDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Username).IsUnique();
                entity.Property(e => e.UserType).HasConversion<int>();
            });

            // Configure Product entity
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.SKU).IsUnique();
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
            });

            // Configure Order entity
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => e.OrderNumber).IsUnique();
                entity.Property(e => e.Status).HasConversion<int>();
                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)");
                
                entity.HasOne(e => e.Seller)
                    .WithMany()
                    .HasForeignKey(e => e.SellerId)
                    .OnDelete(DeleteBehavior.Restrict);
                    
                entity.HasOne(e => e.Distributor)
                    .WithMany()
                    .HasForeignKey(e => e.DistributorId)
                    .OnDelete(DeleteBehavior.Restrict);
                    
                entity.HasOne(e => e.Manufacturer)
                    .WithMany()
                    .HasForeignKey(e => e.ManufacturerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure OrderItem entity
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18,2)");
                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18,2)");
            });

            // Configure Inventory entity
            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasOne(e => e.Product)
                    .WithMany()
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
                    
                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Username = "manufacturer", Email = "manufacturer@cozycomfort.com", PasswordHash = "hashed_password", UserType = UserType.Manufacturer, CompanyName = "Cozy Comfort Manufacturing", Address = "123 Manufacturing St", Phone = "555-0001" },
                new User { UserId = 2, Username = "distributor1", Email = "dist1@example.com", PasswordHash = "hashed_password", UserType = UserType.Distributor, CompanyName = "ABC Distribution", Address = "456 Distribution Ave", Phone = "555-0002" },
                new User { UserId = 3, Username = "seller1", Email = "seller1@example.com", PasswordHash = "hashed_password", UserType = UserType.Seller, CompanyName = "XYZ Retail", Address = "789 Retail Blvd", Phone = "555-0003" }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, ProductName = "Luxury Wool Blanket", SKU = "LWB001", Description = "Premium wool blanket for ultimate comfort", Material = "100% Wool", Size = "Queen", Color = "Navy Blue", Price = 199.99m, ProductionCapacity = 100, ProductionLeadTimeDays = 7 },
                new Product { ProductId = 2, ProductName = "Cotton Comfort Blanket", SKU = "CCB002", Description = "Soft cotton blanket for everyday use", Material = "100% Cotton", Size = "King", Color = "Beige", Price = 89.99m, ProductionCapacity = 200, ProductionLeadTimeDays = 5 },
                new Product { ProductId = 3, ProductName = "Fleece Throw Blanket", SKU = "FTB003", Description = "Cozy fleece throw perfect for any room", Material = "Polyester Fleece", Size = "Throw", Color = "Gray", Price = 39.99m, ProductionCapacity = 300, ProductionLeadTimeDays = 3 }
            );

            // Seed Inventory
            modelBuilder.Entity<Inventory>().HasData(
                // Manufacturer inventory
                new Inventory { InventoryId = 1, ProductId = 1, UserId = 1, StockQuantity = 150, ReorderLevel = 50, MaxStockLevel = 300, Location = "Warehouse A" },
                new Inventory { InventoryId = 2, ProductId = 2, UserId = 1, StockQuantity = 200, ReorderLevel = 75, MaxStockLevel = 400, Location = "Warehouse A" },
                new Inventory { InventoryId = 3, ProductId = 3, UserId = 1, StockQuantity = 250, ReorderLevel = 100, MaxStockLevel = 500, Location = "Warehouse A" },
                
                // Distributor inventory
                new Inventory { InventoryId = 4, ProductId = 1, UserId = 2, StockQuantity = 25, ReorderLevel = 10, MaxStockLevel = 50, Location = "Distribution Center 1" },
                new Inventory { InventoryId = 5, ProductId = 2, UserId = 2, StockQuantity = 30, ReorderLevel = 15, MaxStockLevel = 60, Location = "Distribution Center 1" },
                new Inventory { InventoryId = 6, ProductId = 3, UserId = 2, StockQuantity = 40, ReorderLevel = 20, MaxStockLevel = 80, Location = "Distribution Center 1" },
                
                // Seller inventory
                new Inventory { InventoryId = 7, ProductId = 1, UserId = 3, StockQuantity = 5, ReorderLevel = 2, MaxStockLevel = 15, Location = "Store Floor" },
                new Inventory { InventoryId = 8, ProductId = 2, UserId = 3, StockQuantity = 8, ReorderLevel = 3, MaxStockLevel = 20, Location = "Store Floor" },
                new Inventory { InventoryId = 9, ProductId = 3, UserId = 3, StockQuantity = 12, ReorderLevel = 5, MaxStockLevel = 25, Location = "Store Floor" }
            );
        }
    }
} 