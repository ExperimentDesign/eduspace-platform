namespace Eduspace.Api.IntegrationTests;

using System.Net;
using System.Text.Json;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

public class ReservationsEndpointsTests : IClassFixture<ApiFactory>
{
    private readonly HttpClient _client;
    public ReservationsEndpointsTests(ApiFactory f) => _client = f.CreateClient();

    [Fact]
    public async Task GetAllReservations_Returns_OK_And_Array()
    {
        var res = await _client.GetAsync("/api/v1/reservations");
        res.StatusCode.Should().Be(HttpStatusCode.OK);

        var json = await res.Content.ReadFromJsonAsync<JsonElement>();
        json.ValueKind.Should().Be(JsonValueKind.Array);
    }

    [Fact]
    public async Task GetAllReservationsByArea_Returns_OK_And_Array()
    {
        var res = await _client.GetAsync("/api/v1/areas/1/reservations");
        res.StatusCode.Should().Be(HttpStatusCode.OK);

        var json = await res.Content.ReadFromJsonAsync<JsonElement>();
        json.ValueKind.Should().Be(JsonValueKind.Array);
    }
}
