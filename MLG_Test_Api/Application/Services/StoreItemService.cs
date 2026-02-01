using AutoMapper;
using MLG_Test.Application.DTOs;
using MLG_Test.Core.Models;
using MLG_Test.Infrastructure.Repositories;

namespace MLG_Test.Application.Services
{
	public interface IStoreItemService
	{
		Task<IEnumerable<StoreItemDTO>> GetAllAsync();
		Task<StoreItem> GetByIdAsync(int id);
	}
	public class StoreItemService : IStoreItemService
	{
		private readonly IStoreItemRepository _repository;
		private readonly IMapper _mapper;

		public StoreItemService(IStoreItemRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<StoreItemDTO>> GetAllAsync()
		{
			var storeItems = await _repository.GetAllAsync();
			return _mapper.Map<IEnumerable<StoreItemDTO>>(storeItems);
		}

		public async Task<StoreItem> GetByIdAsync(int id)
		{
			return await _repository.GetByIdAsync(id);
		}

	}
}
