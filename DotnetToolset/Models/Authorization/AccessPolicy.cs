using System.Collections.Generic;

namespace DotnetToolset.Models.Authorization
{
    /// <summary>
    /// Maps an access policy from appsettings*.json
    /// </summary>
    public class AccessPolicy
    {
        /// <summary>
        /// Access policy name
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// List of permissions for the policy
        /// </summary>
        public IEnumerable<string>? Permissions { get; set; }
    }
}
