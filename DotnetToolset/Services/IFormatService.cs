﻿using DotnetToolset.Enums;

namespace DotnetToolset.Services
{
    public interface IFormatService
    {
        /// <summary>
        /// Decodes Base64 encoded text to plain ASCII text
        /// </summary>
        /// <param name="base64EncodedData">Base64 encoded text</param>
        /// <returns></returns>
		string Base64DecodeToUtf7(string base64EncodedData);

        /// <summary>
        /// Decodes Base64 encoded text to plain UTF-8 text
        /// </summary>
        /// <param name="base64EncodedData">Base64 encoded text</param>
        /// <returns></returns>
		string Base64Decode(string base64EncodedData);

        /// <summary>
        /// Encodes plain UTF-8 text to Base64 encoded text
        /// </summary>
        /// <param name="plainTextData">Plain text</param>
        /// <returns></returns>
        string Base64Encode(string plainTextData);

        /// <summary>
        /// Encodes plain ASCII text to Base64 encoded text
        /// </summary>
        /// <param name="plainAsciiTextData">Plain text</param>
        /// <returns></returns>
        string Base64EncodeFromUtf7(string plainUtf7TextData);

        /// <summary>
        /// Fills the data using the specified character (or '0' by default)
        /// </summary>
        /// <param name="data">Data to be filled</param>
        /// <param name="fillLength">Total length of the resulting string</param>
        /// <param name="character">Character to be used for filling (defaults to '0')</param>
        /// <param name="fillingType">Left or right filling</param>
        /// <returns></returns>
        string Fill(int data, int fillLength, string character = "0", FillingType fillingType = FillingType.Left);

        /// <summary>
        /// Performs a masking of the value passed applying octal conversion and adding a mask
        /// </summary>
        /// <returns> String processed </returns>string RemoveAccent(string txt);
        string GetSlug(int id);

        /// <summary>
        /// Slugifies a text to be suitable for url use.
        /// </summary>
        /// <param name="phrase">String to be processed</param>
        /// <param name="maxLength">Maximum length of the normalized string</param>
        /// <returns> Slug of the phrase </returns>
        string NormalizeString(string phrase, int maxLength = 100);

        /// <summary>
        /// Removes accents from a string
        /// </summary>
        /// <returns> String processed </returns>
        string RemoveAccent(string txt);
    }
}
