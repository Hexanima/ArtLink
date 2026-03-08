namespace Domain.Types;

public enum FilterOperator
{
    Eq,
    Neq,
    Gt,
    Gte,
    Lt,
    Lte,
    Contains,
    In
}

public interface IFilter { }

public class BaseFilter : IFilter
{
    public string Field { get; set; } = default!;
    public FilterOperator Operator { get; set; }
    public object? Value { get; set; }
}

public class AndFilter : IFilter
{
    public List<IFilter> Value { get; set; } = new();
}

public class OrFilter : IFilter
{
    public List<IFilter> Value { get; set; } = new();
}

public class Query
{
    public IFilter? Filters { get; set; }
    public bool IncludeDeleted { get; set; } = false;
    public int? Limit { get; set; }
    public int? Offset { get; set; }
}
