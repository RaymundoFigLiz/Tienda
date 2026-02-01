namespace MLG_Test.Application.DTOs
{
	public class StoreItemDTO
	{
		public required int Id { get; set; }
		public required int ItemId { get; set; }
		public required string ItemCode { get; set; }
		public required string ItemName { get; set; }
		public required string ItemImage { get; set; }
		public required int StoreId { get; set; }
		public required string StoreName { get; set; }
		public required float Price { get; set; }
		public required int Stock { get; set; }
	}

	public class CreateStoreItemDTO
	{
		public required int ItemId { get; set; }
		public required int StoreId { get; set; }
		public required float Price { get; set; }
		public required int Stock{ get; set; }
	}

	public class UpdateStoreItemDTO
	{
		public int? ItemId { get; set; }
		public int? StoreId { get; set; }
		public float? Price { get; set; }
		public int? Stock { get; set; }
	}
}
