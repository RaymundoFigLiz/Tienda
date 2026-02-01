namespace MLG_Test.Core.ClientUtilities
{
	public static class Messages
	{
		public static string Ok => "Operación completada con éxito.";
		public static string SingularDataObtained => "Registro obtenido.";
		public static string PluralDataObtained => "Registros obtenidos.";
		public static string DataSaved => "Registro guardado.";
		public static string DataUpdated => "Registro actualizado.";
		public static string EmptyData => "No existen registros.";

		public static string WrongCredentials => "Credenciales inválidas.";
		public static string EmailInUse => "El email ya está registrado.";


		public static string Error400 => "Error 400, la solicitud es inválida.";
		public static string Error403 => "Error 403, no estás autorizado para esta acción.";
		public static string Error404 => "Error 404, el registro que buscas no existe.";
		public static string Error422 => "Error 422, la solicitud no contiene datos requeridos o no cumplen con las reglas de validación.";
		public static string Error500 => "Error 500, ocurrió un error.";
	}
}
