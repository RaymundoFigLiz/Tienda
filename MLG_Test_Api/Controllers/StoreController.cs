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
	[Route("api/store")]
	[Authorize]
	public class StoreController : ControllerBase
	{
		private readonly IGenericService<Store> _genericService;
		private readonly IStoreService _storeService;
		private readonly IMapper _mapper;
		public StoreController(IGenericService<Store> genericService,IStoreService storeService, IMapper mapper)
		{
			_genericService = genericService;
			_storeService = storeService;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<Wrapper<IEnumerable<StoreDTO>>> GetAll()
		{
			var result = await _storeService.GetAllAsync();
			return Wrapper<IEnumerable<StoreDTO>>.CreateOk(result);
		}

		[HttpPost]
		public async Task<Wrapper<Store>> Create(CreateStoreDTO createDto)
		{
			var entity = _mapper.Map<Store>(createDto);

			var result = await _genericService.CreateAsync(entity);
			return Wrapper<Store>.CreateOk(result);
		}

		[HttpPut("{id:int}")]
		public async Task<Wrapper<Store>> Update(int id, UpdateStoreDTO updateDto)
		{
			var entity = _mapper.Map<Store>(updateDto);
			entity.Id = id;

			var result = await _genericService.UpdateAsync(entity);
			return Wrapper<Store>.CreateOk(result);
		}

		[HttpDelete("{id:int}")]
		public async Task<Wrapper<Store>> Deactivate(int id)
		{
			var result = await _genericService.DeactivateAsync(id);
			return Wrapper<Store>.CreateOk(result);
		}
	}
}
