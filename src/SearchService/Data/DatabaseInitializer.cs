using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Models;
using SearchService.Services;

namespace SearchService.Data;

public class DatabaseInitializer
{
  public static async Task Initialize(WebApplication app)
  {
    await DB.InitAsync("SearchDb", MongoClientSettings
      .FromConnectionString(app.Configuration.GetConnectionString("MongoDb")));

    await DB.Index<Item>()
      .Key(x => x.Make, KeyType.Text)
      .Key(x => x.Model, KeyType.Text)
      .Key(x => x.Color, KeyType.Text)
      .CreateAsync();

    var count = await DB.CountAsync<Item>();

    using var scope = app.Services.CreateScope();

    var httpClient = scope.ServiceProvider.GetRequiredService<AuctionSvcHttpClient>();

    var items = await httpClient.GetItemsForSearchDb();

    if (items.Count > 0)
      await DB.InsertAsync(items);

    Console.WriteLine($"Initialized SearchDb with {count} items.");
  }
}
