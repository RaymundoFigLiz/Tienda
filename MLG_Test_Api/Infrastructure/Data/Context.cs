using Microsoft.EntityFrameworkCore;
using MLG_Test.Core.Models;

namespace MLG_Test.Infrastructure.Data
{
	public interface IContext
	{
		DbSet<T> Set<T>() where T : BaseModel;
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
	public class Context : DbContext, IContext
	{
		public DbSet<Address> Addresses => Set<Address>();
		public DbSet<Client> Clients => Set<Client>();
		public DbSet<Item> Items => Set<Item>();
		public DbSet<Role> Roles => Set<Role>();
		public DbSet<Sale> Sales => Set<Sale>();
		public DbSet<Store> Stores => Set<Store>();
		public DbSet<StoreItem> StoreItems => Set<StoreItem>();
		public DbSet<User> Users => Set<User>();

		DbSet<T> IContext.Set<T>() => Set<T>();

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => base.SaveChangesAsync(cancellationToken);
		public Context(DbContextOptions options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Client>().
				HasOne(p => p.User)
				.WithOne()
				.HasForeignKey<Client>(p => p.Id)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Client>()
				.HasOne(e => e.Address)
				.WithMany(a => a.Clients)
				.HasForeignKey(e => e.AddressId)
				.OnDelete(DeleteBehavior.Restrict);

			// Data Seed
			modelBuilder.Entity<Role>()
				.HasData(
					new Role
					{
						Id = 1,
						Description = "Admin",
						IsActive = true
					},
					new Role
					{
						Id = 2,
						Description = "Client",
						IsActive = true
					}
				);

			modelBuilder.Entity<User>()
				.HasOne(e => e.Role)
				.WithMany(a => a.Users)
				.HasForeignKey(e => e.RoleId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Sale>()
				.HasOne(e => e.Client)
				.WithMany(a => a.Sales)
				.HasForeignKey(e => e.ClientId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Sale>()
				.HasOne(e => e.StoreItem)
				.WithMany(a => a.Sales)
				.HasForeignKey(e => e.StoreItemId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Store>()
				.HasOne(e => e.Address)
				.WithMany(a => a.Stores)
				.HasForeignKey(e => e.AddressId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<StoreItem>()
				.HasOne(e => e.Item)
				.WithMany(a => a.StoreItems)
				.HasForeignKey(e => e.ItemId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<StoreItem>()
				.HasOne(e => e.Store)
				.WithMany(a => a.StoreItems)
				.HasForeignKey(e => e.StoreId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
