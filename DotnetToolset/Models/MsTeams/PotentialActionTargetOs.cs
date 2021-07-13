using System.Runtime.Serialization;

namespace DotnetToolset.Models.MsTeams
{
	/// <summary>
	/// Operating system targets for potential actions in message cards
	/// </summary>
	/// <remarks>https://docs.microsoft.com/en-us/outlook/actionable-messages/message-card-reference#actions</remarks>
	public enum PotentialActionTargetsOs
	{
		/// <summary>
		/// Any operating system
		/// </summary>
		[EnumMember(Value = "default")]
		Default,

		/// <summary>
		/// Windows operating system
		/// </summary>
		[EnumMember(Value = "windows")]
		Windows,

		/// <summary>
		/// iOS operating system
		/// </summary>
		[EnumMember(Value = "ios")]
		Ios,

		/// <summary>
		/// Android operating system
		/// </summary>
		[EnumMember(Value = "android")]
		Android
	}
}
