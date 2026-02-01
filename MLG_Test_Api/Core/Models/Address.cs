
using System.Text.Json.Serialization;

namespace MLG_Test.Core.Models
{
	public class Address : BaseModel
	{
		public required string Description { get; set; }
		public required string PostalCode { get; set; }
		public required string ExternalNumber { get; set; }
		public string? InternalNumber { get; set; }

		// Navigation
		[JsonIgnore]
		public ICollection<Client> Clients { get; } = new List<Client>();
		
		[JsonIgnore]
		public ICollection<Store> Stores { get; } = new List<Store>();
	}
}
