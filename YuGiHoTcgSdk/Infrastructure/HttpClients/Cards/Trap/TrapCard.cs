namespace YuGiHoTcgSdk.Infrastructure.HttpClients.Cards.Trap;

using System.Collections.Generic;
using Model;
using Newtonsoft.Json;
using Set;
using YuGiHoTcgSdk.Infrastructure.HttpClients.Common;

public class TrapCard : ApiResource
{
    internal new static string ApiEndpoint { get; } = $"{Global.CardApiBase}?type=trap card";

    [JsonProperty("id")]
    public override string Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("desc")]
    public string Desc { get; set; }

    [JsonProperty("race")]
    public string Race { get; set; }

    [JsonProperty("card_sets", NullValueHandling = NullValueHandling.Ignore)]
    public List<SetInfo> CardSets { get; set; }

    [JsonProperty("card_images")]
    public List<CardImage> CardImages { get; set; }

    [JsonProperty("card_prices")]
    public List<CardPrice> CardPrices { get; set; }

    [JsonProperty("archetype", NullValueHandling = NullValueHandling.Ignore)]
    public string Archetype { get; set; }

    [JsonProperty("banlist_info", NullValueHandling = NullValueHandling.Ignore)]
    public Banlist BanlistInfo { get; set; }
}