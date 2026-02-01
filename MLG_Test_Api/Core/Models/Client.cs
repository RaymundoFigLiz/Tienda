using System.Text.Json.Serialization;

namespace MLG_Test.Core.Models
{
	public class Client : BaseModel
	{
		public int AddressId { get; set; }

		// Navigation
		public User User { get; set; } = null!;
		public Address Address { get; set; } = null!;

		[JsonIgnore]
		public ICollection<Sale> Sales { get; } = new List<Sale>();
	}
}
