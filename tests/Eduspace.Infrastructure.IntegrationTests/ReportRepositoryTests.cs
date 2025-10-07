namespace Eduspace.Infrastructure.IntegrationTests;

using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

using FULLSTACKFURY.EduSpace.API.BreakdownManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.BreakdownManagement.Domain.Model.ValueObjects;
using FULLSTACKFURY.EduSpace.API.BreakdownManagement.Infrastructure.Persistence.EFC.Repositories;

[Collection("appdb")]
public class ReportRepositoryTests
{
    private readonly SqliteAppDbFixture _fx;

    public ReportRepositoryTests(SqliteAppDbFixture fx) => _fx = fx;

    private async Task CleanAsync()
    {
        _fx.Db.Set<Report>().RemoveRange(_fx.Db.Set<Report>());
        await _fx.Db.SaveChangesAsync();
    }

    private static Report NewReport(int resourceId)
    {
        var r = new Report();
        typeof(Report).GetProperty(nameof(Report.ResourceId))!
            .SetValue(r, new ResourceId(resourceId));
        return r;
    }

    private async Task SeedAsync(params Report[] items)
    {
        await _fx.Db.Set<Report>().AddRangeAsync(items);
        await _fx.Db.SaveChangesAsync();
    }

    [Fact]
    public async Task FindByIdAsync_Returns_Saved_Report()
    {
        await CleanAsync();
        var r = NewReport(10); 
        await SeedAsync(r);

        var repo = new ReportRepository(_fx.Db);
        var found = await repo.FindByIdAsync(r.Id);

        found.Should().NotBeNull();
        found!.Id.Should().Be(r.Id);
    }

    [Fact]
    public async Task FindAllAsync_Returns_All_Reports()
    {
        await CleanAsync();
        await SeedAsync(NewReport(1), NewReport(2), NewReport(3)); 
        var repo = new ReportRepository(_fx.Db);
        var all = await repo.FindAllAsync();

        all.Should().NotBeNull();
        all!.Count().Should().Be(3);
    }

    [Fact]
    public async Task FindAllByResourceIdAsync_Returns_Only_Matching()
    {
        await CleanAsync();

        var a = NewReport(10);
        var b = NewReport(20);
        var c = NewReport(10);
        await SeedAsync(a, b, c);

        var repo = new ReportRepository(_fx.Db);
        var list = await repo.FindAllByResourceIdAsync(10);

        list.Should().HaveCount(2).And.OnlyContain(x => x.ResourceId.Equals(new ResourceId(10)));
    }
}
