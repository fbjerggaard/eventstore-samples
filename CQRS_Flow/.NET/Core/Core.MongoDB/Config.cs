using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace Core.MongoDB;

public class MongoDbConfig
{
    public string ConnectionString { get; set; } = null!;
}

public static class MongoDbConfigExtensions
{
    private const string DefaultConfigKey = "MongoDB";

    public static void AddMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbConfig>(configuration.GetSection(DefaultConfigKey));

        var mongoDbConfig = configuration.GetSection(DefaultConfigKey).Get<MongoDbConfig>();

        services.AddSingleton<IMongoClient>(c => new MongoClient(mongoDbConfig.ConnectionString));
    }
}
