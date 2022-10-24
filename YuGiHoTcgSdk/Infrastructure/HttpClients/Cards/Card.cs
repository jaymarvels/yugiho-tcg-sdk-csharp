namespace YuGiHoTcgSdk.Infrastructure.HttpClients.Cards;

using System.Collections.Generic;
using Common;
using Model;
using Newtonsoft.Json;
using Set;

public class Card : ApiResource
{
    public override string Id { get; set; }

    internal new static string ApiEndpoint { get; } = Global.CardApiBase;

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("desc")]
    public string Desc { get; set; }

    [JsonProperty("race")]
    public string Race { get; set; }

    [JsonProperty("archetype", NullValueHandling = NullValueHandling.Ignore)]
    public string Archetype { get; set; }

    [JsonProperty("card_sets", NullValueHandling = NullValueHandling.Ignore)]
    public List<SetInfo> CardSets { get; set; }

    [JsonProperty("card_images")]
    public List<CardImage> CardImages { get; set; }

    [JsonProperty("card_prices")]
    public List<CardPrice> CardPrices { get; set; }

    [JsonProperty("atk")]
    public int? Atk { get; set; }

    [JsonProperty("def", NullValueHandling = NullValueHandling.Ignore)]
    public int? Def { get; set; }

    [JsonProperty("level", NullValueHandling = NullValueHandling.Ignore)]
    public int? Level { get; set; }

    [JsonProperty("attribute", NullValueHandling = NullValueHandling.Ignore)]
    public string Attribute { get; set; }

    [JsonProperty("banlist_info", NullValueHandling = NullValueHandling.Ignore)]
    public Banlist BanlistInfo { get; set; }

    [JsonProperty("scale", NullValueHandling = NullValueHandling.Ignore)]
    public int? Scale { get; set; }

    [JsonProperty("linkval", NullValueHandling = NullValueHandling.Ignore)]
    public int? Linkval { get; set; }

    [JsonProperty("linkmarkers", NullValueHandling = NullValueHandling.Ignore)]
    public List<string> Linkmarkers { get; set; }
}