using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using AutoFixture;
using FluentAssertions;
using MongoDB.Driver;
using MongoDB.Extensions.Repository.Interfaces;
using Moq;
using Xunit;
using Xunit.Spec;

namespace MongoDB.Extensions.Repository.Tests
{
    public abstract class MongoRepositorySpec<TResult> : ResultSpec<MongoRepository<SomeEntity>, TResult>
    {
        protected CancellationToken CancellationToken;

        protected override Task ArrangeAsync(AutoMock mock)
        {
            CancellationToken = new CancellationToken();

            var collection = mock.Mock<IMongoCollection<SomeEntity>>();
            mock.Mock<IMongoContext>()
                .Setup(x => x.GetCollectionAsync<SomeEntity>(CancellationToken))
                .ReturnsAsync(collection.Object)
                .Verifiable();

            return Task.CompletedTask;
        }
    }

    public abstract class ReadableMongoRepositorySpec<TResult> : MongoRepositorySpec<TResult>
    {
        protected virtual IEnumerable<SomeEntity> Data { get; } = Enumerable.Empty<SomeEntity>();

        protected override async Task ArrangeAsync(AutoMock mock)
        {
            // Configure the cursor to return a single batch.
            var cursor = mock.Mock<IAsyncCursor<SomeEntity>>();
            cursor.Setup(x => x.Dispose());
            cursor.SetupSequence(c => c.MoveNextAsync(CancellationToken))
                  .ReturnsAsync(true)
                  .ReturnsAsync(false);
            cursor.Setup(c => c.Current)
                  .Returns(() => Data);

            // Currently no way to check filter definition. Could use rendered BSON document?
            mock.Mock<IMongoCollection<SomeEntity>>()
                .Setup(x => x.FindAsync(It.IsAny<FilterDefinition<SomeEntity>>(), It.IsAny<FindOptions<SomeEntity, SomeEntity>>(), CancellationToken))
                .ReturnsAsync(cursor.Object)
                .Verifiable();

            await base.ArrangeAsync(mock);
        }
    }

    public class When_getting_single_entity_by_id : ReadableMongoRepositorySpec<SomeEntity>
    {
        private string _id;
        private SomeEntity _expected;

        protected override IEnumerable<SomeEntity> Data => Fixture.CreateMany<SomeEntity>(10).Prepend(_expected);

        protected override Task<SomeEntity> ActAsync(MongoRepository<SomeEntity> subject) => subject.GetAsync(_id);
        
        protected override async Task ArrangeAsync(AutoMock mock)
        {
            _id = Faker.Random.AlphaNumeric(10);
            _expected = Fixture.Create<SomeEntity>();
            await base.ArrangeAsync(mock);
        }

        [Fact] public void It_should_not_return_null() => Result.Should().NotBeNull();

        [Fact] public void It_should_return_expected_entity() => Result.Should().BeSameAs(_expected);
    }

    public class When_getting_all_entities : ReadableMongoRepositorySpec<ICollection<SomeEntity>>
    {
        private ICollection<SomeEntity> _expected;

        protected override IEnumerable<SomeEntity> Data => _expected;

        protected override Task<ICollection<SomeEntity>> ActAsync(MongoRepository<SomeEntity> subject) => subject.GetAllAsync();
        
        protected override async Task ArrangeAsync(AutoMock mock)
        {
            _expected = Fixture.CreateMany<SomeEntity>(10).ToList();
            await base.ArrangeAsync(mock);
        }

        [Fact] public void It_should_not_return_null() => Result.Should().NotBeNull();

        [Fact] public void It_should_return_expected_entities() => Result.Should().BeEquivalentTo(_expected);
    }

    public class When_adding_one_entity : MongoRepositorySpec<bool>
    {
        private SomeEntity _entity;

        protected override async Task<bool> ActAsync(MongoRepository<SomeEntity> subject)
        {
            await subject.AddAsync(_entity, CancellationToken);
            return true;
        }

        protected override async Task ArrangeAsync(AutoMock mock)
        {
            _entity = Fixture.Create<SomeEntity>();
            mock.Mock<IMongoCollection<SomeEntity>>()
                .Setup(x => x.InsertOneAsync(_entity, It.IsAny<InsertOneOptions>(), CancellationToken))
                .Returns(Task.CompletedTask)
                .Verifiable();
            await base.ArrangeAsync(mock);
        }

        [Fact] public void It_should_run() => Result.Should().BeTrue();
    }

    public class When_adding_many_entities : MongoRepositorySpec<bool>
    {
        private ICollection<SomeEntity> _entities;

        protected override async Task<bool> ActAsync(MongoRepository<SomeEntity> subject)
        {
            await subject.AddManyAsync(_entities, CancellationToken);
            return true;
        }

        protected override async Task ArrangeAsync(AutoMock mock)
        {
            _entities = Fixture.CreateMany<SomeEntity>(10).ToList();
            mock.Mock<IMongoCollection<SomeEntity>>()
                .Setup(x => x.InsertManyAsync(_entities, It.IsAny<InsertManyOptions>(), CancellationToken))
                .Returns(Task.CompletedTask)
                .Verifiable();
            await base.ArrangeAsync(mock);
        }

        [Fact] public void It_should_run() => Result.Should().BeTrue();
    }
}
