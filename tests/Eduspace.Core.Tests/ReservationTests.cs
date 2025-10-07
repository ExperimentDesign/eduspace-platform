
namespace Eduspace.Core.Tests;

using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

using FULLSTACKFURY.EduSpace.API.EventsScheduling.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.EventsScheduling.Domain.Model.ValueObjects;
using FULLSTACKFURY.EduSpace.API.EventsScheduling.Domain.Model.Commands;

public class ReservationTests
{
    private static DateTime D(int h, int m = 0) => DateTime.UtcNow.Date.AddHours(h).AddMinutes(m);

    [Fact]
    public void Ctor_With_Primitives_Should_Set_All_Fields_And_VOs()
    {
        var r = new Reservation("Reunión 1", D(9), D(10), areaId: 3, teacherId: 7);

        r.Title.Should().Be("Reunión 1");
        r.ReservationDate.Start.Should().Be(D(9));
        r.ReservationDate.End.Should().Be(D(10));
        r.AreaId.Should().Be(new AreaId(3));
        r.TeacherId.Should().Be(new TeacherId(7));
    }

    [Fact]
    public void Ctor_From_Command_Should_Map_Fields()
    {
        var cmd = new CreateReservationCommand(
            "Sprint Planning",
            D(11),
            D(12),
            2,
            5
        );

        var r = new Reservation(cmd);

        r.Title.Should().Be("Sprint Planning");
        r.ReservationDate.Start.Should().Be(D(11));
        r.ReservationDate.End.Should().Be(D(12));

        r.AreaId.Should().Be(new AreaId(2));
        r.TeacherId.Should().Be(new TeacherId(5));
    }



    [Fact]
    public void UpdateReservationDate_Should_Replace_Range()
    {
        var r = new Reservation("X", D(8), D(9), 1, 1);

        r.UpdateReservationDate(D(10), D(11));

        r.ReservationDate.Start.Should().Be(D(10));
        r.ReservationDate.End.Should().Be(D(11));
    }

    [Fact]
    public void UpdateTitle_Should_Update_Value()
    {
        var r = new Reservation("Old", D(8), D(9), 1, 1);

        r.UpdateTitle("New");

        r.Title.Should().Be("New");
    }

    [Fact]
    public void CanReserve_Should_Return_True_When_NoOverlap_Before_Existing()
    {
        var existing = new List<Reservation> { new Reservation("A", D(10), D(11), 1, 1) };
        var candidate = new Reservation("B", D(8), D(9), 1, 1);

        candidate.CanReserve(existing).Should().BeTrue();
    }

    [Fact]
    public void CanReserve_Should_Return_True_When_NoOverlap_After_Existing()
    {
        var existing = new List<Reservation> { new Reservation("A", D(10), D(11), 1, 1) };
        var candidate = new Reservation("B", D(12), D(13), 1, 1);

        candidate.CanReserve(existing).Should().BeTrue();
    }

    [Fact]
    public void CanReserve_Should_Return_False_When_Partial_Overlap()
    {
        var existing = new List<Reservation> { new Reservation("A", D(10), D(11), 1, 1) };
        var candidate = new Reservation("B", D(10), D(12), 1, 1); 
        candidate.CanReserve(existing).Should().BeFalse();
    }

    [Fact]
    public void CanReserve_Should_Return_False_When_Fully_Inside_Existing()
    {
        var existing = new List<Reservation> { new Reservation("A", D(10), D(12), 1, 1) };
        var candidate = new Reservation("B", D(10,30), D(11,30), 1, 1);

        candidate.CanReserve(existing).Should().BeFalse();
    }

    [Fact]
    public void CanReserve_Current_Logic_Disallows_Touching_Boundary()
    {
       var existing = new List<Reservation> { new Reservation("A", D(10), D(11), 1, 1) };
        var candidate = new Reservation("B", D(9), D(10), 1, 1);
        candidate.CanReserve(existing).Should().BeFalse();
    }
}
