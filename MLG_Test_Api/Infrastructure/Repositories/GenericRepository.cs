using Microsoft.EntityFrameworkCore;
using MLG_Test.Core.Models;
using MLG_Test.Infrastructure.Data;

namespace MLG_Test.Infrastructure.Repositories
{
	public interface IGenericRepository<T> where T : BaseModel
	{
		Task<IEnumerable<T>> GetAllAsync();
		Task<T> GetByIdAsNoTrackingAsync(int id);
		Task<T> GetByIdAsync(int id);
		T Create(T entityToCreate);
		T Update(T entityToUpdate);
		T Deactivate(T entityToDeactivate);
		Task SaveChangesAsync();
	}
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
	{
		private readonly IContext _context;
		private readonly DbSet<T> _dbSet;
		public GenericRepository(IContext context)
		{
			_context = context;
			_dbSet = _context.Set<T>();
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _dbSet
				.Where(t => t.IsActive)
				.ToListAsync();
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _dbSet
				.Where(t => t.Id == id)
				.FirstOrDefaultAsync();
		}

		public async Task<T> GetByIdAsNoTrackingAsync(int id)
		{
			return await _dbSet.AsNoTracking()
				.Where(t => t.Id == id)
				.FirstOrDefaultAsync();
		}

		public T Create(T entityToCreate)
		{
			var entityCreated = _dbSet.Add(entityToCreate).Entity;
			return entityCreated;
		}

		public T Update(T entityToUpdate)
		{
			var entityUpdated = _dbSet.Update(entityToUpdate).Entity;
			return entityUpdated;
		}

		public T Activate(T entityToActivate)
		{
			entityToActivate.IsActive = true;
			return entityToActivate;
		}

		public T Deactivate(T entityToDeactivate)
		{
			entityToDeactivate.IsActive = false;
			return entityToDeactivate;
		}

		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}
