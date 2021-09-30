using DotnetToolset.Enums;

namespace DotnetToolset.Services
{
	public interface IFormatService
	{
		/// <summary>
		/// Fills the data using the specified character (or '0' by default)
		/// </summary>
		/// <param name="data">Data to be filled</param>
		/// <param name="fillLength">Total length of the resulting string</param>
		/// <param name="character">Character to be used for filling (defaults to '0')</param>
		/// <param name="fillingType">Left or right filling</param>
		/// <returns></returns>
		string Fill(int data, int fillLength, string character = "0", FillingType fillingType = FillingType.Left);
	}
}
