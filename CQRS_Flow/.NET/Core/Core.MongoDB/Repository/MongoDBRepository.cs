using Core.Aggregates;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using IAggregate = Core.Aggregates.IAggregate;

namespace Core.MongoDB.Repository;

public class MongoDbRepository<T> : Repositories.IRepository<T> where T : class, IAggregate, new()
{
    private readonly IMongoCollection<T> mongoCollection;

    public MongoDbRepository(IOptions<MongoDbConfig> config, IMongoClient client)
    {
        mongoCollection = client.GetDatabase($"{nameof(T)}Store").GetCollection<T>(nameof(T));
    }

    public async Task<T?> Find(Guid id, CancellationToken cancellationToken)
    {
        var response = await mongoCollection.FindAsync(x => x.Id == id, cancellationToken: cancellationToken);
        return await response.FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public Task Add(T aggregate, CancellationToken cancellationToken)
    {
        return mongoCollection.InsertOneAsync(aggregate, cancellationToken: cancellationToken);
    }

    public Task Update(T aggregate, CancellationToken cancellationToken)
    {
        return mongoCollection.ReplaceOneAsync(x => x.Id == aggregate.Id, aggregate, cancellationToken: cancellationToken);
    }

    public Task Delete(T aggregate, CancellationToken cancellationToken)
    {
        return mongoCollection.DeleteOneAsync(x => x.Id == aggregate.Id, cancellationToken: cancellationToken);
    }
}
