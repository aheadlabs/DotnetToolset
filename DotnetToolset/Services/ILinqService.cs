using DotnetToolset.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;

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
		/// Generates a comparison expression between a IEnumerable and an [Method](lambda-expression) method that contains a lambda expression, like users.Any(u => u.Name = 'John').
		/// </summary>
		/// <param name="navigationPropertyName">Property name of the base entity that contains the navigation property</param>
		/// <param name="expressionListOperator"></param>
		/// <param name="parameter">Parameter for the lambda expression</param>
		/// <param name="lambda">Lambda expression for the linked navigation property</param>
		/// <returns></returns>
		Expression GenerateComparisonExpression(string navigationPropertyName, LinqExpressionListOperator expressionListOperator, ParameterExpression parameter, LambdaExpression lambda);

		/// <summary>
		/// Generates a Lambda comparison expression
		/// </summary>
		/// <param name="parameter">Lambda parameter for the main entity</param>
		/// <param name="property">Object property used in the comparison</param>
		/// <param name="expressionComparisonOperator">Operator used in the comparison</param>
		/// <param name="value">Value to compare against</param>
		/// <param name="valueType">Type of the value to compare against</param>
		/// <returns>Lambda comparison expression</returns>
		Expression GenerateComparisonExpression(ParameterExpression parameter, string property, LinqExpressionComparisonOperator expressionComparisonOperator, object value, Type valueType);

        /// <summary>
        /// Generates a Lambda comparison expression from an array and any lambda
        /// </summary>
        /// <param name="leftArrayExpression">Expression array from which we want to do the any operation</param>
        /// <param name="expressionListOperator">List operator used in the comparison</param>
        /// <param name="lambda">LAmbda to embed into the list operator</param>
        /// <returns>Lambda comparison expression</returns>
        Expression GenerateComparisonExpression<T>(NewArrayExpression leftArrayExpression,
            LinqExpressionListOperator expressionListOperator, LambdaExpression lambda);

		/// <summary>
		/// Creates an array expression from an array of values
		/// </summary>
		/// <typeparam name="T">Destination type for the array elements when the cast takes place</typeparam>
		/// <param name="values">Array of values to create the expression from</param>
		/// <returns></returns>
		List<ConstantExpression> GenerateConstantExpressionListFromArray<T>(object[] values);

		/// <summary>
		/// Generates a filter expression that affects an IEnumerable, like users.First()
		/// </summary>
		/// <param name="left">IEnumerable expression, like users</param>
		/// <param name="expressionListOperator">Filter method operator, like .First()</param>
		/// <param name="types">Array of types to be substituted for the type parameters of the current generic method definition</param>
		/// <param name="values">Array of values to be substituted for the value parameters of the current generic method definition</param>
		/// <returns></returns>
		Expression GenerateFilterExpression(Expression left, LinqExpressionListOperator expressionListOperator, Type[] types = null, object[] values = null);

		/// <summary>
		/// Generates a Lambda expression joining several existing expressions
		/// </summary>
		/// <typeparam name="TModel">Main type used in the lambda expressions</typeparam>
		/// <param name="parameter">Main parameter for the lambda expression</param>
		/// <param name="parts">List of tuples containing "join condition"-"expression" pairs</param>
		/// <returns>Lambda expression that contains all expressions joined</returns>
		Expression<Func<TModel, bool>> GenerateLambdaFromExpressions<TModel>(ParameterExpression parameter, IList<(LinqExpressionJoinCondition? joinCondition, Expression expression)> parts);

		/// <summary>
		/// Generates a property member expression
		/// </summary>
		/// <param name="parameterExpression">Base property to access the member from</param>
		/// <param name="memberName">Member name</param>
		/// <returns></returns>
		Expression GenerateMemberExpression(Expression parameterExpression, string memberName);

		/// <summary>
		/// Generates a property member access expression, like property.member
		/// </summary>
		/// <param name="propertyParameter">Parameter for the base property</param>
		/// <param name="basePropertyType">Type for the base property to access the member from</param>
		/// <param name="memberName">Member name</param>
		/// <returns></returns>
		MemberExpression GenerateMemberExpression(ParameterExpression propertyParameter, Type basePropertyType, string memberName);

		/// <summary>
		/// Generates an ordering expression, like users.OrderByDescending(u => u.LastLoginDate) where users is IEnumerable of T
		/// </summary>
		/// <param name="expressionOrderingOperator">Ascending (OrderBy) or descending (OrderByDescending)</param>
		/// <typeparam name="TSource">IEnumerable generic type, like T in IEnumerable of T (users)</typeparam>
		/// <typeparam name="TKey">Type for the selector expression, like DateTime in u.LastLoginDate</typeparam>
		/// <param name="basePropertyAccess">Selector property expression, like users</param>
		/// <param name="selectorLambdaExpression">Lambda expression for the ordering selector, like u => u.LastLoginDate</param>
		/// <returns></returns>
		Expression GenerateOrderingExpression<TSource, TKey>(LinqExpressionOrderingOperator expressionOrderingOperator, MemberExpression basePropertyAccess, LambdaExpression selectorLambdaExpression);

		/// <summary>
		/// Generates a Select expression, like users.Select(u => u.Id) where users is IEnumerable of T
		/// </summary>
		/// <typeparam name="TSource">IEnumerable generic type, like T in IEnumerable of T (users)</typeparam>
		/// <typeparam name="TResult">Type for the selector expression, like DateTime in u.LastLoginDate</typeparam>
		/// <param name="basePropertyAccess">Selector property expression, like users</param>
		/// <param name="projectionLambdaExpression">Lambda expression for the ordering selector, like u => u.LastLoginDate</param>
		/// <returns></returns>
		Expression GenerateSelectExpression<TSource, TResult>(MemberExpression basePropertyAccess, LambdaExpression projectionLambdaExpression);
	}
}
