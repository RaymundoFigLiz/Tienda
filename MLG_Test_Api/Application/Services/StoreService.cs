using AutoMapper;
using MLG_Test.Application.DTOs;
using MLG_Test.Infrastructure.Repositories;

namespace MLG_Test.Application.Services
{
	public interface IStoreService
	{
		Task<IEnumerable<StoreDTO>> GetAllAsync();
	}
	public class StoreService : IStoreService
	{
		private readonly IStoreRepository _repository;
		private readonly IMapper _mapper;

		public StoreService(IStoreRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<StoreDTO>> GetAllAsync()
		{
			var stores = await _repository.GetAllAsync();
			return _mapper.Map<IEnumerable<StoreDTO>>(stores);
		}

	}
}
