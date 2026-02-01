namespace MLG_Test.Core.Models
{
	public abstract class BaseModel
	{
		public int Id { get; set; }
		public bool IsActive { get; set; } = true;
	}
}
