using DotnetToolset.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Res = DotnetToolset.Resources.Literals;

namespace DotnetToolset.Services
{
	public class LinqService : ILinqService
	{
		private readonly ILogger<LinqService> _logger;

		public LinqService(ILogger<LinqService> logger)
		{
			_logger = logger;
		}

		/// <inheritdoc />
		public Expression GenerateLambdaComparisonExpression(ParameterExpression parameter, string property,
			LinqExpressionComparisonOperator expressionComparisonOperator, object value, Type valueType, 
			bool isListExpression = false, ParameterExpression listParameter = null)
		{
			// Check if listParameter is passed when it is a list expression
			if (isListExpression && listParameter is null)
			{
				throw new ArgumentException(Res.b_ListParameterMustNotBeNull, nameof(listParameter));
			}

			// Find out if property name is simple or complex (using dot notation)
			string[] propertyParts = property.Split(".", 2, StringSplitOptions.RemoveEmptyEntries);
			string propertyName = propertyParts.Length == 1 ? propertyParts[0] : propertyParts[1];

			// Create expression
			MemberExpression propertyExpression = Expression.Property(isListExpression ? listParameter : parameter, propertyName); // Property
			ConstantExpression valueExpression = Expression.Constant(value, valueType); // Value
			Expression resultExpression = null;

			// Create comparison expression
			switch (expressionComparisonOperator)
			{
				case LinqExpressionComparisonOperator.LessThan:
					resultExpression = Expression.LessThan(propertyExpression, valueExpression);
					break;
				case LinqExpressionComparisonOperator.LessThanOrEqual:
					resultExpression = Expression.LessThanOrEqual(propertyExpression, valueExpression);
					break;
				case LinqExpressionComparisonOperator.Equal:
					resultExpression = Expression.Equal(propertyExpression, valueExpression);
					break;
				case LinqExpressionComparisonOperator.GreaterThanOrEqual:
					resultExpression = Expression.GreaterThanOrEqual(propertyExpression, valueExpression);
					break;
				case LinqExpressionComparisonOperator.GreaterThan:
					resultExpression = Expression.GreaterThan(propertyExpression, valueExpression);
					break;
			}

			// Process expression as a list statement if needed and return
			return isListExpression && resultExpression != null
				? ProcessListExpression(propertyParts[0], parameter, Expression.Lambda(resultExpression, listParameter))
				: resultExpression;
		}

		/// <inheritdoc />
		public Expression<Func<TModel, bool>> GenerateLambdaFromExpressions<TModel>(ParameterExpression parameter, IList<(LinqExpressionJoinCondition? joinCondition, Expression expression)> parts)
		{
			Expression resultExpression = null;
			LinqExpressionJoinCondition defaultJoinCondition = LinqExpressionJoinCondition.And;

			// Remove all parts that are completely null
			parts = parts.Where(part => part != (null, null)).ToList();

			if (!parts.Any())
			{
				_logger.LogWarning(Res.b_NoExpressionsProvidedToBeJoined);
				return null;
			}

			if (parts.Count == 1)
			{
				resultExpression = parts.First().expression;
			}

			// Do while we have at least two expressions to join
			while (parts.Count >= 2)
			{
				// Join the first two conditions
				Expression left = parts.First().expression;
				parts.Remove(parts.First());
				LinqExpressionJoinCondition joinCondition = parts.First().joinCondition ?? defaultJoinCondition;
				Expression right = parts.First().expression;
				
				resultExpression = JoinExpressions(left, joinCondition, right);

				// Replace first original condition by the joined one, in order to continue joining the rest
				var part = parts.First(); // Used var for readability
				parts.Remove(part);
				parts = parts.Prepend((part.joinCondition ?? defaultJoinCondition, resultExpression)).ToList();
			}

			// Return all joined expressions together
			return resultExpression != null
				? Expression.Lambda<Func<TModel, bool>>(resultExpression, parameter)
				: null;
		}

		private Expression GetMemberExpression(Expression param, string propertyName)
		{
			if (!propertyName.Contains("."))
			{
				return Expression.Property(param, propertyName);
			}

			int index = propertyName.IndexOf(".", StringComparison.Ordinal);
			MemberExpression subParam = Expression.Property(param, propertyName.Substring(0, index));
			return GetMemberExpression(subParam, propertyName.Substring(index + 1));
		}

		private Expression JoinExpressions(Expression left, LinqExpressionJoinCondition joinCondition, Expression right)
		{
			switch (joinCondition)
			{
				case LinqExpressionJoinCondition.Or:
					return Expression.OrElse(left, right);
				default:
					return Expression.AndAlso(left, right);
			}
		}

		/// <inheritdoc />
		public Expression ProcessListExpression(string basePropertyName, ParameterExpression parameter, LambdaExpression lambda)
		{
			// Parse property name
			Expression member = GetMemberExpression(parameter, basePropertyName);

			// Get a reference to Enumerable.Any() method
			MethodInfo anyMethodInfo = typeof(Enumerable).GetMethods().First(m => m.Name == "Any" && m.GetParameters().Length == 2);

			// Create new method and return a call to it
			Type type = parameter.Type.GetProperty(basePropertyName)?.PropertyType.GetGenericArguments()[0];
			anyMethodInfo = anyMethodInfo.MakeGenericMethod(type);
			return Expression.Call(anyMethodInfo, member, lambda);
		}
	}
}
