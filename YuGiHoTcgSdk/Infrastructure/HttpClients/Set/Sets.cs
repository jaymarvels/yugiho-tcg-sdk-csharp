namespace YuGiHoTcgSdk.Infrastructure.HttpClients.Set;

using System;
using Common;
using Newtonsoft.Json;

public class Sets : ResourceBase
{
    public override string Id { get; set; }

    internal new static string ApiEndpoint { get; } = Global.SetsApiBase;

    [JsonProperty("set_name")]
    public string SetName { get; set; }

    [JsonProperty("set_code")]
    public string SetCode { get; set; }

    [JsonProperty("num_of_cards")]
    public long NumOfCards { get; set; }

    [JsonProperty("tcg_date", NullValueHandling = NullValueHandling.Ignore)]
    public DateTime? TcgDate { get; set; }
}