namespace YugihoTcgSdk.Tests
{
    using NUnit.Framework;
    using System.Collections.Generic;
    using YuGiHoTcgSdk.Infrastructure.HttpClients;
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
        public async Task GetSet_ApiResourcePageAsync_Pagination()
        {
            // assemble
            var dicObj = new Dictionary<string, string> {{"setcode", "SDY-046"}};
            // act
            var page = await YugihoClient.GetStringResourceAsync<SetInfo>();

            // assert
            var xx = page;
        }
    }
}