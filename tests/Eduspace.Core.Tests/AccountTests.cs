namespace Eduspace.Core.Tests;

using System;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using FluentAssertions;
using Xunit;
using FULLSTACKFURY.EduSpace.API.IAM.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.IAM.Domain.Model.ValueObjects;

public class AccountTests
{
    private static string AnyRoleName() => Enum.GetNames(typeof(ERoles)).First();

    [Fact]
    public void Ctor_Should_Set_Props_And_Parse_Role()
    {
        var roleName = AnyRoleName();
        var acc = new Account("user1", "h123", roleName);

        acc.Username.Should().Be("user1");
        acc.PasswordHash.Should().Be("h123");
        acc.Role.ToString().Should().Be(roleName);
        acc.GetRole().Should().Be(roleName);
    }

    [Fact]
    public void Ctor_Should_Throw_On_Invalid_Role()
    {
        Action act = () => new Account("user1", "h123", "NOT_A_ROLE");
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void UpdateUsername_Should_Change_And_Return_Same_Instance()
    {
        var acc = new Account("old", "h", AnyRoleName());

        var ret = acc.UpdateUsername("new");

        ret.Should().BeSameAs(acc);
        acc.Username.Should().Be("new");
    }

    [Fact]
    public void UpdatePasswordHash_Should_Change_And_Return_Same_Instance()
    {
        var acc = new Account("u", "oldHash", AnyRoleName());

        var ret = acc.UpdatePasswordHash("newHash");

        ret.Should().BeSameAs(acc);
        acc.PasswordHash.Should().Be("newHash");
    }

    [Fact]
    public void DefaultCtor_Should_Leave_Nulls_And_DefaultRole()
    {
        var acc = new Account();

        acc.Username.Should().BeNull();
        acc.PasswordHash.Should().BeNull();
        acc.Role.Should().Be(default(ERoles));
        acc.Id.Should().Be(0);
    }

    [Fact]
    public void PasswordHash_Should_Be_Annotated_With_JsonIgnore()
    {
        var prop = typeof(Account).GetProperty(nameof(Account.PasswordHash))!;
        prop.GetCustomAttribute<JsonIgnoreAttribute>().Should().NotBeNull();
    }
}
