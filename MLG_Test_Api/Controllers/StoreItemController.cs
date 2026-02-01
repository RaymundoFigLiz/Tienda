using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MLG_Test.Application.DTOs;
using MLG_Test.Application.Services;
using MLG_Test.Core.ClientUtilities;
using MLG_Test.Core.Models;

namespace MLG_Test.Controllers
{
	[ApiController]
	[Route("api/store-item")]
	[Authorize]
	public class StoreItemController : ControllerBase
	{
		private readonly IGenericService<StoreItem> _genericService;
		private readonly IStoreItemService _storeItemService;
		private readonly IMapper _mapper;
		public StoreItemController(IGenericService<StoreItem> genericService,IStoreItemService storeService, IMapper mapper)
		{
			_genericService = genericService;
			_storeItemService = storeService;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<Wrapper<IEnumerable<StoreItemDTO>>> GetAll()
		{
			var result = await _storeItemService.GetAllAsync();
			return Wrapper<IEnumerable<StoreItemDTO>>.CreateOk(result);
		}

		[HttpPost]
		public async Task<Wrapper<StoreItem>> Create(CreateStoreItemDTO createDto)
		{
			var entity = _mapper.Map<StoreItem>(createDto);

			var result = await _genericService.CreateAsync(entity);
			return Wrapper<StoreItem>.CreateOk(result);
		}

		[HttpPut("{id:int}")]
		public async Task<Wrapper<StoreItem>> Update(int id, UpdateStoreItemDTO updateDto)
		{
			var entity = _mapper.Map<StoreItem>(updateDto);
			entity.Id = id;

			var result = await _genericService.UpdateAsync(entity);
			return Wrapper<StoreItem>.CreateOk(result);
		}

		[HttpDelete("{id:int}")]
		public async Task<Wrapper<StoreItem>> Deactivate(int id)
		{
			var result = await _genericService.DeactivateAsync(id);
			return Wrapper<StoreItem>.CreateOk(result);
		}
	}
}
