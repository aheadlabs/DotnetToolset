namespace DotnetToolset.Models.Auth0
{
    /// <summary>
    /// Auth0 audiences settings mappings
    /// </summary>
    public class Auth0SettingsAudience
    {
        public string Audience { get; set; }
        public string AuthenticationScheme { get; set; }
        public string ClientId { get; set; }
        public string Description { get; set; }
    }
}
