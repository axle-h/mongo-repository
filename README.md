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

Configuration is easy in ASP.Net Core applications. In `Startup.cs`:

```C#
public void ConfigureServices(IServiceCollection services)
{
    var assembly = typeof(Startup).Assembly;
    services.AddMongoRepositories(_configuration.GetConnectionString("mongo"))
            .FromAssembly(assembly) // Registers all classes that implement IMongoRepository<TEntity>
            .WithIndexesFromAssembly(assembly); // Registers all classes that implement MongoIndexProfile<TEntity>
}
```

## Indexes

Configured indexes will be created the first time that a collection is accessed.
To configure indexes, implement `MongoIndexProfile<TEntity>` for example, to create an ascending, case insensitive, unique index on `SomeEntity.Name` called `name_unique`:

```C#
public class SomeIndexProfile : MongoIndexProfile<SomeEntity>
{
    public SomeIndexProfile()
    {
        Add("name_unique", IndexKeys.Ascending(x => x.Name), o => o.Unique().WithCaseInsensitiveCollation());
    }
}
```