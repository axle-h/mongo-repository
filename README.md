[![CircleCI](https://circleci.com/gh/axle-h/mongo-repository/tree/master.svg?&style=shield)](https://circleci.com/gh/axle-h/mongo-repository/tree/master)
[![NuGet](https://img.shields.io/nuget/v/MongoDB.Extensions.Repository.svg)](https://www.nuget.org/packages/MongoDB.Extensions.Repository/)

# mongo-repository

Repository pattern for the official MongoDB .NET Core driver.

## Installation

The package is available on NuGet.

```bash
dotnet add package MongoDB.Extensions.Repository
```

## Basic Usage

For basic, generic access, inject `IMongoRepository<TEntity>` into your services for example:

```C#
public class SomeService
{
    private readonly IMongoRepository<SomeEntity> _repository;

    public SomeService(IMongoRepository<SomeEntity> repository)
    {
        _repository = repository;
    }

    public async Task<SomeEntity> GetSomeDataAsync(string id) => await _repository.GetAsync(id);
}
```

By default, collections will be named in lower `snake_case`.

## Configuration

This library uses `Microsoft.Extensions.DependencyInjection`. E.g. in an ASP.Net Core `Startup.cs`.

```C#
public void ConfigureServices(IServiceCollection services)
{
    var assembly = typeof(Startup).Assembly;
    services.AddMongoRepositories(_configuration.GetConnectionString("mongo"))
            // Registers all classes that implement IMongoRepository<> or IMongoEntityConfiguration<>
            .FromAssembly(assembly);
}
```

Entity specific configuration is provided via implementations of `IMongoEntityConfiguration<>`.

```C#
public class BreakfastItemEntityConfiguration : IMongoEntityConfiguration<BreakfastItem>
{
    public void Configure(MongoEntityBuilder<BreakfastItem> context)
    {
        context.Indexes.Add("name_unique",
            Builders<BreakfastItem>.IndexKeys.Ascending(x => x.Name),
            o => o.Unique().WithCaseInsensitiveCollation());

        context.Seed.Add(new BreakfastItem
        {
            Id = "5e0b29f7a2077ef4078c049b",
            Name = "Bacon"
        });
    }
}
```