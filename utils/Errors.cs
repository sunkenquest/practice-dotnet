namespace practice_dotnet.utils
{
	public static class Errors
	{
		// Not Found Error
		public static string NotFound(string entityName, object key)
		{
			return $"{entityName} with key '{key}' was not found.";
		}

		// Internal Server Error
		public static string InternalError(string action)
		{
			return $"An unexpected error occurred while trying to {action}. Please try again later.";
		}

		// Validation Error
		public static string ValidationError(string fieldName, string issue)
		{
			return $"The value for {fieldName} is invalid: {issue}.";
		}

		// Unauthorized Error
		public static string Unauthorized(string action)
		{
			return $"You are not authorized to {action}. Please check your credentials.";
		}

		// Forbidden Error
		public static string Forbidden(string action)
		{
			return $"You do not have permission to {action}.";
		}

		// Bad Request Error
		public static string BadRequest(string issue)
		{
			return $"Bad request: {issue}. Please check your input.";
		}

		// Conflict Error
		public static string Conflict(string entityName, object key)
		{
			return $"There is a conflict with {entityName} that has the key '{key}'.";
		}

		// Generic Error Message for Catch-All Cases
		public static string GenericError()
		{
			return "An unexpected error occurred. Please try again later.";
		}
	}
}
