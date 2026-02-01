namespace MLG_Test.Core.Models
{
	public class Sale : BaseModel
	{
		public int ClientId { get; set; }
		public int StoreItemId { get; set; }
		public float Price { get; set; }
		public DateTime Date { get; set; }

		// Navigation
		public Client Client { get; set; } = null!;
		public StoreItem StoreItem { get; set; } = null!;
	}
}
