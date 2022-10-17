namespace YuGiHoTcgSdk.Infrastructure.HttpClients.Cards.Monster;

using Common;

public class LinkMonster : Monster
{
    internal new static string ApiEndpoint { get; } = $"{Global.CardApiBase}?type={Global.Monster.Link}";
}