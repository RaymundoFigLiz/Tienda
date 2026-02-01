namespace MLG_Test.Application.DTOs
{
	public class CreateItemDTO
	{
		public required string Code { get; set; }
		public required string Description { get; set; }
		public string? Image { get; set; }
	}

	public class UpdateItemDTO
	{
		public string? Code { get; set; }
		public string? Description { get; set; }
		public string? Image { get; set; }
	}
}
