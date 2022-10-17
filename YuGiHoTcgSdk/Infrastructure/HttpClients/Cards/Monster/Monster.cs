namespace YuGiHoTcgSdk.Infrastructure.HttpClients.Cards.Monster;

using System.Collections.Generic;
using Model;
using Newtonsoft.Json;
using Set;

public class Monster : ApiResource
{
    [JsonProperty("id")]
    public override string Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("desc")]
    public string Desc { get; set; }

    [JsonProperty("atk")]
    public long Atk { get; set; }

    [JsonProperty("race")]
    public string Race { get; set; }

    [JsonProperty("attribute")]
    public string Attribute { get; set; }

    [JsonProperty("archetype", NullValueHandling = NullValueHandling.Ignore)]
    public string Archetype { get; set; }

    [JsonProperty("linkval")]
    public int Linkval { get; set; }

    [JsonProperty("linkmarkers")]
    public List<string> Linkmarkers { get; set; }

    [JsonProperty("card_sets", NullValueHandling = NullValueHandling.Ignore)]
    public List<SetInfo> CardSets { get; set; }

    [JsonProperty("card_images")]
    public List<CardImage> CardImages { get; set; }

    [JsonProperty("card_prices")]
    public List<CardPrice> CardPrices { get; set; }

    [JsonProperty("banlist_info", NullValueHandling = NullValueHandling.Ignore)]
    public Banlist BanlistInfo { get; set; }
}