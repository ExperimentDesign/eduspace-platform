namespace Eduspace.Core.Tests;

using System;
using FluentAssertions;
using Xunit;
using FULLSTACKFURY.EduSpace.API.SpacesAndResourceManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.SpacesAndResourceManagement.Domain.Model.ValueObjects;

public class ClassroomTests
{
    [Fact]
    public void Ctor_With_Primitives_Should_Set_Props()
    {
        var name = "A-101";
        var description = "Sala de estudio";
        var teacherId = 7;

        var classroom = new Classroom(name, description, teacherId);

        classroom.Name.Should().Be(name);
        classroom.Description.Should().Be(description);
        classroom.TeacherId.Should().Be(new TeacherId(teacherId));
        classroom.Resources.Should().NotBeNull().And.BeEmpty();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void UpdateName_Should_Ignore_Null_Or_Empty(string invalid)
    {
        var classroom = new Classroom("A-101", "Desc", 1);

        classroom.UpdateName(invalid);

        classroom.Name.Should().Be("A-101"); 
    }

    [Fact]
    public void UpdateName_Should_Apply_When_Not_Empty()
    {
        var classroom = new Classroom("A-101", "Desc", 1);

        classroom.UpdateName("A-102");

        classroom.Name.Should().Be("A-102");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void UpdateDescription_Should_Ignore_Null_Or_Empty(string invalid)
    {
        var classroom = new Classroom("A-101", "Desc", 1);

        classroom.UpdateDescription(invalid);

        classroom.Description.Should().Be("Desc");
    }

    [Fact]
    public void UpdateDescription_Should_Apply_When_Not_Empty()
    {
        var classroom = new Classroom("A-101", "Desc", 1);

        classroom.UpdateDescription("Nueva desc");

        classroom.Description.Should().Be("Nueva desc");
    }

    [Fact]
    public void UpdateTeacherId_Should_Not_Change_When_Null()
    {
        var classroom = new Classroom("A-101", "Desc", 10);
        var before = classroom.TeacherId;

        classroom.UpdateTeacherId(null, _ => true);

        classroom.TeacherId.Should().Be(before); 
    }

    [Fact]
    public void UpdateTeacherId_Should_Not_Change_When_VerifyProfile_False()
    {
        var classroom = new Classroom("A-101", "Desc", 10);
        var before = classroom.TeacherId;

        classroom.UpdateTeacherId(99, _ => false);

        classroom.TeacherId.Should().Be(before);
    }

    [Fact]
    public void UpdateTeacherId_Should_Update_When_VerifyProfile_True()
    {
        var classroom = new Classroom("A-101", "Desc", 10);
        var before = classroom.TeacherId;

        classroom.UpdateTeacherId(42, x => x == 42);

        classroom.TeacherId.Should().NotBe(before);
        classroom.TeacherId.Should().Be(new TeacherId(42));
    }
}
