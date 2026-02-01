using System.ComponentModel.DataAnnotations;

namespace MLG_Test.Application.DTOs
{
	public class RegisterRequestDTO
	{
		public required string Name { get; set; }
		public required string FirstLastName { get; set; }
		public string? SecondLastName { get; set; }
		public int RoleId { get; set; }
		public required string AddressName { get; set; }
		public required string PostalCode { get; set; }
		public required string InternalNumber { get; set; }
		public string? ExternalNumber { get; set; }

		[EmailAddress]
		public required string Email { get; set; }

		[MinLength(8)]
		public required string Password { get; set; }
	}

	public class RegisterResponseDTO
	{
		public int UserId { get; set; }
		public int ClientId { get; set; }
		public required string Name { get; set; }
		public required string FirstLastName { get; set; }
		public string? SecondLastName { get; set; }
		public int RoleId { get; set; }
		public string Email { get; set; } = null!;
	}
}
