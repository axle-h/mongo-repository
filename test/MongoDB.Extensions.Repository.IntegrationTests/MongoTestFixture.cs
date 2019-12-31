using System;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Extensions.Repository.Extensions;
using MongoDB.Extensions.Repository.IntegrationTests.Breakfast;
using MongoDB.Extensions.Repository.Interfaces;
using Xunit.Abstractions;

namespace MongoDB.Extensions.Repository.IntegrationTests
{
    public class MongoTestFixture : IDisposable
    {
        private readonly ServiceProvider _provider;
        
        public MongoTestFixture(ServiceProvider provider)
        {
            _provider = provider;
            Repository = provider.GetRequiredService<IMongoRepository<BreakfastItem>>();
        }

        public IMongoRepository<BreakfastItem> Repository { get; }

        public void Dispose()
        {
            _provider.Dispose();
        }

        public static async Task<MongoTestFixture> NewAsync(ITestOutputHelper output)
        {
            var type = output.GetType();
            var testMember = type.GetField("test", BindingFlags.Instance | BindingFlags.NonPublic);
            var test = (ITest)testMember?.GetValue(output) ?? throw new Exception("Cannot get current test");

            var testDatabase = Convert.ToBase64String(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(test.DisplayName)))
                .Replace('/', '_')
                .Replace('+', '-')
                .TrimEnd('=');

            var connectionString = (Environment.GetEnvironmentVariable("CONNECTIONSTRINGS__MONGO") ?? "mongodb://localhost:27017") + "/" + testDatabase;

            var services = new ServiceCollection();

            services.AddMongoRepositories(connectionString)
                .FromAssemblyContaining<BreakfastItem>();

            var provider = services.BuildServiceProvider();

            await provider.GetRequiredService<IMongoContext>().DropCollectionAsync<BreakfastItem>();

            return new MongoTestFixture(provider);
        }
    }
}