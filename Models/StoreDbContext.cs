using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options)
            : base(options)
        {
        }

        public DbSet<Part> Parts => Set<Part>();

        public DbSet<Invoice> Invoices => Set<Invoice>();

        public DbSet<Inventory> InventoryItems => Set<Inventory>();

        public DbSet<Operation> Operations => Set<Operation>();

        public DbSet<InventoryOperation> InventoryOperations
            => Set<InventoryOperation>();

        public DbSet<Order> Orders => Set<Order>();

        public DbSet<OrderLine> OrderLines => Set<OrderLine>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Part>(entity =>
            {
                entity.ToTable("Parts");
                entity.HasKey(e => e.PartID);

                entity.Property(e => e.Name)
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("Invoices");
                entity.HasKey(e => e.InvoiceID);

                entity.Property(e => e.SerialNumber)
                    .HasMaxLength(100);

                entity.Property(e => e.TotalValue)
                    .HasColumnName("totalValue")
                    .HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.ToTable("Inventory");
                entity.HasKey(e => e.InventoryID);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 2)");

                entity.HasOne(e => e.Part)
                    .WithMany(e => e.InventoryItems)
                    .HasForeignKey(e => e.PartID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(e => e.Invoice)
                    .WithMany(e => e.InventoryItems)
                    .HasForeignKey(e => e.InvoiceID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Operation>(entity =>
            {
                entity.ToTable("Operations");
                entity.HasKey(e => e.OperationID);

                entity.Property(e => e.Name)
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<InventoryOperation>(entity =>
            {
                entity.ToTable("InventoryOperations");
                entity.HasKey(e => e.InventoryOperationID);

                entity.Property(e => e.Explanation)
                    .HasMaxLength(300);

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 2)");

                entity.HasOne(e => e.Inventory)
                    .WithMany(e => e.InventoryOperations)
                    .HasForeignKey(e => e.InventoryID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(e => e.Operation)
                    .WithMany(e => e.InventoryOperations)
                    .HasForeignKey(e => e.OperationID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Orders");
                entity.HasKey(e => e.OrderID);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Line1)
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(e => e.Line2)
                    .HasMaxLength(200);

                entity.Property(e => e.Line3)
                    .HasMaxLength(200);

                entity.Property(e => e.City)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.State)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Zip)
                    .HasMaxLength(30);

                entity.Property(e => e.Country)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Shipped)
                    .HasDefaultValue(false);

                entity.HasMany(e => e.Lines)
                    .WithOne(e => e.Order)
                    .HasForeignKey(e => e.OrderID)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<OrderLine>(entity =>
            {
                entity.ToTable("OrderLines");
                entity.HasKey(e => e.OrderLineID);

                entity.Property(e => e.ItemName)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 2)");
            });
        }
    }
}