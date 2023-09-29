using ecommerce.Infrastructure.Command.Product;
using ecommerce.Infrastructure.Event.Product;

namespace ecommerce.Product.Api.Services;

public interface IProductService
{
  Task<ProductCreated> GetProduct(Guid ProductId);
  Task<ProductCreated> AddProduct(CreateProduct product);
}
