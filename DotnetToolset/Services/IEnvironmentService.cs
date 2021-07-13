namespace DotnetToolset.Services
{
	/// <summary>
	/// Interacts with the environment
	/// </summary>
	public interface IEnvironmentService
	{
		string EnvironmentName { get; set; }
	}
}
