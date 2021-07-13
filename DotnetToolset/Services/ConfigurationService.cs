using Microsoft.Extensions.Configuration;
using System.IO;

namespace DotnetToolset.Services
{
	/// <inheritdoc />
	public class ConfigurationService : IConfigurationService
	{
		private IEnvironmentService EnvironmentService { get; }

		public ConfigurationService(IEnvironmentService environmentService) => EnvironmentService = environmentService;

		/// <summary>
		/// Gets the configuration
		/// </summary>
		/// <returns>IConfiguration compatible instance</returns>
		public IConfiguration GetConfiguration() => GetConfiguration(Directory.GetCurrentDirectory());

		/// <summary>
		/// Gets the configuration
		/// </summary>
		/// <param name="basePath">Base path for binaries</param>
		/// <returns>IConfiguration compatible instance</returns>
		public IConfiguration GetConfiguration(string basePath) =>
			new ConfigurationBuilder()
				.SetBasePath(basePath)

				// General settings
				.AddJsonFile("appsettings.json", false, true)

				// Environmental settings override general settings
				.AddJsonFile($"appsettings.{EnvironmentService.EnvironmentName}.json", true, true)

				// Environment variables set at machine or cloud platform override previous settings
				.AddEnvironmentVariables()

				.Build();
	}
}
