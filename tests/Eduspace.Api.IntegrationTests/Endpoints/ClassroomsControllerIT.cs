using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Eduspace.Api.IntegrationTests
{
    public class ClassroomsControllerIT : IClassFixture<ApiFactory>
    {
        private readonly HttpClient _client;

        public ClassroomsControllerIT(ApiFactory factory)
        {
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task GetAllClassrooms_EmptyDb_ReturnsOk_AndEmptyArray()
        {
            var res = await _client.GetAsync("/api/v1/Classrooms");
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);

            var list = await res.Content.ReadFromJsonAsync<List<object>>();
            Assert.NotNull(list);
            Assert.Empty(list!);
        }

        [Fact]
        public async Task GetClassroomById_NotExisting_ReturnsNotFound()
        {
            var res = await _client.GetAsync("/api/v1/Classrooms/999999");
            Assert.Equal(HttpStatusCode.NotFound, res.StatusCode);
        }
    }
}
