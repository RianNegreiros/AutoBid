using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Services;

public class AuctionSvcHttpClient(HttpClient httpClient, IConfiguration config)
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly IConfiguration _config = config;

    public async Task<List<Item>> GetItemsForSearchDatabaseSeed()
    {
        var lastUpdated = await DB.Find<Item, string>()
            .Sort(x => x.Descending(x => x.UpdatedAt))
            .Project(x => x.UpdatedAt.ToString())
            .ExecuteFirstAsync();

        return await _httpClient.GetFromJsonAsync<List<Item>>(_config["AuctionServiceUrl"]
            + "/api/auctions?date=" + lastUpdated);
    }
}