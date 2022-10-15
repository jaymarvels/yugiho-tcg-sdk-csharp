namespace YuGiHoTcgSdk.Infrastructure.HttpClients.Base;

using System;
using Newtonsoft.Json;

public class Meta
{
    [JsonProperty("current_rows")]
    public long CurrentRows { get; set; }

    [JsonProperty("total_rows")]
    public long TotalRows { get; set; }

    [JsonProperty("rows_remaining")]
    public long RowsRemaining { get; set; }

    [JsonProperty("total_pages")]
    public long TotalPages { get; set; }

    [JsonProperty("pages_remaining")]
    public long PagesRemaining { get; set; }

    [JsonProperty("next_page")]
    public Uri NextPage { get; set; }

    [JsonProperty("next_page_offset")]
    public long NextPageOffset { get; set; }
}