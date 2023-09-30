using ecommerce.Infrastructure.Command.Product;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.ApiGateway.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
  private readonly IBusControl _bus;
  public ProductController(IBusControl bus)
  {
    _bus = bus;
  }

  [HttpGet]
  public async Task<IActionResult> Get()
  {
    await Task.CompletedTask;
    return Ok("Product list");
  }

  [HttpPost]
  public async Task<IActionResult> Add([FromForm] CreateProduct command)
  {
    var uri = new Uri("rabbitmq://localhost/create-product");
    var endPoint = await _bus.GetSendEndpoint(uri);
    await endPoint.Send(command);
    return Ok();
  }
}
