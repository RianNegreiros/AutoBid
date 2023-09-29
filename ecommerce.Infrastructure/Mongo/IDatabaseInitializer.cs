namespace ecommerce.Infrastructure.Mongo;

public interface IDatabaseInitializer
{
  Task InitializeAsync();
}
