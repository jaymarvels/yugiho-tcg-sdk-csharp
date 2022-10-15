namespace YuGiHoTcgSdk.Infrastructure.HttpClients.Set
{
    using Common;
    using Newtonsoft.Json;

    public class SetInfo : ResourceBase
    {
        public override string Id { get; set; }

        internal new static string ApiEndpoint { get; } = Global.SetInfoApiBase;

        [JsonProperty("set_name")]
        public string SetName { get; set; }

        [JsonProperty("set_code")]
        public string SetCode { get; set; }

        [JsonProperty("set_rarity")]
        public string SetRarity { get; set; }

        [JsonProperty("set_rarity_code")]
        public string SetRarityCode { get; set; }

        [JsonProperty("set_price")]
        public string SetPrice { get; set; }
    }
}