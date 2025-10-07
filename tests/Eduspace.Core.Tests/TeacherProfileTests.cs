namespace Eduspace.Core.Tests;

using FluentAssertions;
using Xunit;
using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.ValueObjects;
using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.Commands;

public class TeacherProfileTests
{
    [Fact]
    public void Ctor_With_Primitives_Should_Set_AdministratorId()
    {
        var tp = new TeacherProfile(
            firstName: "Ana",
            lastName: "Pérez",
            email: "ana@upc.edu",
            dni: "12345678",
            address: "Av. X 123",
            phone: "999999999",
            accountId: new AccountId(123),
            administratorId: 77
        );

        tp.AdministratorId.Should().Be(77);
    }

    [Fact]
    public void Ctor_From_Command_Should_Set_AdministratorId()
    {
        var cmd = new CreateTeacherProfileCommand(
            "Ana", "Pérez", "ana@upc.edu", "12345678", "Av. X 123", "999999999",
            77,                       
            "ana.perez",              
            "Secret#123"              
        );

        var tp = new TeacherProfile(cmd, new AccountId(123));

        tp.AdministratorId.Should().Be(77);
    }
}
