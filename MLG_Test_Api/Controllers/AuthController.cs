using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MLG_Test.Application.DTOs;
using MLG_Test.Application.Services;
using MLG_Test.Core.ClientUtilities;

namespace MLG_Test.Controllers
{
	[ApiController]
	[Route("api/auth")]
	[AllowAnonymous]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("login")]
		public async Task<Wrapper<LoginResponseDTO>> Login(LoginRequestDTO dto)
		{
			var result = await _authService.LoginAsync(dto);
			return Wrapper<LoginResponseDTO>.CreateOk(result);
		}

		[HttpPost("register")]
		public async Task<Wrapper<RegisterResponseDTO>> Register(RegisterRequestDTO dto)
		{
			var result = await _authService.RegisterAsync(dto);
			return Wrapper<RegisterResponseDTO>.CreateOk(result);
		}
	}
}

