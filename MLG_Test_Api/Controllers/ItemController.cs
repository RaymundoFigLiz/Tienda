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
	[Route("api/item")]
	[Authorize]
	public class ItemController : ControllerBase
	{
		private readonly IGenericService<Item> _service;
		private readonly IMapper _mapper;
		public ItemController(IGenericService<Item> service, IMapper mapper)
		{
			_service = service;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<Wrapper<IEnumerable<Item>>> GetAll()
		{
			var result = await _service.GetAllAsync();
			return Wrapper<IEnumerable<Item>>.CreateOk(result);
		}

		[HttpPost]
		public async Task<Wrapper<Item>> Create(CreateItemDTO createDto)
		{
			var entity = _mapper.Map<Item>(createDto);

			var result = await _service.CreateAsync(entity);
			return Wrapper<Item>.CreateOk(result);
		}

		[HttpPut("{id:int}")]
		public async Task<Wrapper<Item>> Update(int id, UpdateItemDTO updateDto)
		{
			var entity = _mapper.Map<Item>(updateDto);
			entity.Id = id;

			var result = await _service.UpdateAsync(entity);
			return Wrapper<Item>.CreateOk(result);
		}

		[HttpDelete("{id:int}")]
		public async Task<Wrapper<Item>> Deactivate(int id)
		{
			var result = await _service.DeactivateAsync(id);
			return Wrapper<Item>.CreateOk(result);
		}
	}
}
