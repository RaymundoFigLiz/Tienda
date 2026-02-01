using System.Text.Json.Serialization;

namespace MLG_Test.Core.Models
{
	public class StoreItem : BaseModel
	{
		public int StoreId { get; set; }
		public int ItemId { get; set; }
		public float Price { get; set; }
		public int Stock { get; set; }

		// Navigation

		[JsonIgnore]
		public ICollection<Sale> Sales { get; } = new List<Sale>();
		public Store Store { get; set; } = null!;
		public Item Item { get; set; } = null!;
	}
}
