using AutoMapper;
using BidService.DTOs;
using BidService.Models;
using Contracts;

namespace BidService.RequestHelpers;

public class MappingProfiles : Profile
{
  public MappingProfiles()
  {
    CreateMap<Bid, BidDto>();
    CreateMap<Bid, BidPlaced>();
  }
}
