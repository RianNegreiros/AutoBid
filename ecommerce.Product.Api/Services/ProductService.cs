using ecommerce.Infrastructure.Command.Product;
using ecommerce.Infrastructure.Event.Product;

namespace ecommerce.Product.Api.Services;

public class ProductService : IProductService
{
  public Task<ProductCreated> AddProduct(CreateProduct product)
  {
    throw new NotImplementedException();
  }

  public Task<ProductCreated> GetProduct(Guid ProductId)
  {
    throw new NotImplementedException();
  }
}
