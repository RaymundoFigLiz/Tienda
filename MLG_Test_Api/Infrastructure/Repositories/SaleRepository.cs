using Microsoft.EntityFrameworkCore;
using MLG_Test.Core.Models;
using MLG_Test.Infrastructure.Data;

namespace MLG_Test.Infrastructure.Repositories
{
	public interface ISaleRepository
	{
		Task<IEnumerable<Sale>> GetAllAsync();
	}
	public class SaleRepository : ISaleRepository
	{
		private readonly IContext _context;
		private readonly DbSet<Sale> _dbSet;
		public SaleRepository(IContext context)
		{
			_context = context;
			_dbSet = _context.Set<Sale>();
		}

		public async Task<IEnumerable<Sale>> GetAllAsync()
		{
			return await _dbSet
				.Where(s => s.IsActive)
				.Include(s => s.Client)
					.ThenInclude(c => c.User)
				.Include(s => s.StoreItem)
					.ThenInclude(s => s.Store)
				.Include(s => s.StoreItem)
					.ThenInclude(s => s.Item)
				.ToListAsync();
		}
	}
}
