namespace Eduspace.Core.Tests;

using System;
using FluentAssertions;
using Xunit;
using FULLSTACKFURY.EduSpace.API.ReservationScheduling.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.ReservationScheduling.Domain.Model.ValueObjects;
using FULLSTACKFURY.EduSpace.API.ReservationScheduling.Domain.Model.Commands;

public class MeetingTests
{
    private static DateOnly Today() => DateOnly.FromDateTime(DateTime.UtcNow.Date);
    private static TimeOnly T(int h, int m = 0) => new TimeOnly(h, m);

    [Fact]
    public void Ctor_With_Primitives_Should_Map_All_Fields_And_VOs()
    {
        var m = new Meeting(
            title: "Demo",
            description: "Sprint review",
            date: Today(),
            start: T(9),
            end: T(10),
            administratorId: 3,
            classroomId: 7
        );

        m.Title.Should().Be("Demo");
        m.Description.Should().Be("Sprint review");
        m.Date.Should().Be(Today());
        m.StartTime.Should().Be(T(9));
        m.EndTime.Should().Be(T(10));

        m.AdministratorId.Should().Be(new AdministratorId(3));
        m.ClassroomId.Should().Be(new ClassroomId(7));
    }

    [Fact]
    public void Ctor_From_CreateMeetingCommand_Should_Map_All_Fields()
    {
        var cmd = new CreateMeetingCommand(
            "Planificación", "Sprint 12", Today(), T(11), T(12), 5, 9
        );

        var m = new Meeting(cmd);

        m.Title.Should().Be("Planificación");
        m.Description.Should().Be("Sprint 12");
        m.Date.Should().Be(Today());
        m.StartTime.Should().Be(T(11));
        m.EndTime.Should().Be(T(12));
        m.AdministratorId.Should().Be(new AdministratorId(5));
        m.ClassroomId.Should().Be(new ClassroomId(9));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void UpdateTitle_Should_Ignore_Null_Or_Empty(string? invalid)
    {
        var m = new Meeting("Demo", "Desc", Today(), T(9), T(10), 1, 1);

        m.UpdateTitle(invalid);

        m.Title.Should().Be("Demo");
    }

    [Fact]
    public void UpdateTitle_Should_Apply_When_Not_Empty()
    {
        var m = new Meeting("Demo", "Desc", Today(), T(9), T(10), 1, 1);

        m.UpdateTitle("Nueva");

        m.Title.Should().Be("Nueva");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void UpdateDescription_Should_Ignore_Null_Or_Empty(string? invalid)
    {
        var m = new Meeting("Demo", "Desc", Today(), T(9), T(10), 1, 1);

        m.UpdateDescription(invalid);

        m.Description.Should().Be("Desc");
    }

    [Fact]
    public void UpdateDate_Should_Update_When_HasValue()
    {
        var m = new Meeting("Demo", "Desc", Today(), T(9), T(10), 1, 1);
        var newDate = Today().AddDays(1);

        m.UpdateDate(newDate);

        m.Date.Should().Be(newDate);
    }

    [Fact]
    public void UpdateTime_Should_Update_Start_Only_And_End_Only()
    {
        var m = new Meeting("Demo", "Desc", Today(), T(9), T(10), 1, 1);

        m.UpdateTime(start: T(8), end: null);
        m.StartTime.Should().Be(T(8));
        m.EndTime.Should().Be(T(10));

        m.UpdateTime(start: null, end: T(11));
        m.StartTime.Should().Be(T(8));
        m.EndTime.Should().Be(T(11));
    }

    [Fact]
    public void UpdateAdministrator_Should_Not_Change_On_Null_Or_Invalid()
    {
        var m = new Meeting("Demo", "Desc", Today(), T(9), T(10), 10, 1);
        var before = m.AdministratorId;

         m.UpdateAdministrator(null, _ => true);
        m.AdministratorId.Should().Be(before);

        m.UpdateAdministrator(99, _ => false);
        m.AdministratorId.Should().Be(before);
    }

    [Fact]
    public void UpdateAdministrator_Should_Update_When_Validator_True()
    {
        var m = new Meeting("Demo", "Desc", Today(), T(9), T(10), 10, 1);

        m.UpdateAdministrator(42, id => id == 42);

        m.AdministratorId.Should().Be(new AdministratorId(42));
    }

    [Fact]
    public void UpdateClassroom_Should_Not_Change_On_Null_Or_Invalid()
    {
        var m = new Meeting("Demo", "Desc", Today(), T(9), T(10), 1, 10);
        var before = m.ClassroomId;

        m.UpdateClassroom(null, _ => true);
        m.ClassroomId.Should().Be(before);

        m.UpdateClassroom(77, _ => false);
        m.ClassroomId.Should().Be(before);
    }

    [Fact]
    public void UpdateClassroom_Should_Update_When_Validator_True()
    {
        var m = new Meeting("Demo", "Desc", Today(), T(9), T(10), 1, 10);

        m.UpdateClassroom(21, id => id == 21);

        m.ClassroomId.Should().Be(new ClassroomId(21));
    }
}
