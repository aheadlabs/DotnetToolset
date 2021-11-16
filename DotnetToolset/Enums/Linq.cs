namespace DotnetToolset.Enums
{
	/// <summary>
	/// Comparison operators used in lambda expressions
	/// </summary>
	public enum LinqExpressionComparisonOperator
	{
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

	/// <summary>
	/// List operators used in lambda expressions
	/// </summary>
	public enum LinqExpressionListOperator
	{
		/// <summary>
		/// IEnumerable.Any()
		/// </summary>
		Any,

		/// <summary>
		/// IEnumerable.Any(lambda-expression)
		/// </summary>
		AnyLambda,

		/// <summary>
		/// IEnumerable.First()
		/// </summary>
		First,

		/// <summary>
		/// IEnumerable.Intersect(IEnumerable, IEnumerable) => no comparer used
		/// </summary>
		Intersect
	}

	/// <summary>
	/// Method operators used in lambda expressions
	/// </summary>
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

	/// <summary>
	/// Ordering operators used in lambda expressions
	/// </summary>
	public enum LinqExpressionOrderingOperator
	{
		/// <summary>
		/// IEnumerable.OrderBy()
		/// </summary>
		Ascending,

		/// <summary>
		/// IEnumerable.OrderByDescending()
		/// </summary>
		Descending
	}

	/// <summary>
	/// Select operators used in lambda expressions
	/// </summary>
	public enum LinqExpressionSelectOperator
	{
		/// <summary>
		/// IEnumerable.Select()
		/// </summary>
		IntSelect
	}
}
