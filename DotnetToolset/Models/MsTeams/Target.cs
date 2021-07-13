using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DotnetToolset.Models.MsTeams
{
	public class Target
	{
		[JsonConverter(typeof(StringEnumConverter))]
		public PotentialActionTargetsOs Os { get; set; }
		public string Uri { get; set; }
	}
}
