using System;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;
using FULLSTACKFURY.EduSpace.API.Shared.Infrastructure.Persistence.EFC.Configuration; // AppDbContext

public class SqliteAppDbFixture : IDisposable
{
    public DbConnection Connection { get; }
    public AppDbContext Db { get; }

    public SqliteAppDbFixture()
    {
        Connection = new SqliteConnection("DataSource=:memory:");
        Connection.Open();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(Connection)
            .EnableSensitiveDataLogging()
            .Options;

        Db = new AppDbContext(options);
        Db.Database.EnsureCreated();
    }

    public void Dispose()
    {
        Db?.Dispose();
        Connection?.Dispose();
    }
}

[CollectionDefinition("appdb")]
public class AppDbCollection : ICollectionFixture<SqliteAppDbFixture> { }
