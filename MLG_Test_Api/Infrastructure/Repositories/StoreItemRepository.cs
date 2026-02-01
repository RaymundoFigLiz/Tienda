using Microsoft.EntityFrameworkCore;
using MLG_Test.Core.Models;
using MLG_Test.Infrastructure.Data;

namespace MLG_Test.Infrastructure.Repositories
{
	public interface IStoreItemRepository
	{
		Task<IEnumerable<StoreItem>> GetAllAsync();
		Task<StoreItem> GetByIdAsync(int id);
	}
	public class StoreItemRepository : IStoreItemRepository
	{
		private readonly IContext _context;
		private readonly DbSet<StoreItem> _dbSet;
		public StoreItemRepository(IContext context)
		{
			_context = context;
			_dbSet = _context.Set<StoreItem>();
		}

		public async Task<IEnumerable<StoreItem>> GetAllAsync()
		{
			return await _dbSet
				.Where(s => s.IsActive)
				.Include(s => s.Store)
				.Include(s => s.Item)
				.ToListAsync();
		}

		public async Task<StoreItem> GetByIdAsync(int id)
		{
			return await _dbSet
				.Where(s => s.Id == id && s.IsActive)
				.Include(s => s.Store)
				.Include(s => s.Item)
				.FirstOrDefaultAsync();
		}
	}
}
