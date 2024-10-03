namespace practice_dotnet.utils
{
	public static class Success
	{
		// Success for entity creation
		public static string Created(string entityName)
		{
			return $"{entityName} has been successfully created.";
		}

		// Success for entity update
		public static string Updated(string entityName)
		{
			return $"{entityName} has been successfully updated.";
		}

		// Success for entity deletion (soft delete or hard delete)
		public static string Deleted(string entityName)
		{
			return $"{entityName} has been successfully deleted.";
		}

		// Success for any generic operation
		public static string OperationCompleted(string action)
		{
			return $"{action} completed successfully.";
		}

		// Success for retrieval of data
		public static string DataRetrieved(string entityName)
		{
			return $"{entityName} data retrieved successfully.";
		}

		// Generic success message for other actions
		public static string GenericSuccess()
		{
			return "Operation completed successfully.";
		}
	}
}
