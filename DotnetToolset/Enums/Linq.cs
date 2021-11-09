namespace DotnetToolset.Enums
{
	/// <summary>
	/// Comparison operators used in Lambda expressions
	/// </summary>
	public enum LinqExpressionComparisonOperator
	{
		Equal,
		GreaterThan,
		GreaterThanOrEqual,
		LessThan,
		LessThanOrEqual
	}

	public enum LinqExpressionMethodOperator
	{
		/// <summary>
		/// int.Contains()
		/// </summary>
		IntContains,

		/// <summary>
		/// string.Contains()
		/// </summary>
		StringContains
	}

	public enum LinqExpressListOperator
	{
		/// <summary>
		/// IEnumerable.Any(lambda-expression)
		/// </summary>
		AnyLambda
	}

	/// <summary>
	/// Join conditions for linking lambda expressions together
	/// </summary>
	public enum LinqExpressionJoinCondition
	{
		/// <summary>
		/// Equals to LINQ expression And
		/// </summary>
		And,

		/// <summary>
		/// Equals to LINQ Expression.AndAlso (short-circuits)
		/// </summary>
		AndAlso,

		/// <summary>
		/// Equals to LINQ Expression.Or
		/// </summary>
		Or,

		/// <summary>
		/// Equals to LINQ Expression.OrElse (short-circuits)
		/// </summary>
		OrElse
	}


}
