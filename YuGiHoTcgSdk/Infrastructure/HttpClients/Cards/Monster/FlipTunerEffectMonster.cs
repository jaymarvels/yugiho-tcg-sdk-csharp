using YuGiHoTcgSdk.Infrastructure.HttpClients.Common;

namespace YuGiHoTcgSdk.Infrastructure.HttpClients.Cards.Monster;

public class FlipTunerEffectMonster : Monster
{
    internal new static string ApiEndpoint { get; } = $"{Global.CardApiBase}?type={Global.Monster.FlipTunerEffect}";
}