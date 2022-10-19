namespace YuGiHoTcgSdk.Infrastructure.HttpClients.Cards.Skill;

using System.Collections.Generic;
using Model;
using Newtonsoft.Json;
using Set;
using Common;

public class SkillCard :ApiResource
{
    internal new static string ApiEndpoint { get; } = $"{Global.CardApiBase}?type=skill card";

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

    [JsonProperty("card_images")]
    public List<CardImage> CardImages { get; set; }

    [JsonProperty("card_prices")]
    public List<CardPrice> CardPrices { get; set; }

    [JsonProperty("card_sets", NullValueHandling = NullValueHandling.Ignore)]
    public List<SetInfo> CardSets { get; set; }

    [JsonProperty("archetype", NullValueHandling = NullValueHandling.Ignore)]
    public string Archetype { get; set; }
}