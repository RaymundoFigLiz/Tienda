using System.Text.Json.Serialization;

namespace MLG_Test.Core.Models
{
	public class Store : BaseModel
	{
		public required string Name { get; set; }
		public int AddressId { get; set; }

		// Navigation
		public Address Address { get; set; } = null!;

		[JsonIgnore]
		public ICollection<StoreItem> StoreItems { get; } = new List<StoreItem>();
	}
}
