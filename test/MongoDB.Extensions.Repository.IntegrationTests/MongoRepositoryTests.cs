using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using MongoDB.Extensions.Repository.IntegrationTests.Breakfast;
using Xunit;
using Xunit.Abstractions;

namespace MongoDB.Extensions.Repository.IntegrationTests
{
    public class MongoRepositoryTests
    {
        private readonly ITestOutputHelper _output;

        public MongoRepositoryTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task When_getting_one()
        {
            using var fixture = await MongoTestFixture.NewAsync(_output);
            var entity = await fixture.Repository.GetAsync(BreakfastItemEntityConfiguration.Seed.Id);
            entity.Should().BeEquivalentTo(BreakfastItemEntityConfiguration.Seed);
        }

        [Fact]
        public async Task When_getting_all()
        {
            using var fixture = await MongoTestFixture.NewAsync(_output);
            var entities = await fixture.Repository.GetAllAsync();

            entities.Should().ContainSingle()
                .Which.Should()
                .BeEquivalentTo(BreakfastItemEntityConfiguration.Seed);
        }

        [Fact]
        public async Task When_adding()
        {
            using var fixture = await MongoTestFixture.NewAsync(_output);

            var entity = new BreakfastItem {Name = "Eggs"};
            await fixture.Repository.AddAsync(entity);

            var added = await fixture.Repository.GetAsync(entity.Id);

            added.Should().BeEquivalentTo(entity);
        }

        [Fact]
        public async Task When_adding_many()
        {
            using var fixture = await MongoTestFixture.NewAsync(_output);

            var entities = new[] { new BreakfastItem { Name = "Eggs" }, new BreakfastItem { Name = "Toast" } };
            await fixture.Repository.AddManyAsync(entities);

            var allEntities = await fixture.Repository.GetAllAsync();

            allEntities.Should().BeEquivalentTo(entities.Append(BreakfastItemEntityConfiguration.Seed));
        }

        [Fact]
        public async Task When_deleting()
        {
            using var fixture = await MongoTestFixture.NewAsync(_output);

            var result = await fixture.Repository.DeleteAsync(BreakfastItemEntityConfiguration.Seed.Id);
            var entity = await fixture.Repository.GetAsync(BreakfastItemEntityConfiguration.Seed.Id);

            result.Should().BeTrue();
            entity.Should().BeNull();
        }

        [Fact]
        public async Task When_replacing()
        {
            using var fixture = await MongoTestFixture.NewAsync(_output);

            var entity = new BreakfastItem
            {
                Id = BreakfastItemEntityConfiguration.Seed.Id,
                Name = "Eggs"
            };
            var result = await fixture.Repository.ReplaceAsync(entity);
            
            var replaced = await fixture.Repository.GetAsync(BreakfastItemEntityConfiguration.Seed.Id);

            result.Should().BeEquivalentTo(entity);
            replaced.Should().BeEquivalentTo(entity);
        }
    }
}
