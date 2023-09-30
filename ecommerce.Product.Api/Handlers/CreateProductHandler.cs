using ecommerce.Infrastructure.Command.Product;
using ecommerce.Product.Api.Services;
using MassTransit;

namespace ecommerce.Product.Api.Handlers;

public class CreateProductHandler : IConsumer<CreateProduct>
{
  private readonly IProductService _productService;
  public CreateProductHandler(IProductService productService)
  {
    _productService = productService;
  }

    public async Task Consume(ConsumeContext<CreateProduct> context)
    {
      await _productService.AddProduct(context.Message);
      await Task.CompletedTask;
    }
}
