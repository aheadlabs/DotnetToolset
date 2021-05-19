namespace DotnetToolset.Settings
{
	/// <summary>
	/// Use this as a template to define your defaults in your project
	/// </summary>
	public class DefaultDatabaseColumnTypes
	{
		/// <summary>
		/// Default column type for CLR string equivalent types in your DBMS
		/// <example>varchar(200)</example>
		/// </summary>
		public string StringType { get; set; }
	}
}
