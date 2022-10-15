using Newtonsoft.Json;
using System;

namespace YuGiHoTcgSdk.Infrastructure.HttpClients.Cards.Model
{
    public class CardImage
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("image_url")]
        public Uri ImageUrl { get; set; }

        [JsonProperty("image_url_small")]
        public Uri ImageUrlSmall { get; set; }
    }
}