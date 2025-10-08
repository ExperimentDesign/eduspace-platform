using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Eduspace.Api.IntegrationTests
{
    public class ResourcesControllerIT : IClassFixture<ApiFactory>
    {
        private readonly HttpClient _client;

        public ResourcesControllerIT(ApiFactory factory)
        {
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task GetAllResourcesByClassroomId_EmptyDb_ReturnsOk_AndEmptyArray()
        {
            var res = await _client.GetAsync("/api/v1/classrooms/1/resources");
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);

            var list = await res.Content.ReadFromJsonAsync<List<object>>();
            Assert.NotNull(list);
            Assert.Empty(list!);
        }

        [Fact]
        public async Task GetResourceById_NotExistingOrWrongClassroom_ReturnsNotFound()
        {
            var res = await _client.GetAsync("/api/v1/classrooms/999/resources/12345");
            Assert.Equal(HttpStatusCode.NotFound, res.StatusCode);
        }
    }
}
