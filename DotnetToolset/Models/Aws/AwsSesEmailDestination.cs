using System.Collections.Generic;

namespace DotnetToolset.Models.Aws
{
    /// <summary>
    /// AWS SES service destination that includes 3 lists: To, Cc and Bcc
    /// </summary>
    public class AwsSesEmailDestination
    {
        public string Name { get; set; }
        public List<string> To { get; set; }
        public List<string> Cc { get; set; }
        public List<string> Bcc { get; set; }
    }
}
