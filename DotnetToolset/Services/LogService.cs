using DotnetToolset.ExtensionMethods;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Res = DotnetToolset.Resources.Literals;

namespace DotnetToolset.Services
{
	/// <inheritdoc />
	public class LogService<T> : ILogService<T>
	{
		private readonly ILogger<T> _logger;

		public LogService(ILogger<T> logger)
		{
			_logger = logger;
		}

		/// <inheritdoc />
		public void LogExceptionTree(Exception exception, LogLevel logLevel = LogLevel.Information, int exceptionsLimit = 4)
		{
			List<Exception> exceptionsList = ExtractExceptionsListFromExceptionsTree(exception, exceptionsLimit);

			for (var index = 0; index < exceptionsList.Count; index++)
			{
				Exception currentException = exceptionsList[index];
				_logger.Log(logLevel, currentException, Res.p_LoggingExceptionTree.ParseParameters(new object[] { index + 1, exceptionsList.Count }));
			}
		}

		/// <inheritdoc />
		public List<Exception> ExtractExceptionsListFromExceptionsTree(Exception exception, int exceptionsLimit = 4)
		{
			List<Exception> exceptionsList = new List<Exception>();
			Exception currentException = exception;
			int currentIteration = 1;

			while (currentException != null && currentIteration <= exceptionsLimit)
			{
				exceptionsList.Add(currentException);
				currentException = currentException.InnerException;
				currentIteration++;
			}

			return exceptionsList;
		}
	}
}
