using System.Collections.Generic;

namespace DotnetToolset.Models.Aws
{
    /// <summary>
    /// AWS SES service settings mappings
    /// </summary>
    public class AwsSesSettings
    {
        /// <summary>
        /// AWS SES configuration set name
        /// </summary>
        public string ConfigurationSet { get; set; }

        /// <summary>
        /// Default sender e-mail address
        /// </summary>
        public string DefaultSender { get; set; }

        /// <summary>
        /// Lists of e-mai recipients
        /// </summary>
        public List<AwsSesEmailDestination> EmailRecipients { get; set; }
    }
}
