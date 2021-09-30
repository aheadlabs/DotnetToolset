using DotnetToolset.Enums;
using DotnetToolset.Resources;
using Microsoft.Extensions.Logging;
using System;
using System.Text.RegularExpressions;

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
			if (character.Length > 1 || string.IsNullOrEmpty(character))
			{
				throw new ArgumentException(Literals.b_FillingCharacterLengthMustBeOne, nameof(character));
			}

			int numberOfCharactersToFill = fillLength - data.ToString().Length;
			string fillString = numberOfCharactersToFill > 0 ? new string(char.Parse(character), numberOfCharactersToFill) : string.Empty;
			return fillingType == FillingType.Left ? $"{fillString}{data}" : $"{data}{fillString}";
		}

        /// <inheritdoc />
        public string GenerateSlug (string phrase) 
        { 
            string str = RemoveAccent(phrase).ToLower(); 
            // Invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", ""); 
            // Convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim(); 
            // Cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();   
            str = Regex.Replace(str, @"\s", "-"); // Hyphens   
            return str; 
        } 

        /// <inheritdoc />
        public string RemoveAccent(string txt) 
        { 
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt); 
            return System.Text.Encoding.ASCII.GetString(bytes); 
        }
	}
}