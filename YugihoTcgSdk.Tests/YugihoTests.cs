namespace YugihoTcgSdk.Tests
{
    using NUnit.Framework;
    using System.Collections.Generic;
    using YuGiHoTcgSdk.Features.Common;
    using YuGiHoTcgSdk.Features.FilterBuilder;
    using YuGiHoTcgSdk.Infrastructure.HttpClients;
    using YuGiHoTcgSdk.Infrastructure.HttpClients.Archetype;
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
            var page = await YugihoClient.GetSetInfoResourceAsync<SetInfo>(dicObj);

            // assert
            Assert.That(page.Id, Is.EqualTo("54652250"));
            Assert.That(page.SetCode, Is.EqualTo("SDY-046"));
            Assert.That(page.SetName, Is.EqualTo("Starter Deck: Yugi"));
            Assert.That(page.SetRarity, Is.EqualTo("Common"));
        }

        [Test]
        public async Task GetSets_ApiResourceAsync()
        {
            // act
            var page = await YugihoClient.GetArrayResourceAsync<Sets>();

            // assert
            Assert.That(page.Count, Is.AtLeast(1));
            Assert.That(page.Any(x => x.SetCode == "YS15"));
        }

        [Test]
        public async Task GetArchetypes_ApiResourceAsync()
        {
            // act
            var page = await YugihoClient.GetArrayResourceAsync<Archetypes>();

            // assert
            Assert.That(page.Count, Is.AtLeast(1));
            Assert.That(page.Any(x => x.ArchetypeName == "ABC"));
        }

        [Test]
        public async Task GetLinkMonster_ApiResourceAsync()
        {
            //assemble
            string type = "Link Monster";

            // act
            var page = await YugihoClient.GetApiResourceAsync<LinkMonster>();

            // assert
            Assert.Multiple(() =>
            {
                Assert.That(page.Results.Any);
                Assert.That(page.Results.FirstOrDefault()?.Type, Is.EqualTo(type));
                Assert.That(page.Results.LastOrDefault()?.Type, Is.EqualTo(type));
            });
        }

        [Test]
        public async Task GetAllMonsters_ApiResourceAsync()
        {
            // act
            var page = await YugihoClient.GetApiResourceAsync<AllMonsters>();

            // assert

            Assert.That(page.Results.Any);

        }

        [Test]
        public async Task GetSpellCards_ApiResourceAsync()
        {
            //assemble
            string type = "Spell Card";

            // act
            var page = await YugihoClient.GetApiResourceAsync<SpellCard>();

            // assert
            Assert.Multiple(() =>
            {
                Assert.That(page.Results.Any);
                Assert.That(page.Results.FirstOrDefault()?.Type, Is.EqualTo(type));
                Assert.That(page.Results.LastOrDefault()?.Type, Is.EqualTo(type));
            });
        }

        [Test]
        public async Task GetTrapCards_ApiResourceAsync()
        {
            //assemble
            string type = "Trap Card";

            // act
            var page = await YugihoClient.GetApiResourceAsync<TrapCard>();

            // assert
            Assert.Multiple(() =>
            {
                Assert.That(page.Results.Any);
                Assert.That(page.Results.FirstOrDefault()?.Type, Is.EqualTo(type));
                Assert.That(page.Results.LastOrDefault()?.Type, Is.EqualTo(type));
            });
        }

        [Test]
        public async Task GetTrapCardsFiltered_ApiResourceAsync()
        {
            //assemble
            string type = "Normal";
            var filter = CardFilterBuilder.CreateCardFilter().AddFilter("Normal", nameof(TrapCard.Race));

            // act
            var page = await YugihoClient.GetApiResourceAsync<TrapCard>(filter);

            // assert
            Assert.Multiple(() =>
            {
                Assert.That(page.Results.Any);
                Assert.That(page.Results.Select(item => item.Race), Is.All.EqualTo(type));
                Assert.That(page.Results.FirstOrDefault()?.Race, Is.EqualTo(type));
                Assert.That(page.Results.LastOrDefault()?.Race, Is.EqualTo(type));
            });
        }

        [Test]
        public async Task GetTrapCardsMultiFiltered_ApiResourceAsync()
        {
            //assemble
            string type = "Normal";
            string cardType = "Legacy of Darkness";
            var filter = CardFilterBuilder.CreateCardFilter().AddFilter("Normal", nameof(TrapCard.Race)).AddCardSet("Legacy of Darkness");

            // act
            var page = await YugihoClient.GetApiResourceAsync<TrapCard>(filter);

            // assert
            Assert.Multiple(() =>
            {
                Assert.That(page.Results.Any);
                Assert.That(page.Results.Select(item => item.Race), Is.All.EqualTo(type));
                Assert.That(page.Results.Select(item => item.CardSets.Select(card => card.SetName)), Is.All.Contains(cardType));
            });
        }

        [Test]
        public async Task GetTokenCards_ApiResourceAsync()
        {
            //assemble
            string type = "Token";

            // act
            var page = await YugihoClient.GetApiResourceAsync<TokenCard>();

            // assert
            Assert.Multiple(() =>
            {
                Assert.That(page.Results.Any);
                Assert.That(page.Results.FirstOrDefault()?.Type, Is.EqualTo(type));
                Assert.That(page.Results.LastOrDefault()?.Type, Is.EqualTo(type));
            });
        }

        [Test]
        public async Task GetSkillCards_ApiResourceAsync()
        {
            //assemble
            string type = "Skill Card";

            // act
            var page = await YugihoClient.GetApiResourceAsync<SkillCard>();

            // assert
            Assert.Multiple(() =>
            {
                Assert.That(page.Results.Any);
                Assert.That(page.Results.FirstOrDefault()?.Type, Is.EqualTo(type));
                Assert.That(page.Results.LastOrDefault()?.Type, Is.EqualTo(type));
            });
        }

        [Test]
        public async Task GetLinkMonsterAtkValue_ApiResourceAsync()
        {
            //assemble
            string type = "Link Monster";
            var filter = CardFilterBuilder.CreateCardFilter().AddAttack(2000, FilterModifer.GreaterThan);

            // act
            var page = await YugihoClient.GetApiResourceAsync<LinkMonster>(filter);

            // assert
            Assert.Multiple(() =>
            {
                Assert.That(page.Results.Any);
                Assert.That(page.Results.Select(x => x.Atk), Is.All.GreaterThan(2000));
                Assert.That(page.Results.FirstOrDefault()?.Type, Is.EqualTo(type));
                Assert.That(page.Results.LastOrDefault()?.Type, Is.EqualTo(type));
            });
        }

        [Test]
        public async Task GetMonster_ByName_ApiResourceAsync()
        {
            //assemble
            string name = "3-Hump Lacooda";
            string name2 = "4-Starred Ladybug of Doom";
            var filter = CardFilterBuilder.CreateCardFilter().AddFilter(name, nameof(Monster.Name)).AddFilter(name2, nameof(Monster.Name));

            // act
            var page = await YugihoClient.GetApiResourceAsync<AllMonsters>(filter);

            // assert
            Assert.Multiple(() =>
            {
                Assert.That(page.Results.Any);
                Assert.That(page.Results.FirstOrDefault()?.Name, Is.EqualTo(name));
                Assert.That(page.Results.LastOrDefault()?.Name, Is.EqualTo(name2));
            });
        }
    }
}



//3-Hump Lacooda