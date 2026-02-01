namespace MLG_Test.Application.DTOs
{
	public class StoreDTO
	{
		public required int Id { get; set; }
		public required string Name { get; set; }
		public required string Address { get; set; }
		public required string PostalCode { get; set; }
		public required string InternalNumber { get; set; }
		public string? ExternalNumber { get; set; }
	}

	public class CreateStoreDTO
	{
		public required string Name { get; set; }
		public required string Address { get; set; }
		public required string PostalCode { get; set; }
		public required string InternalNumber { get; set; }
		public string? ExternalNumber { get; set; }
	}

	public class UpdateStoreDTO
	{
		public string? Name { get; set; }
		public string? Address { get; set; }
		public string? PostalCode { get; set; }
		public string? InternalNumber { get; set; }
		public string? ExternalNumber { get; set; }
	}
}
