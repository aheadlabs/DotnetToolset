using DotnetToolset.Settings;
using System;

namespace DotnetToolset.Services
{
	/// <inheritdoc />
	public class EnvironmentService : IEnvironmentService
	{
		public string EnvironmentName { get; set; }

		/// <summary>
		/// Sets the environment name
		/// </summary>
		public EnvironmentService()
		{
			string aspnetcoreEnvironment = Environment.GetEnvironmentVariable(Constants.EnvironmentVariables.AspnetCoreEnvironment);
			EnvironmentName = (aspnetcoreEnvironment == null || aspnetcoreEnvironment == Constants.Environments.Local)
				? Constants.Environments.Local
				: aspnetcoreEnvironment;
		}
	}
}
