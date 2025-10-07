namespace Eduspace.Core.Tests;
using FluentAssertions;
using Xunit;

public class SmokeTests
{
    [Fact] public void Framework_Is_Working() => (1 + 1).Should().Be(2);
}
