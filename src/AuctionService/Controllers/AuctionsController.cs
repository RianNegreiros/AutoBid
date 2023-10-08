using AuctionService.Data;
using AuctionService.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuctionsController : ControllerBase
{
  private readonly AuctionDbContext _context;
  private readonly IMapper _mapper;

  public AuctionsController(AuctionDbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<AuctionDto>>> GetAuctions()
  {
    var auctions = await _context.Auctions
      .Include(x => x.Item)
      .OrderBy(x => x.Id)
      .ToListAsync();

    return Ok(_mapper.Map<IEnumerable<AuctionDto>>(auctions));
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<AuctionDto>> GetAuction(Guid id)
  {
    var auction = await _context.Auctions
      .Include(x => x.Item)
      .FirstOrDefaultAsync(x => x.Id == id);

    if (auction == null)
      return NotFound();

    return Ok(_mapper.Map<AuctionDto>(auction));
  }
}
