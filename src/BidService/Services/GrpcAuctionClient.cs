using AuctionService;
using BidService.Models;
using Grpc.Net.Client;

namespace BidService.Services;

public class GrpcAuctionClient(IConfiguration config, ILogger<GrpcAuctionClient> logger)
{
  private readonly ILogger<GrpcAuctionClient> _logger = logger;
  private readonly IConfiguration _config = config;

  public Auction GetAuction(string id)
  {
    _logger.LogInformation("Calling GRPC Service");
    var channel = GrpcChannel.ForAddress(_config["GrpcAuction"]);
    var client = new GrpcAuction.GrpcAuctionClient(channel);
    var request = new GetAuctionRequest { Id = id };

    try
    {
      var reply = client.GetAuction(request);
      var auction = new Auction
      {
        ID = reply.Auction.Id,
        AuctionEnd = DateTime.Parse(reply.Auction.AuctionEnd),
        Seller = reply.Auction.Seller,
        ReservePrice = reply.Auction.ReservePrice
      };

      return auction;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Could not call GRPC Server");
      return null;
    }
  }
}
