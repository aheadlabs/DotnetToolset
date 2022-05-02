namespace DotnetToolset.Models.Auth0
{
    /// <summary>
    /// Auth0 settings mappings
    /// </summary>
    public class Auth0Settings
    {
        /// <summary>
        /// Auth0 OAuth token endpoint
        /// </summary>
        public string OAuthTokenEndpoint { get; set; }

        /// <summary>
        /// Auth0 domain
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Auth0 audiences
        /// </summary>
        public Auth0SettingsAudience[] Audiences { get; set; }
    }
}
