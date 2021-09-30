using DotnetToolset.Enums;
using DotnetToolset.Resources;
using Microsoft.Extensions.Logging;
using System;

namespace DotnetToolset.Services
{
	public class FormatService : IFormatService
	{
		private readonly ILogger<FormatService> _logger;

		public FormatService(ILogger<FormatService> logger)
		{
			_logger = logger;
		}


		/// <inheritdoc />
		public string Fill(int data, int fillLength, string character = "0", FillingType fillingType = FillingType.Left)
		{
			if (character.Length > 1 || String.IsNullOrEmpty(character))
			{
				throw new ArgumentException(Literals.b_FillingCharacterLengthMustBeOne, nameof(character));
			}

			int numberOfCharactersToFill = fillLength - data.ToString().Length;
			string fillString = numberOfCharactersToFill > 0 ? new string(char.Parse(character), numberOfCharactersToFill) : string.Empty;
			return fillingType == FillingType.Left ? $"{fillString}{data}" : $"{data}{fillString}";
		}
	}
}