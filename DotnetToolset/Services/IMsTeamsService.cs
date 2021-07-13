using DotnetToolset.Models.MsTeams;
using System;

namespace DotnetToolset.Services
{
	public interface IMsTeamsService
	{
		/// <summary>
		/// Creates a message card
		/// </summary>
		/// <param name="themeColor">Color for the message card</param>
		/// <param name="title">Title for the message card</param>
		/// <param name="text">Text for the message card</param>
		/// <param name="potentialActions">Potential actions to be added to the message card</param>
		/// <returns>A deserialized message card</returns>
		MessageCard CreateMesssageCard(ThemeColor themeColor, string title, string text, PotentialAction[] potentialActions);

		/// <summary>
		/// Creates an error message card with a link button
		/// </summary>
		/// <param name="title">Title for the message card</param>
		/// <param name="text">Text for the message card</param>
		/// <param name="linkText">Text for the link button</param>
		/// <param name="linkUrl">URL for the link button</param>
		/// <returns>A deserialized message card</returns>
		MessageCard CreateMessageCardErrorLink(string title, string text, string linkText, string linkUrl);

		/// <summary>
		/// Sends a message card to a web hook
		/// </summary>
		/// <param name="messageCard">Message card to be sent</param>
		/// <param name="webHookUri">Web hook to send the message card to</param>
		/// <returns>True if sent successfully</returns>
		bool SendMessageCard(MessageCard messageCard, Uri webHookUri);
	}
}
