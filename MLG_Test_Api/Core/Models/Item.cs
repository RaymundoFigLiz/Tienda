
using System.Text.Json.Serialization;

namespace MLG_Test.Core.Models
{
	public class Item : BaseModel
	{
		public required string Code { get; set; }
		public required string Description { get; set; }
		public string? Image { get; set; }

		// Navigation

		[JsonIgnore] 
		public ICollection<StoreItem> StoreItems { get; } = new List<StoreItem>();
	}
}
