using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace DotnetToolset.Models.MsTeams
{
	public class PotentialAction
	{
		[JsonProperty(PropertyName = "@type")]
		[JsonConverter(typeof(StringEnumConverter))]
		public PotentialActionType Type { get; set; }
		public string Name { get; set; }
		public Target[] Targets { get; set; }
	}
}
