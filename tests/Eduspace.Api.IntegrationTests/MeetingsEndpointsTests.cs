namespace Eduspace.Api.IntegrationTests;

using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using FluentAssertions;
using Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using FULLSTACKFURY.EduSpace.API.ReservationScheduling.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.ReservationScheduling.Domain.Model.Commands;
using FULLSTACKFURY.EduSpace.API.ReservationScheduling.Domain.Model.Queries;
using FULLSTACKFURY.EduSpace.API.ReservationScheduling.Domain.Services;

public class MeetingsEndpointsTests : IClassFixture<ApiFactory>
{
    private readonly HttpClient _client;

    public MeetingsEndpointsTests(ApiFactory baseFactory)
    {
        var factory = baseFactory.WithWebHostBuilder(b =>
        {
            b.ConfigureTestServices(s =>
            {
                s.RemoveAll<IMeetingCommandService>();
                s.RemoveAll<IMeetingQueryService>();
                s.AddSingleton<IMeetingCommandService, FakeMeetingCommandService>();
                s.AddSingleton<IMeetingQueryService,  FakeMeetingQueryService>();
            });
        });

        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAllMeetings_Returns_OK_And_Array()
    {
        var res = await _client.GetAsync("/api/v1/meetings");
        res.StatusCode.Should().Be(HttpStatusCode.OK);

        var json = await res.Content.ReadFromJsonAsync<JsonElement>();
        json.ValueKind.Should().Be(JsonValueKind.Array);
    }

    [Fact]
    public async Task CreateMeeting_Returns_OK_And_Resource()
    {
        var payload = new
        {
            title = "Sprint planning",
            description = "Reunión semanal",
            date = DateOnly.FromDateTime(DateTime.UtcNow.Date),
            start = new TimeOnly(10, 0),
            end   = new TimeOnly(11, 0)
        };

        var res = await _client.PostAsJsonAsync("/api/v1/administrators/1/classrooms/101/meetings", payload);
        res.StatusCode.Should().Be(HttpStatusCode.OK);

        var json = await res.Content.ReadFromJsonAsync<JsonElement>();
        json.GetProperty("title").GetString().Should().Be("Sprint planning");
    }
}


file sealed class FakeMeetingCommandService : IMeetingCommandService
{
    private static readonly List<Meeting> _store = FakeMemory.Meetings;

    public Task<Meeting?> Handle(CreateMeetingCommand command)
    {
        var m = new Meeting(
            command.Title,
            command.Description,
            command.Date,
            command.Start,
            command.End,
            command.AdministratorId,
            command.ClassroomId
        );

        var nextId = _store.Count == 0 ? 1 : _store.Max(x => x.Id) + 1;
        typeof(Meeting).GetProperty(nameof(Meeting.Id))!
            .SetValue(m, nextId);

        _store.Add(m);
        return Task.FromResult<Meeting?>(m);
    }

    public Task<Meeting?> Handle(UpdateMeetingCommand command)
    {
        var m = _store.FirstOrDefault(x => x.Id == command.Id);
        if (m == null) return Task.FromResult<Meeting?>(null);

        m.UpdateTitle(command.Title);
        m.UpdateDescription(command.Description);
        m.UpdateDate(command.Date);
        m.UpdateTime(command.Start, command.End);
        m.UpdateAdministrator(command.AdministratorId, _ => true);
        m.UpdateClassroom(command.ClassroomId, _ => true);

        return Task.FromResult<Meeting?>(m);
    }

    public Task Handle(DeleteMeetingCommand command)
    {
        _store.RemoveAll(x => x.Id == command.Id);
        return Task.CompletedTask;
    }

    public Task Handle(AddTeacherToMeetingCommand command)
    {
        return Task.CompletedTask;
    }
}

file sealed class FakeMeetingQueryService : IMeetingQueryService
{
    private static readonly List<Meeting> _store = FakeMemory.Meetings;

    public Task<IEnumerable<Meeting>> Handle(GetAllMeetingsQuery _)
        => Task.FromResult<IEnumerable<Meeting>>(_store.ToList());

    public Task<IEnumerable<Meeting>> Handle(GetMeetingByIdQuery q)
        => Task.FromResult<IEnumerable<Meeting>>(_store.Where(m => m.Id == q.Id));

    public Task<IEnumerable<Meeting>> Handle(GetAllMeetingByAdminIdQuery q)
        => Task.FromResult<IEnumerable<Meeting>>(_store.Where(m => m.AdministratorId.Value == q.AdministratorId));
}

file static class FakeMemory
{
    public static List<Meeting> Meetings { get; } = new();
}
