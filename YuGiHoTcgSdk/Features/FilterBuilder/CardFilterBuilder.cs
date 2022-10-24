namespace YuGiHoTcgSdk.Features.FilterBuilder;

public class CardFilterBuilder
{
    public static CardFilterCollection<string, string> CreateCardFilter()
    {
        return new CardFilterCollection<string, string>();
    }
}