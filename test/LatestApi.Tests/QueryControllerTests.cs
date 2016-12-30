using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Xunit;
using LatestApi.Queries;

namespace LatestApi.Tests
{
    // see example explanation on xUnit.net website:
    // https://xunit.github.io/docs/getting-started-dotnet-core.html
    public class QueryControllerTests
    {
        private TestServer Server;
        private HttpClient Client;

        public QueryControllerTests()
        {
            Server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            Client = Server.CreateClient();
        }

        [Fact]
        public async void BlogQuery()
        {
            var content = new StringContent("{}", Encoding.UTF8, "application/json");
            var response = Client.PostAsync("/api/query/BlogQuery", content).Result;

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BlogData>(responseString);

            Assert.NotNull(result);
            Assert.NotNull(result.Title);
        }

        [Fact]
        public async void GitHubQuery()
        {
            var content = new StringContent("{ 'Username': 'hlaueriksson' }", Encoding.UTF8, "application/json");
            var response = Client.PostAsync("/api/query/GitHubQuery", content).Result;

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GitHubData>(responseString);

            Assert.NotNull(result);
            Assert.NotNull(result.Repo);
            Assert.NotNull(result.Commit);
        }

        [Fact]
        public async void InstagramQuery()
        {
            var content = new StringContent("{}", Encoding.UTF8, "application/json");
            var response = Client.PostAsync("/api/query/InstagramQuery", content).Result;

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<InstagramData>(responseString);

            Assert.NotNull(result);
            Assert.NotNull(result.Html);
        }
    }
}
