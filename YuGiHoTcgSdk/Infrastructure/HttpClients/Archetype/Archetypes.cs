using Newtonsoft.Json;
using YuGiHoTcgSdk.Infrastructure.HttpClients.Common;

namespace YuGiHoTcgSdk.Infrastructure.HttpClients.Archetype;

public class Archetypes : ResourceBase
{
    internal new static string ApiEndpoint { get; } = Global.ArchetypeApiBase;

    public override string Id { get; set; }

    [JsonProperty("archetype_name")]
    public string ArchetypeName { get; set; }
}