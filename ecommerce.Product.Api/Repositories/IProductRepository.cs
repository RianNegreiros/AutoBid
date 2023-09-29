using ecommerce.Infrastructure.Command.Product;
using ecommerce.Infrastructure.Event.Product;

namespace ecommerce.Product.Api.Repositories;

public interface IProductRepository
{
  Task<ProductCreated> GetProduct(Guid ProductId);
  Task<ProductCreated> AddProduct(CreateProduct product);
}
