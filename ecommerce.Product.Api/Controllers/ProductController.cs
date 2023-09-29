using ecommerce.Infrastructure.Command.Product;
using ecommerce.Product.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Product.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
  private readonly IProductService _productService;

  public ProductController(IProductService productService)
  {
    _productService = productService;
  }

  [HttpPost]
  public async Task<IActionResult> CreateProduct([FromForm] CreateProduct product)
  {
    var result = await _productService.AddProduct(product);
    return Ok(result);
  }

  [HttpGet]
  public async Task<IActionResult> GetProduct(string productId)
  {
    var result = await _productService.GetProduct(productId);
    return Ok(result);
  }
}
