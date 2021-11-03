using DotnetToolset.Enums;
using DotnetToolset.Resources;
using Microsoft.Extensions.Logging;
using System;
using System.Text.RegularExpressions;
using Constants = DotnetToolset.Settings.Constants;


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
		public string Base64Decode(string base64EncodedData)
		{
			byte[] bytes = Convert.FromBase64String(base64EncodedData);
			return System.Text.Encoding.UTF8.GetString(bytes);
		}

		/// <inheritdoc />
		public string Base64DecodeToUtf7(string base64EncodedData)
		{
			byte[] bytes = Convert.FromBase64String(base64EncodedData);
			return System.Text.Encoding.UTF7.GetString(bytes);
		}

		/// <inheritdoc />
		public string Base64Encode(string plainTextData)
		{
			byte[] bytes = System.Text.Encoding.UTF8.GetBytes(plainTextData);
			return System.Convert.ToBase64String(bytes);
		}

		/// <inheritdoc />
		public string Base64EncodeFromUtf7(string plainUtf7TextData)
		{
			byte[] bytes = System.Text.Encoding.UTF7.GetBytes(plainUtf7TextData);
			return System.Convert.ToBase64String(bytes);
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
		public string GetSlug(int id)
        {
            return Convert.ToString(id + Constants.Mask, 8).Replace("0o","");
        }

        /// <inheritdoc />
        public string NormalizeString (string phrase) 
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
