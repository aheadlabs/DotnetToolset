namespace DotnetToolset.Enums
{
	/// <summary>
	/// Comparison operators used in Lambda expressions
	/// </summary>
	public enum LinqExpressionComparisonOperator
	{
		Contains,
		Equal,
		GreaterThan,
		GreaterThanOrEqual,
		LessThan,
		LessThanOrEqual
	}

	/// <summary>
	/// Join conditions for linking lambda expressions together
	/// </summary>
	public enum LinqExpressionJoinCondition
	{
		/// <summary>
		/// Equals to LINQ Expression.AndAlso
		/// </summary>
		And,

		/// <summary>
		/// Equals to LINQ Expression.OrElse
		/// </summary>
		Or
	}
}
