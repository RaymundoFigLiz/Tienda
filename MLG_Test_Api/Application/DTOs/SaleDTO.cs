namespace MLG_Test.Application.DTOs
{
	public class SaleDTO
	{
		public required int Id { get; set; }
		public required string ClientFullName { get; set; }
		public required string ItemName { get; set; }
		public required string StoreName { get; set; }
		public required float Price { get; set; }
		public required DateTime Date { get; set; }
	}

	public class CreateSaleDTO
	{
		public required int ClientId { get; set; }
		public required List<int> StoreItemIds { get; set; }
	}

	public class UpdateSaleDTO
	{
		public string? ClientId { get; set; }
		public string? StoreItemId { get; set; }
	}
}
