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
		public Expression GenerateComparisonExpression(Expression baseExpression, string property, LinqExpressionComparisonOperator expressionComparisonOperator, object value, Type valueType)
		{
			Expression left = Expression.Property(baseExpression, property);
			Expression right = Expression.Constant(value, valueType);
			return GenerateComparisonExpression(left, expressionComparisonOperator, right);
		}
		
		/// <inheritdoc />
		public Expression GenerateComparisonExpression(Expression left, LinqExpressionComparisonOperator expressionComparisonOperator, Expression right)
		{
			switch (expressionComparisonOperator)
			{
				case LinqExpressionComparisonOperator.LessThan:
					return Expression.LessThan(left, right);
				case LinqExpressionComparisonOperator.LessThanOrEqual:
					return Expression.LessThanOrEqual(left, right);
				case LinqExpressionComparisonOperator.Equal:
					return Expression.Equal(left, right);
				case LinqExpressionComparisonOperator.GreaterThanOrEqual:
					return Expression.GreaterThanOrEqual(left, right);
				case LinqExpressionComparisonOperator.GreaterThan:
					return Expression.GreaterThan(left, right);
			}

			return null;
		}

		/// <inheritdoc />
		public Expression GenerateComparisonExpression(Expression left, LinqExpressionMethodOperator expressionMethodOperator, Expression right)
		{
			switch (expressionMethodOperator)
			{
				case LinqExpressionMethodOperator.StringContains:
					MethodInfo stringContainsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
					return Expression.Call(left, stringContainsMethod!, right);
				case LinqExpressionMethodOperator.IntContains:
					MethodInfo intContainsMethod = typeof(string).GetMethod("Contains", new[] { typeof(int) });
					return Expression.Call(left, intContainsMethod!, right);
            }
			
			return null;
		}

		/// <inheritdoc />
		public Expression GenerateComparisonExpression(ParameterExpression parameter, string property, LinqExpressionMethodOperator expressionMethodOperator, object value, Type valueType)
		{
			Expression propertyExpression = GenerateMemberExpression(parameter, property); // Property
			ConstantExpression valueExpression = Expression.Constant(value, valueType); // Value
			return GenerateComparisonExpression(propertyExpression, expressionMethodOperator, valueExpression);
		}

		/// <inheritdoc />
		public Expression GenerateComparisonExpression(string navigationPropertyName, LinqExpressionListOperator expressionListOperator, ParameterExpression parameter, LambdaExpression lambda)
		{
			// Parse property name
			Expression member = GenerateMemberExpression(parameter, navigationPropertyName);

			// Get a reference to Enumerable.Any() method
			MethodInfo anyMethodInfo = typeof(Enumerable).GetMethods().First(m => m.Name == "Any" && m.GetParameters().Length == 2);

			// Create new method and return a call to it
			Type type = parameter.Type.GetProperty(navigationPropertyName)?.PropertyType.GetGenericArguments()[0];
			anyMethodInfo = anyMethodInfo.MakeGenericMethod(type);

			return expressionListOperator switch
			{
				LinqExpressionListOperator.AnyLambda => Expression.Call(anyMethodInfo, member, lambda),
				_ => null
			};
		}

        /// <inheritdoc />
        public Expression GenerateComparisonExpression<T>(NewArrayExpression leftArrayExpression, LinqExpressionListOperator expressionListOperator, LambdaExpression lambda)
        {

            // Get a reference to Enumerable.Any() method
            MethodInfo anyMethodInfo = typeof(Enumerable).GetMethods().First(m => m.Name == "Any" && m.GetParameters().Length == 2);

            // Create new method and return a call to it
            anyMethodInfo = anyMethodInfo.MakeGenericMethod(typeof(T));

            return expressionListOperator switch
            {
                LinqExpressionListOperator.AnyLambda => Expression.Call(anyMethodInfo, leftArrayExpression, lambda),
                _ => null
            };
        }

		/// <inheritdoc />
		public Expression GenerateComparisonExpression(ParameterExpression parameter, string property, LinqExpressionComparisonOperator expressionComparisonOperator, object value, Type valueType)
		{
			// Create expression
			Expression propertyExpression = GenerateMemberExpression(parameter, property); // Property
			ConstantExpression valueExpression = Expression.Constant(value, valueType); // Value
			
			return GenerateComparisonExpression(propertyExpression, expressionComparisonOperator, valueExpression);
		}

		/// <inheritdoc />
		public List<ConstantExpression> GenerateConstantExpressionListFromArray<T>(object[] values) => values.Select(v => Expression.Constant((T)v)).ToList();
		
		/// <inheritdoc />
		public Expression GenerateFilterExpression(Expression left, LinqExpressionListOperator expressionListOperator, Type[] types = null, object[] values = null)
		{
			MethodInfo methodInfo;

			switch (expressionListOperator)
			{
				case LinqExpressionListOperator.Any:
					methodInfo = typeof(Enumerable)
						.GetMethods(BindingFlags.Public | BindingFlags.Static)
						.First(method => method.Name == "Any" && method.GetParameters().Length == 1);
					methodInfo = methodInfo.MakeGenericMethod(types![0]);
					return Expression.Call(methodInfo, left);
				case LinqExpressionListOperator.First:
					methodInfo = typeof(Enumerable)
						.GetMethods(BindingFlags.Public | BindingFlags.Static)
						.First(method => method.Name == "First" && method.GetParameters().Length == 1);
					methodInfo = methodInfo.MakeGenericMethod(types![0]);
					return Expression.Call(methodInfo, left);
				case LinqExpressionListOperator.Intersect:
					methodInfo = typeof(Enumerable)
						.GetMethods(BindingFlags.Public | BindingFlags.Static)
						.First(method => method.Name == "Intersect" && method.GetParameters().Length == 2);
					methodInfo = methodInfo.MakeGenericMethod(types![0]);
					NewArrayExpression items = Expression.NewArrayInit(typeof(int), GenerateConstantExpressionListFromArray<int>(values));
					return Expression.Call(methodInfo, left, items);
			}

			return null;
		}

		/// <inheritdoc />
		public Expression<Func<TModel, bool>> GenerateLambdaFromExpressions<TModel>(ParameterExpression parameter, IList<(LinqExpressionJoinCondition? joinCondition, Expression expression)> parts)
		{
			Expression resultExpression = null;
			LinqExpressionJoinCondition defaultJoinCondition = LinqExpressionJoinCondition.AndAlso;

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

		/// <inheritdoc />
		public Expression GenerateMemberExpression(Expression parameterExpression, string memberName)
		{
			// Return property if it is a simple name (no dots)
			if (!memberName.Contains("."))
			{
				return Expression.Property(parameterExpression, memberName);
			}

			// Process recursively to get a complex property expression: p.property1.property2 instead p.property1
			int index = memberName.IndexOf(".", StringComparison.Ordinal);
			MemberExpression subProperty = Expression.Property(parameterExpression, memberName.Substring(0, index));
			return GenerateMemberExpression(subProperty, memberName.Substring(index + 1));
		}

		/// <inheritdoc />
		public MemberExpression GenerateMemberExpression(ParameterExpression propertyParameter, Type basePropertyType, string memberName)
		{
			PropertyInfo property = basePropertyType.GetProperty(memberName);
			return Expression.MakeMemberAccess(propertyParameter, property!);
		}

		/// <inheritdoc />
		public Expression GenerateOrderingExpression<TSource, TKey>(LinqExpressionOrderingOperator expressionOrderingOperator, MemberExpression basePropertyAccess, LambdaExpression selectorLambdaExpression)
		{
			string methodName = expressionOrderingOperator is LinqExpressionOrderingOperator.Ascending ? "OrderBy": "OrderByDescending";

			// Create OrderBy() or OrderByDescending() methods
			MethodInfo orderByMethod = typeof(Enumerable)
				.GetMethods(BindingFlags.Public | BindingFlags.Static)
				.First(method => method.Name == methodName && method.GetParameters().Length == 2);
			orderByMethod = orderByMethod.MakeGenericMethod(typeof(TSource), typeof(TKey));

			return Expression.Call(orderByMethod, basePropertyAccess, selectorLambdaExpression);
		}

		/// <inheritdoc />
		public Expression GenerateSelectExpression<TSource, TResult>(MemberExpression basePropertyAccess, LambdaExpression projectionLambdaExpression)
		{
			// Create OrderBy() or OrderByDescending() methods
			MethodInfo selectMethod = typeof(Enumerable)
				.GetMethods(BindingFlags.Public | BindingFlags.Static)
				.First(method => method.Name == "Select" && method.GetParameters()[1].ToString().Contains("[TSource,TResult]"));
			selectMethod = selectMethod!.MakeGenericMethod(typeof(TSource), typeof(TResult));

			return Expression.Call(selectMethod, basePropertyAccess, projectionLambdaExpression);
		}

		private Expression JoinExpressions(Expression left, LinqExpressionJoinCondition joinCondition, Expression right)
		{
			switch (joinCondition)
			{
				case LinqExpressionJoinCondition.And:
					return Expression.And(left, right);
				case LinqExpressionJoinCondition.Or:
					return Expression.Or(left, right);
				case LinqExpressionJoinCondition.OrElse:
					return Expression.OrElse(left, right);
				default:
					return Expression.AndAlso(left, right);
			}
		}
	}
}
