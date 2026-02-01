using AutoMapper;
using MLG_Test.Application.DTOs;
using MLG_Test.Core.Models;
using MLG_Test.Infrastructure.Repositories;

namespace MLG_Test.Application.Services
{
	public interface ISaleService
	{
		Task<IEnumerable<SaleDTO>> GetAllAsync();
		Task<IEnumerable<Sale>> CreateAsync(CreateSaleDTO saleDto);
	}
	public class SaleService : ISaleService
	{
		private readonly ISaleRepository _saleRepository;
		private readonly IGenericRepository<Sale> _genericRepository;
		private readonly IStoreItemService _storeItemService;
		private readonly IMapper _mapper;

		public SaleService(ISaleRepository repository, IGenericRepository<Sale> genericRepository, IStoreItemService storeItemService, IMapper mapper)
		{
			_saleRepository = repository;
			_genericRepository = genericRepository;
			_storeItemService = storeItemService;
			_mapper = mapper;
		}

		public async Task<IEnumerable<SaleDTO>> GetAllAsync()
		{
			var sales = await _saleRepository.GetAllAsync();
			return _mapper.Map<IEnumerable<SaleDTO>>(sales);
		}

		public async Task<IEnumerable<Sale>> CreateAsync(CreateSaleDTO saleDto)
		{
			var sales = new List<Sale>();

			foreach (var storeItemId in saleDto.StoreItemIds)
			{
				var storeItem = await _storeItemService.GetByIdAsync(storeItemId);
				var saleToCreate = new Sale
				{
					ClientId = saleDto.ClientId,
					StoreItemId = storeItemId,
					Price = storeItem.Price,
					Date = DateTime.Now
				};

				var createdSale = _genericRepository.Create(saleToCreate);
				sales.Add(createdSale);
				storeItem.Stock -= 1; // Restar uno al stock
			}

			await _genericRepository.SaveChangesAsync();

			return sales;
		}

	}
}
