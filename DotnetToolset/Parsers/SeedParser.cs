using CsvHelper;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DotnetToolset.Parsers
{
  public class SeedParser
  {
	  /// <summary>
	  /// Gets the data to be inserted
	  /// </summary>
	  /// <typeparam name="T">Entity type to get data for</typeparam>
	  /// <returns>Data enumerable</returns>
	  public IEnumerable<T> GetSeeds<T>()
	  {
		  List<T> recordsList;

		  string csvPath = GetCsvPath(typeof(T).Name.Replace("Data", string.Empty));

		  using (StreamReader reader = new StreamReader(csvPath))
		  {
			  using (CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture))
			  {
				  recordsList = csv.GetRecords<T>().ToList();
			  }
		  }

		  return recordsList;
	  }

	  /// <summary>
	  /// Gets the path to the assembly
	  /// </summary>
	  /// <returns></returns>
	  public string GetAssemblyPath() => Path.GetDirectoryName(Assembly.GetAssembly(typeof(SeedParser))?.Location);

	  /// <summary>
	  /// Gets the data set name from the settings.json file
	  /// </summary>
	  /// <returns></returns>
	  public string GetDataSetName()
	  {
		  string settingsPath = Path.Combine(GetAssemblyPath(), "settings.json");

		  JObject jsonJObject = JObject.Parse(File.ReadAllText(settingsPath));
		  return (string)jsonJObject["data-set"];
	  }

	  /// <summary>
	  /// Gets the path to the entity
	  /// </summary>
	  /// <param name="entityName"></param>
	  /// <returns></returns>
	  public string GetCsvPath(string entityName) => Path.Combine(GetAssemblyPath(), "Data", GetDataSetName(), $"{entityName}.csv");
  }
}
