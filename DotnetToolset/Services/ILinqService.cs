using DotnetToolset.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DotnetToolset.Services
{
	public interface ILinqService
	{
		/// <summary>
		/// Generates a comparison expression based on an existing expression. ie: user.Assets.Name == 'Jacket'
		/// </summary>
		/// <param name="baseExpression">Expression that acts as the base of the comparison, like user.Assets where Assets is a collection</param>
		/// <param name="property">Property that exists in the base expression, like Name in user.Assets.Name</param>
		/// <param name="expressionComparisonOperator">Any of the operators available for creating comparisons</param>
		/// <param name="value">Value of the comparison, like 'Jacket'</param>
		/// <param name="valueType">Type of the value, like typeof(string)</param>
		/// <returns></returns>
		Expression GenerateComparisonExpression(Expression baseExpression, string property, LinqExpressionComparisonOperator expressionComparisonOperator, object value, Type valueType);

		/// <summary>
		/// Generates a comparison expression based on two existing expressions
		/// </summary>
		/// <param name="left">First expression</param>
		/// <param name="expressionComparisonOperator">Comparison operator</param>
		/// <param name="right">Second expression</param>
		/// <returns></returns>
		Expression GenerateComparisonExpression(Expression left, LinqExpressionComparisonOperator expressionComparisonOperator, Expression right);

		/// <summary>
		/// Generates a comparison expression based on two existing expressions and a method that connects them, like user.Name.Contains('John')
		/// </summary>
		/// <param name="left">Property expression, like user.Name</param>
		/// <param name="expressionMethodOperator">Method that links both expressions</param>
		/// <param name="right">Constant expression, like 'John'</param>
		/// <returns></returns>
		Expression GenerateComparisonExpression(Expression left, LinqExpressionMethodOperator expressionMethodOperator, Expression right);

		/// <summary>
		/// Generates a comparison expression based on two existing expressions and a method that connects them, like user.Name.Contains('John')
		/// </summary>
		/// <param name="parameter">Parameter for the property expression</param>
		/// <param name="property">Name of the property expression</param>
		/// <param name="expressionMethodOperator">Method that links both expressions</param>
		/// <param name="value">Value for the constant expression</param>
		/// <param name="valueType">Value type for the constant expression</param>
		/// <returns></returns>
		Expression GenerateComparisonExpression(ParameterExpression parameter, string property, LinqExpressionMethodOperator expressionMethodOperator, object value, Type valueType);

		/// <summary>
		/// Generates a comparison expression between a IEnumerable and an Any(lambda-expression) method that contains a lambda expression, like users.Any(u => u.Name = 'John').
		/// </summary>
		/// <param name="navigationPropertyName">Property name of the base entity that contains the navigation property</param>
		/// <param name="expressListOperator"></param>
		/// <param name="parameter">Parameter for the lambda expression</param>
		/// <param name="lambda">Lambda expression for the linked navigation property</param>
		/// <returns></returns>
		Expression GenerateComparisonExpression(string navigationPropertyName, LinqExpressListOperator expressListOperator, ParameterExpression parameter, LambdaExpression lambda);

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
		Expression GenerateComparisonExpression(ParameterExpression parameter, string property, LinqExpressionComparisonOperator expressionComparisonOperator, object value, Type valueType);

		/// <summary>
		/// Generates a Lambda expression joining several existing expressions
		/// </summary>
		/// <typeparam name="TModel">Main type used in the lambda expressions</typeparam>
		/// <param name="parameter">Main parameter for the lambda expression</param>
		/// <param name="parts">List of tuples containing "join condition"-"expression" pairs</param>
		/// <returns>Lambda expression that contains all expressions joined</returns>
		Expression<Func<TModel, bool>> GenerateLambdaFromExpressions<TModel>(ParameterExpression parameter, IList<(LinqExpressionJoinCondition? joinCondition, Expression expression)> parts);

		/// <summary>
		/// Gets the property expression
		/// </summary>
		/// <param name="parameterExpression"></param>
		/// <param name="propertyName"></param>
		/// <returns></returns>
		Expression GetMemberExpression(Expression parameterExpression, string propertyName);

		/// <summary>
		/// Processes the expression as a list expression (additional coding is needed)
		/// </summary>
		/// <param name="basePropertyName">Property name of the base entity that contains the navigation property</param>
		/// <param name="parameter">Parameter for the lambda expression</param>
		/// <param name="lambda">Lambda expression for the linked navigation property</param>
		/// <returns></returns>
		//Expression ProcessListExpression(string basePropertyName, ParameterExpression parameter, LambdaExpression lambda);
	}
}
