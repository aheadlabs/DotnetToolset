using DotnetToolset.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DotnetToolset.Services
{
	public interface ILinqService
	{
		/// <summary>
		/// Generates a Lambda comparison expression
		/// </summary>
		/// <param name="parameter">Lambda parameter for the main entity</param>
		/// <param name="property">Object property used in the comparison</param>
		/// <param name="expressionComparisonOperator">Operator used in the comparison</param>
		/// <param name="value">Value to compare against</param>
		/// <param name="valueType">Type of the value to compare against</param>
		/// <param name="isListExpression">If true, the expression will processed as a list expression</param>
		/// <param name="listParameter">Lambda parameter for the related entity</param>
		/// <returns>Lambda comparison expression</returns>
		Expression GenerateLambdaComparisonExpression(ParameterExpression parameter, string property, LinqExpressionComparisonOperator expressionComparisonOperator, object value, Type valueType, 
			bool isListExpression = false, ParameterExpression listParameter = null);

		/// <summary>
		/// Generates a Lambda expression joining several existing expressions
		/// </summary>
		/// <typeparam name="TModel">Main type used in the lambda expressions</typeparam>
		/// <param name="parameter">Main parameter for the lambda expression</param>
		/// <param name="parts">List of tuples containing "join condition"-"expression" pairs</param>
		/// <returns>Lambda expression that contains all expressions joined</returns>
		Expression<Func<TModel, bool>> GenerateLambdaFromExpressions<TModel>(ParameterExpression parameter, IList<(LinqExpressionJoinCondition? joinCondition, Expression expression)> parts);

		/// <summary>
		/// Processes the expression as a list expression (additional coding is needed)
		/// </summary>
		/// <param name="basePropertyName">Property name of the base entity that contains the navigation property</param>
		/// <param name="parameter">Parameter for the lambda expression</param>
		/// <param name="lambda">Lambda expression for the linked navigation property</param>
		/// <returns></returns>
		Expression ProcessListExpression(string basePropertyName, ParameterExpression parameter, LambdaExpression lambda);
	}
}
