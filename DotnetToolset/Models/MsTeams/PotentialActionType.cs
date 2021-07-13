namespace DotnetToolset.Models.MsTeams
{
	/// <summary>
	/// Types of potential actions for message cards
	/// </summary>
	/// <remarks>https://docs.microsoft.com/en-us/outlook/actionable-messages/message-card-reference#actions</remarks>
	public enum PotentialActionType
	{
		/// <summary>
		/// Opens a URI in a separate browser or app
		/// </summary>
		OpenUri,

		/// <summary>
		/// Makes a call to an external web service or endpoint
		/// </summary>
		HttpPost,

		/// <summary>
		/// Presents additional UI that contains one or more inputs, along with associated actions that can be either OpenUri or HttpPOST types.
		/// </summary>
		ActionCard,

		/// <summary>
		/// Opens an Outlook add-in task pane
		/// </summary>
		InvokeAddInCommand
	}
}