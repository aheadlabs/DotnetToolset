namespace DotnetToolset.Settings
{
	/// <summary>
	/// Parser for Microsoft Teams' configuration section
	/// </summary>
	public class MsTeams
	{
		/// <summary>
		/// Secret name where the URL for the web hook is stored
		/// </summary>
		public string WebHookUrlSecretName { get; set; }
	}
}
