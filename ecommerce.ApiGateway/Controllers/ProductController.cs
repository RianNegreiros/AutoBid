using ecommerce.Infrastructure.Command.Product;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.ApiGateway.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
  [HttpGet]
  public async Task<IActionResult> Get()
  {
    await Task.CompletedTask;
    return Ok("Product list");
  }

  [HttpPost]
  public async Task<IActionResult> Add([FromForm] CreateProduct command)
  {
    await Task.CompletedTask;
    return Accepted("Product created");
  }
}
