using Newtonsoft.Json;

namespace YuGiHoTcgSdk.Infrastructure.HttpClients.Cards.Model
{
    public class CardPrice
    {
        [JsonProperty("cardmarket_price")]
        public string CardmarketPrice { get; set; }

        [JsonProperty("tcgplayer_price")]
        public string TcgplayerPrice { get; set; }

        [JsonProperty("ebay_price")]
        public string EbayPrice { get; set; }

        [JsonProperty("amazon_price")]
        public string AmazonPrice { get; set; }

        [JsonProperty("coolstuffinc_price")]
        public string CoolstuffincPrice { get; set; }
    }
}