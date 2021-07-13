using Newtonsoft.Json;

namespace DotnetToolset.Models.MsTeams
{
	/// <summary>
	/// Legacy actionable message card
	/// </summary>
	/// <remarks>https://docs.microsoft.com/en-us/outlook/actionable-messages/message-card-reference</remarks>
	public class MessageCard
	{
		[JsonProperty(PropertyName = "@context")]
		public string Context { get; set; }
		[JsonProperty(PropertyName = "@type")]
		public string Type { get; set; }
		public string ThemeColor { get; set; }
		public string Title { get; set; }
		public string Text { get; set; }
		[JsonProperty(PropertyName = "potentialAction")]
		public PotentialAction[] PotentialActions { get; set; }
	}
}
