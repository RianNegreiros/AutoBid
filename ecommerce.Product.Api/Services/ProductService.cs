using ecommerce.Infrastructure.Command.Product;
using ecommerce.Infrastructure.Event.Product;
using ecommerce.Product.Api.Repositories;

namespace ecommerce.Product.Api.Services;

public class ProductService : IProductService
{
  private readonly IProductRepository _productRepository;

  public ProductService(IProductRepository productRepository)
  {
    _productRepository = productRepository;
  }
  
  public async Task<ProductCreated> AddProduct(CreateProduct product)
  {
    return await _productRepository.AddProduct(product);
  }

  public async Task<ProductCreated> GetProduct(string ProductId)
  {
    return await _productRepository.GetProduct(ProductId);
  }
}
