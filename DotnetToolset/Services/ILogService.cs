using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace DotnetToolset.Services
{
	/// <summary>
	/// Provides tools for logging
	/// </summary>
	public interface ILogService<T>
	{
		/// <summary>
		/// Logs a tree of exceptions
		/// </summary>
		/// <param name="exception">Exception that contains the exception tree (inner exceptions)</param>
		/// <param name="logLevel">Log level</param>
		/// <param name="exceptionsLimit">Exception depth that will be logged to avoid infinite loops</param>
		void LogExceptionTree(Exception exception, LogLevel logLevel = LogLevel.Information, int exceptionsLimit = 4);

		/// <summary>
		/// Extracts the exceptions to a list
		/// </summary>
		/// <param name="exception">Exception that contains the exception tree (inner exceptions)</param>
		/// <param name="exceptionsLimit">Exception depth that will be logged to avoid infinite loops</param>
		/// <returns>List of Exception objects</returns>
		List<Exception> ExtractExceptionsListFromExceptionsTree(Exception exception, int exceptionsLimit = 4);
	}
}
