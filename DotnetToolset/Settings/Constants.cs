namespace DotnetToolset.Settings
{
	public static class Constants
	{
		public static class EnvironmentVariables
		{
			public const string AspnetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";
			public const string DotNetRunningInContainer = "DOTNET_RUNNING_IN_CONTAINER";
		}

		public static class Environments
		{
			public const string Local = "local";
			public const string Development = "development";
			public const string Staging = "staging";
			public const string Production = "production";
		}

        public const int Mask = 1000;
    }
}