namespace YugihoTcgSdk.Tests
{
    using NUnit.Framework;
    using System.Collections.Generic;
    using YuGiHoTcgSdk.Infrastructure.HttpClients;
    using YuGiHoTcgSdk.Infrastructure.HttpClients.Cards.Monster;
    using YuGiHoTcgSdk.Infrastructure.HttpClients.Cards.Skill;
    using YuGiHoTcgSdk.Infrastructure.HttpClients.Cards.Spell;
    using YuGiHoTcgSdk.Infrastructure.HttpClients.Cards.Token;
    using YuGiHoTcgSdk.Infrastructure.HttpClients.Cards.Trap;
    using YuGiHoTcgSdk.Infrastructure.HttpClients.Set;

    public class Tests
    {
        public YugihoApiClient YugihoClient { get; set; }

        [SetUp]
        public void Setup()
        {
            YugihoClient = new YugihoApiClient();
        }

        [Test]
        public async Task GetSetInfo_ApiResourceAsync()
        {
            // assemble
            var dicObj = new Dictionary<string, string> {{"setcode", "SDY-046"}};
            // act
            var page = await YugihoClient.GetStringResourceAsync<SetInfo>();

            // assert
            var xx = page;
        }

        [Test]
        public async Task GetSets_ApiResourceAsync()
        {
            // assemble
            var dicObj = new Dictionary<string, string> { { "setcode", "SDY-046" } };
            // act
            var page = await YugihoClient.GetApiResourceAsync<Sets>();

            // assert
            var xx = page;
        }

        [Test]
        public async Task GetLinkMonster_ApiResourceAsync()
        {
            // assemble
            var dicObj = new Dictionary<string, string> { { "setcode", "SDY-046" } };
            // act
            var page = await YugihoClient.GetApiResourceAsync<LinkMonster>();

            // assert
            var xx = page;
        }

        [Test]
        public async Task GetSpellCards_ApiResourceAsync()
        {
            // assemble
            var dicObj = new Dictionary<string, string> { { "setcode", "SDY-046" } };
            // act
            var page = await YugihoClient.GetApiResourceAsync<SpellCard>();

            // assert
            var xx = page;
        }

        [Test]
        public async Task GetTrapCards_ApiResourceAsync()
        {
            // assemble
            var dicObj = new Dictionary<string, string> { { "setcode", "SDY-046" } };
            // act
            var page = await YugihoClient.GetApiResourceAsync<TrapCard>();

            // assert
            var xx = page;
        }

        [Test]
        public async Task GetTokenCards_ApiResourceAsync()
        {
            // assemble
            var dicObj = new Dictionary<string, string> { { "setcode", "SDY-046" } };
            // act
            var page = await YugihoClient.GetApiResourceAsync<TokenCard>();

            // assert
            var xx = page;
        }

        [Test]
        public async Task GetSkillCards_ApiResourceAsync()
        {
            // assemble
            var dicObj = new Dictionary<string, string> { { "setcode", "SDY-046" } };
            // act
            var page = await YugihoClient.GetApiResourceAsync<SkillCard>();

            // assert
            var xx = page;
        }
    }
}