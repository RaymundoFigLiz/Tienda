namespace MLG_Test.Application.DTOs
{
	public class LoginRequestDTO
	{
		public required string Email { get; set; }
		public required string Password { get; set; }
	}

	public class LoginResponseDTO
	{
		public required string Token { get; set; }
		public DateTime ExpiresAt { get; set; }
	}
}
