using Newtonsoft.Json;

namespace YuGiHoTcgSdk.Infrastructure.HttpClients.Cards.Model
{
    public class Banlist
    {
        [JsonProperty("ban_ocg", NullValueHandling = NullValueHandling.Ignore)]
        public string BanOcg { get; set; }

        [JsonProperty("ban_tcg", NullValueHandling = NullValueHandling.Ignore)]
        public string BanTcg { get; set; }

        [JsonProperty("ban_goat", NullValueHandling = NullValueHandling.Ignore)]
        public string BanGoat { get; set; }
    }
}