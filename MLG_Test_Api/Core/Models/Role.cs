namespace MLG_Test.Core.Models
{
	public class Role : BaseModel
	{
		public required string Description { get; set; }

		// Navigation
		public ICollection<User> Users { get; } = new List<User>();
	}
}
