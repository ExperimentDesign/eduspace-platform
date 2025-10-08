using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Eduspace.Api.IntegrationTests
{
    public class MeetingsControllerIT : IClassFixture<ApiFactory>
    {
        private readonly HttpClient _client;

        public MeetingsControllerIT(ApiFactory factory)
        {
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task GetAllMeetings_EmptyDb_ReturnsOk_AndEmptyArray()
        {
            var res = await _client.GetAsync("/api/v1/meetings");

            Assert.Equal(HttpStatusCode.OK, res.StatusCode);

            var list = await res.Content.ReadFromJsonAsync<List<object>>();
            Assert.NotNull(list);
            Assert.Empty(list!); 
        }

        [Fact]
        public async Task DeleteMeeting_NotExistingId_ReturnsNotFound()
        {
            var res = await _client.DeleteAsync("/api/v1/meetings/999999");

            Assert.Equal(HttpStatusCode.NotFound, res.StatusCode);
        }
    }
}
