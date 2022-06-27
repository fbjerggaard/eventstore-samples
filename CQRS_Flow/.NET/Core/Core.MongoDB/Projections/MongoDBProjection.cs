using Core.Events;
using Core.Projections;
using MongoDB.Driver;

namespace Core.MongoDB.Projections;

public class MongoDBProjection<TEvent, TView>: IEventHandler<StreamEvent<TEvent>>
    where TView : class, IProjection
    where TEvent : notnull
{
    private readonly IMongoClient mongoClient;
    private readonly Func<TEvent, string> getId;


    public MongoDBProjection(IMongoClient mongoClient, Func<TEvent, string> getId)
    {
        this.mongoClient = mongoClient ?? throw new ArgumentNullException(nameof(mongoClient));
        this.getId = getId ?? throw new ArgumentNullException(nameof(getId));
    }


    public Task Handle(StreamEvent<TEvent> @event, CancellationToken ct)
    {
        var id = getId(@event.Data);
        var indexName = nameof(TEvent);
    }
}
