using Microsoft.EntityFrameworkCore;
using MLG_Test.Core.Models;
using MLG_Test.Infrastructure.Data;

namespace MLG_Test.Infrastructure.Repositories
{
	public interface IStoreRepository
	{
		Task<IEnumerable<Store>> GetAllAsync();
	}
	public class StoreRepository : IStoreRepository
	{
		private readonly IContext _context;
		private readonly DbSet<Store> _dbSet;
		public StoreRepository(IContext context)
		{
			_context = context;
			_dbSet = _context.Set<Store>();
		}

		public async Task<IEnumerable<Store>> GetAllAsync()
		{
			return await _dbSet
				.Where(s => s.IsActive)
				.Include(s => s.Address)
				.ToListAsync();
		}
	}
}
