using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using MLG_Test.Application.DTOs;
using MLG_Test.Core.ClientUtilities;
using MLG_Test.Core.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MLG_Test.Application.Services
{
	public interface IAuthService
	{
		Task<LoginResponseDTO> LoginAsync(LoginRequestDTO dto);
		Task<RegisterResponseDTO> RegisterAsync(RegisterRequestDTO dto);
	}

	public class AuthService : IAuthService
	{
		private readonly IGenericService<User> _userService;
		private readonly IGenericService<Client> _clientService;
		private readonly IConfiguration _config;
		private readonly IMapper _mapper;

		public AuthService(IGenericService<User> userService, IGenericService<Client> clientService, IConfiguration config, IMapper mapper)
		{
			_userService = userService;
			_clientService = clientService;
			_config = config;
			_mapper = mapper;
		}

		public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO dto)
		{
			var user = (await _userService.GetAllAsync()).FirstOrDefault(u => u.Email == dto.Email && u.IsActive);

			if (user == null)
				throw new UnauthorizedAccessException(Messages.WrongCredentials);

			var isPasswordCorrect = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
			if (!isPasswordCorrect)
				throw new UnauthorizedAccessException(Messages.WrongCredentials);

			return GenerateToken(user);
		}

		private LoginResponseDTO GenerateToken(User user)
		{
			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
				new Claim("email", user.Email),
				new Claim("name", user.Name),
				new Claim("firstLastName", user.FirstLastName),
				new Claim("secondLastName", user.SecondLastName ?? ""),
				new Claim("roleId", user.RoleId.ToString()),
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var expires = DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:ExpiresMinutes"]!));

			var token = new JwtSecurityToken(
				issuer: _config["Jwt:Issuer"],
				audience: _config["Jwt:Audience"],
				claims: claims,
				expires: expires,
				signingCredentials: creds
			);

			return new LoginResponseDTO
			{
				Token = new JwtSecurityTokenHandler().WriteToken(token),
				ExpiresAt = expires
			};
		}

		public async Task<RegisterResponseDTO> RegisterAsync(RegisterRequestDTO registerDto)
		{
			var exists = (await _userService.GetAllAsync()).Any(u => u.Email == registerDto.Email);

			if (exists)
				throw new InvalidOperationException(Messages.EmailInUse);

			var hash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

			var client = _mapper.Map<Client>(registerDto);
			client.User.PasswordHash = hash;

			var createdClient = await _clientService.CreateAsync(client);

			return new RegisterResponseDTO
			{
				ClientId = createdClient.Id,
				UserId = createdClient.User.Id,
				Name = createdClient.User.Name,
				FirstLastName = createdClient.User.FirstLastName,
				SecondLastName = createdClient.User.SecondLastName,
				RoleId = createdClient.User.RoleId,
				Email = createdClient.User.Email
			};
		}
	}
}
