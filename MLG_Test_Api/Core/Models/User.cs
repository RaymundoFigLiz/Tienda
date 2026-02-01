namespace MLG_Test.Core.Models
{
	public class User : BaseModel
	{
		public required string Name { get; set; }
		public required string FirstLastName { get; set; }
		public string? SecondLastName { get; set; }
		public required string Email { get; set; }
		public required string PasswordHash { get; set; }
		public required int RoleId { get; set; }

		// Navigation
		public Role Role { get; set; } = null!;
	}
}
