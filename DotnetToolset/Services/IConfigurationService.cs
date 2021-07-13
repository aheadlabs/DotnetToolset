using Microsoft.Extensions.Configuration;

namespace DotnetToolset.Services
{
	/// <summary>
	/// Gets the app configuration based on JSON files and environment variables
	/// </summary>
	public interface IConfigurationService
	{
		/// <summary>
		/// Gets the configuration using the current directory
		/// </summary>
		/// <returns></returns>
		IConfiguration GetConfiguration();

		/// <summary>
		/// Gets the configuration using a provided base path
		/// </summary>
		/// <param name="basePath"></param>
		/// <returns></returns>
		IConfiguration GetConfiguration(string basePath);
	}
}
