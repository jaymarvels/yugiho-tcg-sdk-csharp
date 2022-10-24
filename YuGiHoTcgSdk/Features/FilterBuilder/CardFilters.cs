namespace YuGiHoTcgSdk.Features.FilterBuilder;

using System.Linq;
using Common;
using Infrastructure.HttpClients.Cards.Trap;

public static class CardFilters
{
    /// <summary>
    /// Extension method. Will add new language filter. If language filter exists it will overwrite.
    /// Options are fr,de,it,pt,ko,ja. No language filter = english
    /// </summary>
    /// <param name="dictionary"></param>
    /// <param name="value">The name value to add</param>
    public static CardFilterCollection<string, string> AddLanguage(this CardFilterCollection<string, string> dictionary, string value)
    {
        var values = new[] { "fr", "de", "it", "pt", "ko", "ja" };
        if (values.Any(value.Contains))
        {
            return AddOrOverwrite(dictionary, "language", value);
        }

        return AddOrOverwrite(dictionary, "language", "en");
    }

    /// <summary>
    /// Extension method. Will add new id filter. If id filter exists
    /// will concat and create an OR filter. e.g "dp4-3" or "dp4-4"
    /// You cannot pass this alongside name. You can add multiple ID filters.
    /// </summary>
    /// <param name="dictionary"></param>
    /// <param name="value">The name value to add</param>
    public static CardFilterCollection<string, string> AddId(this CardFilterCollection<string, string> dictionary, string value)
    {
        return AddOrUpdate(dictionary, nameof(TrapCard.Id).ToLower(), value);
    }

    /// <summary>
    /// Extension method. Will add new card set filter. If card set filter exists
    /// will concat and create an OR filter. e.g "dp4-3" or "dp4-4"
    /// </summary>
    /// <param name="dictionary"></param>
    /// <param name="value">The name value to add</param>
    public static CardFilterCollection<string, string> AddCardSet(this CardFilterCollection<string, string> dictionary, string value)
    {
        return AddOrUpdate(dictionary, "cardset", value);
    }

    /// <summary>
    /// Extension method. Will add new banlist filter. If card set filter exists
    /// will concat and create an OR filter. e.g "dp4-3" or "dp4-4".
    /// Will return cards within the selected banlist e.g tcg, ocg, goat
    /// </summary>
    /// <param name="dictionary"></param>
    /// <param name="value">The name value to add</param>
    public static CardFilterCollection<string, string> AddBanList(this CardFilterCollection<string, string> dictionary, string value)
    {
        return AddOrUpdate(dictionary, "banlist", value);
    }

    /// <summary>
    /// Extension method. Will add new link marker filter. If link marker filter exists
    /// will concat and create an OR filter. e.g "dp4-3" or "dp4-4".
    /// Will return cards within the selected link marker e.g Top, Bottom, Left, Right
    /// </summary>
    /// <param name="dictionary"></param>
    /// <param name="value">The name value to add</param>
    public static CardFilterCollection<string, string> AddLinkMarker(this CardFilterCollection<string, string> dictionary, string value)
    {
        return AddOrUpdate(dictionary, "linkmarker", value);
    }

    /// <summary>
    /// Extension method. Will add new link filter.
    /// Filter the cards by Link value (0-6). If value is greater than 6 will default to 6.
    /// </summary>
    /// <param name="dictionary"></param>
    /// <param name="value">The name value to add</param>
    public static CardFilterCollection<string, string> AddLinkValue(this CardFilterCollection<string, string> dictionary, int value)
    {
        if (value > 6)
        {
            value = 6;
        }

        return AddOrOverwrite(dictionary, "link", value.ToString());
    }

    /// <summary>
    /// Extension method. Will add new scale filter.
    /// Filter the cards by Link value (0-13). If value is greater than 13 will default to 13.
    /// </summary>
    /// <param name="dictionary"></param>
    /// <param name="value">The name value to add</param>
    public static CardFilterCollection<string, string> AddScaleValue(this CardFilterCollection<string, string> dictionary, int value)
    {
        if (value > 13)
        {
            value = 13;
        }

        return AddOrOverwrite(dictionary, "link", value.ToString());
    }

    /// <summary>
    /// Extension method. Will add new attack filter. If a previous atk filter exists it will be overwritten
    /// Filter the cards by attack value.
    /// </summary>
    /// <param name="dictionary"></param>
    /// <param name="value">The name value to add</param>
    /// <param name="modifer">Optional modify to enable less than/greater than etc</param>
    public static CardFilterCollection<string, string> AddAttack(this CardFilterCollection<string, string> dictionary, int value, FilterModifer modifer = null)
    {
        return AddOrOverwrite(dictionary, "atk", modifer != null ? $"{modifer.Value}{value}" : value.ToString());
    }

    /// <summary>
    /// Extension method. Will add new defence filter. If a previous def filter exists it will be overwritten
    /// Filter the cards by defence value.
    /// </summary>
    /// <param name="dictionary"></param>
    /// <param name="value">The name value to add</param>
    /// <param name="modifer">Optional modify to enable less than/greater than etc</param>
    public static CardFilterCollection<string, string> AddDefence(this CardFilterCollection<string, string> dictionary, int value, FilterModifer modifer = null)
    {
        return AddOrOverwrite(dictionary, "def", modifer != null ? $"{modifer.Value}{value}" : value.ToString());
    }

    /// <summary>
    /// Extension method. Will add new level filter. If a previous level filter exists it will be overwritten
    /// Filter the cards by level value.
    /// </summary>
    /// <param name="dictionary"></param>
    /// <param name="value">The name value to add</param>
    /// <param name="modifer">Optional modify to enable less than/greater than etc</param>
    public static CardFilterCollection<string, string> AddLevel(this CardFilterCollection<string, string> dictionary, int value, FilterModifer modifer = null)
    {
        return AddOrOverwrite(dictionary, "level", modifer != null ? $"{modifer.Value}{value}" : value.ToString());
    }

    /// <summary>
    /// Extension method. Will add new filter based on the model property nameof. If filter exists
    /// will try and concat and create an OR filter. e.g "dp4-3" or "dp4-4"
    /// </summary>
    /// <param name="dictionary"></param>
    /// <param name="value">The name value to add</param>
    /// <param name="paramName">The model property to filter against i.e nameof(TrapCard.Race)</param>
    public static CardFilterCollection<string, string> AddFilter(this CardFilterCollection<string, string> dictionary, string value, string paramName)
    {
        if (paramName.ToLower() == "name")
        {
            return AddOrUpdate(dictionary, paramName.ToLower(), value, "|");
        }

        return AddOrUpdate(dictionary, paramName.ToLower(), value);
    }

    /// <summary>
    /// Extension method. Will add new filter based on the model property nameof. If filter exists
    /// will try and concat and create an OR filter. e.g "dp4-3" or "dp4-4"
    /// </summary>
    /// <param name="dictionary"></param>
    /// <param name="value">The name value to add</param>
    /// <param name="paramName">The model property to filter against i.e nameof(TrapCard.Race)</param>
    public static CardFilterCollection<string, string> AddName(this CardFilterCollection<string, string> dictionary, string value)
    {
        return AddOrUpdate(dictionary, "name", value, "|");
    }

    private static CardFilterCollection<string, string> AddOrUpdate(CardFilterCollection<string, string> dictionary, string key, string value, string delim = ",")
    {
        if (dictionary.TryGetValue(key, out var oldValue))
        {
            oldValue = $"{oldValue}{delim}{value}";
            dictionary[key] = oldValue;
            return dictionary;
        }

        dictionary.Add(key, value);
        return dictionary;
    }
    
    private static CardFilterCollection<string, string> AddOrOverwrite(CardFilterCollection<string, string> dictionary, string key, string value)
    {
        if (dictionary.TryGetValue(key, out var oldValue))
        {
            dictionary[key] = value;
            return dictionary;
        }

        dictionary.Add(key, value);
        return dictionary;
    }
}