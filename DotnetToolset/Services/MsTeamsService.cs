using DotnetToolset.Models.MsTeams;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using System;
using System.Net;

namespace DotnetToolset.Services
{
	public class MsTeamsService : IMsTeamsService
	{
		private readonly string _messageCardContext = "https://schema.org/extensions";
		private readonly string _type = "MessageCard";

		/// <inheritdoc />
		public MessageCard CreateMesssageCard(ThemeColor themeColor, string title, string text, PotentialAction[] potentialActions)
		{
			return new MessageCard
			{
				Context = _messageCardContext,
				Type = _type,
				ThemeColor = GetRgbCardColor(themeColor),
				Title = title,
				Text = text,
				PotentialActions = potentialActions
			};
		}

		/// <inheritdoc />
		public MessageCard CreateMessageCardErrorLink(string title, string text, string linkText, string linkUrl)
		{
			return new MessageCard
			{
				Context = _messageCardContext,
				Type = _type,
				ThemeColor = GetRgbCardColor(ThemeColor.Error),
				Title = title,
				Text = text,
				PotentialActions = new []
				{
					new PotentialAction
					{
						Type = PotentialActionType.OpenUri,
						Name = linkText,
						Targets = new []
						{
							new Target
							{
								Os = PotentialActionTargetsOs.Default,
								Uri = linkUrl
							}
						}
					}
				}
			};
		}

		/// <inheritdoc />
		public bool SendMessageCard(MessageCard messageCard, Uri webHookUri)
		{
			// Set serializer configuration
			JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
			{
				ContractResolver = new DefaultContractResolver
				{
					NamingStrategy = new CamelCaseNamingStrategy()
				}
			};

			// Serialize message card
			string jsonMessageCard = JsonConvert.SerializeObject(messageCard, jsonSerializerSettings);

			// Send to Microsoft Teams
			RestService<object> restService = new RestService<object>
			{
				BaseUrl = webHookUri.AbsoluteUri
			};
			IRestResponse<object> response = restService.Post("", jsonMessageCard);

			// Return true if success
			return response.StatusCode == HttpStatusCode.OK;
		}

		/// <summary>
		/// Returns color in RGB format
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		private string GetRgbCardColor(ThemeColor color)
		{
			string debug = "aaaaaa";
			string error = "ff0000";
			string info = "0000ff";
			string success = "00ff00";
			string warning = "ff0000";

			switch (color)
			{
				case ThemeColor.Debug:
					return debug;
				case ThemeColor.Error:
					return error;
				case ThemeColor.Info:
					return info;
				case ThemeColor.Success:
					return success;
				case ThemeColor.Warning:
					return warning;
				default:
					return error;
			}
		}
	}
}
