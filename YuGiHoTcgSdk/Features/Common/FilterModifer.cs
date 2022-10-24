namespace YuGiHoTcgSdk.Features.Common;

public class FilterModifer
{
    private FilterModifer(string value) { Value = value; }

    public string Value { get; private set; }

    public static FilterModifer LessThan => new("lt");

    public static FilterModifer LessThanEqualTo => new("lte");

    public static FilterModifer GreaterThan => new("gt");

    public static FilterModifer GreaterThanEqualTo => new("gte");
}