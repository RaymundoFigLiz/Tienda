using MLG_Test.Core.ClientUtilities;
using MLG_Test.Core.Models;
using MLG_Test.Infrastructure.Repositories;

namespace MLG_Test.Application.Services
{
	public interface IGenericService<T> where T : BaseModel
	{
		Task<IEnumerable<T>> GetAllAsync();
		Task<T> GetByIdAsync(int id);
		Task<T> CreateAsync(T entityToCreate);
		Task<T> UpdateAsync(T entityToUpdate);
		Task<T> DeactivateAsync(int id);
	}
	public class GenericService<T> : IGenericService<T> where T : BaseModel
	{
		private readonly IGenericRepository<T> _repository;

		public GenericService(IGenericRepository<T> repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _repository.GetAllAsync();
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _repository.GetByIdAsync(id);
		}

		public async Task<T> CreateAsync(T entityToCreate)
		{
			var entityCreated = _repository.Create(entityToCreate);
			await _repository.SaveChangesAsync();
			return entityCreated;
		}

		public async Task<T> UpdateAsync(T entityToUpdate)
		{
			var entityUpdated = _repository.Update(entityToUpdate);
			await _repository.SaveChangesAsync();
			return entityUpdated;
		}

		public async Task<T> DeactivateAsync(int id)
		{
			var entityToDeactivate = await _repository.GetByIdAsync(id);

			if(entityToDeactivate == null)
				throw new Exception(Messages.Error404);

			var entityDeactivated = _repository.Deactivate(entityToDeactivate);
			await _repository.SaveChangesAsync();
			return entityDeactivated;
		}
	}
}
