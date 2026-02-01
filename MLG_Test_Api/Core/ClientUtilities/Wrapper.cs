using System.Net;

namespace MLG_Test.Core.ClientUtilities
{
	public class Wrapper<T>
	{
		public HttpStatusCode StatusCode { get; set; }
		public string Message { get; set; }
		public T Result { get; set; }

		public static Wrapper<T> CreateOk(T? result = default, string? message = null)
		{
			return new Wrapper<T>
			{
				StatusCode = HttpStatusCode.OK,
				Message = message ?? Messages.Ok,
				Result = result,
			};
		}
	}
}
