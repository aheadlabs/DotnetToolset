using System.Globalization;

namespace Toolbox.ExtensionMethods
{
    /// <summary>
    /// Provides extension methods for String
    /// </summary>
    public static class StringExt
    {
        /// <summary>
        /// Parses only one parameter value of a literal (Useful when the literal has only one parameter to replace)
        /// </summary>
        /// <param name="text">Literal to be interpreted</param>
        /// <param name="parameter">Parameter to be parsed</param>
        /// <returns>Parsed text</returns>
        public static string ParseParameter(this string text, string parameter)
        {
            if (text != null && parameter != null)
            {
                text = string.Format(CultureInfo.CurrentCulture, text, parameter);
            }

            return text;
        }

        /// <summary>
        /// Parses all parameter values of a literal
        /// </summary>
        /// <param name="text">Literal to be interpreted</param>
        /// <param name="parameters">Parameters to be parsed</param>
        /// <returns>Parsed text</returns>
        public static string ParseParameters(this string text, object[] parameters)
        {
            if (text != null && parameters != null)
            {
                text = string.Format(CultureInfo.CurrentCulture, text, parameters);
            }

            return text;
        }
    }
}
