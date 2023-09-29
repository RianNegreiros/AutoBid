using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace ecommerce.Infrastructure.Mongo;

public class MongoInitializer : IDatabaseInitializer
{
  private bool _initialized;
  private IMongoDatabase _database;
  
  public MongoInitializer(IMongoDatabase database)
  {
    _database = database;
  }

    public async Task InitializeAsync()
    {
        if (_initialized)
            return;

      IConventionPack conventionPack = new ConventionPack
      {
        new CamelCaseElementNameConvention(),
        new IgnoreExtraElementsConvention(true),
        new EnumRepresentationConvention(MongoDB.Bson.BsonType.String)
      };
      ConventionRegistry.Register("Conventions", conventionPack, x => true);

      _initialized = true;
      await Task.CompletedTask;
    }
}
