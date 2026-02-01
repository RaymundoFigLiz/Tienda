using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MLG_Test.Application.DTOs;
using MLG_Test.Application.Services;
using MLG_Test.Core.ClientUtilities;
using MLG_Test.Core.Models;

namespace MLG_Test.Controllers
{
	[ApiController]
	[Route("api/sale")]
	[Authorize]
	public class SaleController : ControllerBase
	{
		private readonly ISaleService _saleService;
		public SaleController(ISaleService saleService)
		{
			_saleService = saleService;
		}

		[HttpGet]
		public async Task<Wrapper<IEnumerable<SaleDTO>>> GetAll()
		{
			var result = await _saleService.GetAllAsync();
			return Wrapper<IEnumerable<SaleDTO>>.CreateOk(result);
		}

		[HttpPost]
		public async Task<Wrapper<IEnumerable<Sale>>> Create(CreateSaleDTO createDto)
		{
			var result = await _saleService.CreateAsync(createDto);
			return Wrapper<IEnumerable<Sale>>.CreateOk(result);
		}
	}
}
